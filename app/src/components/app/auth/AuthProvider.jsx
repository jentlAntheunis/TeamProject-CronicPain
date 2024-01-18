import { createContext, useContext, useEffect, useState } from "react";

// localstorage key
const KEY = "PEBBLES_AUTH";

const AuthContext = createContext();

const getAuthFromLocalStorage = () => {
  const auth = localStorage.getItem(KEY);
  // return auth in base64 encode
  return auth ? JSON.parse(atob(auth)) : null;
};

const saveAuthToLocalStorage = (auth) => {
  // save auth in base64 decode
  localStorage.setItem(KEY, btoa(JSON.stringify(auth)));
};

const AuthProvider = ({ children }) => {
  const [auth, setAuth] = useState(getAuthFromLocalStorage());

  useEffect(() => {
    if (auth) {
      saveAuthToLocalStorage(auth);
    } else {
      localStorage.removeItem(KEY);
    }
  }, [auth]);

  const handleLogout = () => {
    setAuth(null);
  };

  const handleLogin = (auth) => {
    setAuth(auth);
  };

  return (
    <AuthContext.Provider
      value={{ auth, saveAuth: handleLogin, removeAuth: handleLogout }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export const useAuthContext = () => {
  return useContext(AuthContext);
};

export const useUser = () => {
  const { auth } = useAuthContext();
  return auth;
};

export default AuthProvider;
