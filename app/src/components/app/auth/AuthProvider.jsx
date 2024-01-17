import { createContext, useContext, useEffect, useState } from "react";
import { auth } from "../../../core/services/firebase";
import { getUser } from "../../../core/utils/apiCalls";

// localstorage key
const KEY = "PEBBLES_AUTH";

const AuthContext = createContext();

const getAuthFromLocalStorage = () => {
  const user = localStorage.getItem(KEY);
  // return auth in base64 encode
  return user ? JSON.parse(atob(user)) : null;
};

const saveAuthToLocalStorage = (user) => {
  // save auth in base64 decode
  localStorage.setItem(KEY, btoa(JSON.stringify(user)));
};

const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(getAuthFromLocalStorage());

  useEffect(() => {
    if (user) {
      saveAuthToLocalStorage(user);
    } else {
      localStorage.removeItem(KEY);
    }
  }, [user]);

  const logout = () => {
    auth.signOut();
    setUser(null);
  };

  const login = (email) => {
    return new Promise((resolve, reject) => {
      getUser(email)
        .then(({ data }) => {
          console.log("res", data);
          setUser(data);
          resolve(data);
        })
        .catch((error) => {
          reject(error);
        });
    });
  };

  return (
    <AuthContext.Provider
      value={{ user, login, logout }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export const useAuthContext = () => {
  return useContext(AuthContext);
};

export const useUser = () => {
  const { user } = useAuthContext();
  return user;
};

export default AuthProvider;
