import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import "./normalize.css";
import "./main.css";
import "react-toastify/dist/ReactToastify.min.css";
import App from "./components/app/App.jsx";
import AuthProvider from "./components/app/auth/AuthProvider.jsx";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import ScrollToTop from "./components/app/routing/ScrollToTop.jsx";

const queryClient = new QueryClient();

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <BrowserRouter>
      <ScrollToTop />
      <QueryClientProvider client={queryClient}>
        <AuthProvider>
          <App />
        </AuthProvider>
      </QueryClientProvider>
      <ToastContainer hideProgressBar theme="colored" draggable={false} />
    </BrowserRouter>
  </React.StrictMode>
);
