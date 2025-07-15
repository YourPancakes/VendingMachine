# Vending Machine Project

A full-stack vending machine simulation platform, consisting of:
- **Backend**: ASP.NET Core Web API (C#)
- **Frontend**: React + TypeScript

## Architecture

- **VendingMachine.Server**: RESTful API for managing products, brands, cart, coins, and machine lock state. Uses PostgreSQL for data storage.
- **VendingMachine.Client**: Modern React SPA for interacting with the vending machine (product selection, cart, payment, etc).

Both services are containerized and orchestrated via Docker Compose.

---

## Quick Start (Docker Compose)

1. **Clone the repository:**
   ```sh
   git clone https://github.com/YourPancakes/VendingMachine
   cd VendingMachine
   ```
2. **Build and run all services:**
   ```sh
   docker-compose up --build
   ```
3. **Access the apps:**
   - Frontend: [http://localhost:3000](http://localhost:3000)
   - API (Swagger): [http://localhost:5071/swagger](http://localhost:5071/swagger)
   - PostgreSQL: localhost:5432 (user: postgres, password: password)

---

## Backend (VendingMachine.Server)

- **Tech:** ASP.NET Core 8, Entity Framework Core, PostgreSQL, AutoMapper, FluentValidation
- **API:**
  - `/api/v1.0/brands` — CRUD for brands
  - `/api/v1.0/products` — CRUD and filtering for products
  - `/api/v1.0/cart` — Cart management, purchase
  - `/api/v1.0/coins` — Coin management, change calculation
  - `/api/v1.0/machinelock` — Lock/unlock vending machine

---

## Frontend (VendingMachine.Client)

- **Tech:** React 19, TypeScript, Redux Toolkit, React Query, Bootstrap
- **Features:**
  - Product catalog with filtering (brand, price, stock)
  - Cart management (add, update, remove, clear)
  - Payment simulation (coin input, change calculation)
  - Machine lock/unlock
  - Responsive UI
