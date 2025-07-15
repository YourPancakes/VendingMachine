import React, { useEffect, useState, useRef } from 'react';
import { Provider } from 'react-redux';
import { store } from './app/store';
import { SodaDrinksPage } from './ui/pages/SodaDrinksPage';
import { CheckoutPage } from './ui/pages/CheckoutPage/CheckoutPage';
import { PaymentPage } from './ui/pages/PaymentPage/PaymentPage';
import { ResultPage } from './ui/pages/ResultPage/ResultPage';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { getMachineLockStatus, lockMachine } from './api/machineLockApi';

function getUserName() {
  return 'user-' + Math.random().toString(36).slice(2, 10);
}

function App() {
  const [locked, setLocked] = useState<boolean | null>(null);
  const [userName] = useState(getUserName());
  const lockRequested = useRef(false);

  useEffect(() => {
    async function checkAndLock() {
      try {
        const status = await getMachineLockStatus();
        if (status.isLocked) {
          setLocked(true);
        } else if (!lockRequested.current) {
          lockRequested.current = true;
          await lockMachine(userName);
          setLocked(false);
        }
      } catch {
        setLocked(true);
      }
    }
    checkAndLock();
    const handleUnload = () => {
      try {
        navigator.sendBeacon(
          'http://localhost:5071/api/v1.0/MachineLock/release',
          new Blob([JSON.stringify(userName)], { type: 'application/json' })
        );
      } catch {}
    };
    window.addEventListener('beforeunload', handleUnload);
    window.addEventListener('unload', handleUnload);
    return () => {
      window.removeEventListener('beforeunload', handleUnload);
      window.removeEventListener('unload', handleUnload);
    };
  }, [userName]);

  if (locked === null) {
    return <div className="d-flex vh-100 justify-content-center align-items-center"><div className="fs-3">Loading...</div></div>;
  }
  if (locked) {
    return (
      <div className="d-flex vh-100 justify-content-center align-items-center bg-light">
        <div className="bg-white p-5 rounded shadow text-center">
          <div className="fs-3 mb-3">Sorry, the vending machine is currently in use</div>
          <div className="fs-5">Please try again later</div>
        </div>
      </div>
    );
  }

  return (
    <Provider store={store}>
      <Router>
        <Routes>
          <Route path="/" element={<SodaDrinksPage />} />
          <Route path="/checkout" element={<CheckoutPage />} />
          <Route path="/payment" element={<PaymentPage />} />
          <Route path="/result" element={<ResultPage />} />
          <Route path="*" element={<Navigate to="/" />} />
        </Routes>
      </Router>
    </Provider>
  );
}

export default App;
