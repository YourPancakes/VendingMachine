import React from 'react';
import { useLocation, useNavigate } from 'react-router-dom';

export function ResultPage() {
  const location = useLocation();
  const navigate = useNavigate();
  const { changeAmount, changeCoins, message } = (location.state as any) || { changeAmount: 0, changeCoins: [], message: undefined };

  let errorMessage = undefined;
  if (Array.isArray(message) && message.length > 0 && message[0].message) {
    errorMessage = message[0].message;
  } else if (typeof message === 'string') {
    errorMessage = message;
  }

  const canGiveChange = changeAmount >= 0;

  const handleBackToCatalog = () => {
    navigate('/');
  };

  return (
    <div className="container py-5 text-center" style={{ maxWidth: 600 }}>
      {canGiveChange ? (
        <>
          <h1 className="mb-4">Thank you for your purchase, please take your change</h1>
          <div className="fs-4 fw-semibold mb-3">Change: {changeAmount}₽</div>
          <div className="mb-4">
            <div className="fw-semibold mb-2">Your change coins:</div>
            {Array.isArray(changeCoins) && changeCoins.length > 0 ? (
              <>
                {changeCoins.map((c: any, idx: number) =>
                  c.quantity > 0 ? (
                    <div key={idx} className="fs-5">{c.quantity} coin{c.quantity > 1 ? 's' : ''} of {c.denomination}₽</div>
                  ) : null
                )}
              </>
            ) : (
              <div className="text-muted">No change coins</div>
            )}
          </div>
          <button className="btn btn-primary" onClick={handleBackToCatalog}>Drinks catalog</button>
        </>
      ) : (
        <>
          <h1 className="mb-4">{errorMessage || 'Sorry, we cannot sell you the product because the machine cannot give you the required change'}</h1>
          <button className="btn btn-primary" onClick={handleBackToCatalog}>Drinks catalog</button>
        </>
      )}
    </div>
  );
} 