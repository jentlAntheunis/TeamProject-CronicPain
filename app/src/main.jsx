import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import "./normalize.css";
import "./main.css";
import "react-toastify/dist/ReactToastify.min.css";
import App from "./components/app/App.jsx";
import AuthProvider from "./components/app/auth/AuthProvider.jsx";

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <BrowserRouter>
      <AuthProvider>
        <App />
      </AuthProvider>
      <ToastContainer hideProgressBar theme="colored" draggable={false} />
    </BrowserRouter>
  </React.StrictMode>
);
