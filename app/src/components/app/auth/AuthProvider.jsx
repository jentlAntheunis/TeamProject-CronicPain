import { createContext, useContext, useEffect, useState } from "react";
import { auth } from "../../../core/services/firebase";
import { getUser } from "../../../core/utils/apiCalls";
import { useAuthState } from "react-firebase-hooks/auth";

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
  const [localAuth, setLocalAuth] = useState(getAuthFromLocalStorage());
  const [user, loading] = useAuthState(auth);

  useEffect(() => {
    if (localAuth) {
      saveAuthToLocalStorage(localAuth);
    } else {
      localStorage.removeItem(KEY);
    }
  }, [localAuth]);

  useEffect(() => {
    if (!user && !loading) {
      setLocalAuth(null);
    }
  }, [user, loading]);

  const logout = () => {
    auth.signOut();
    setLocalAuth(null);
  };

  const login = (email) => {
    return new Promise((resolve, reject) => {
      getUser(email)
        .then(({ data }) => {
          setLocalAuth(data);
          resolve(data);
        })
        .catch((error) => {
          reject(error);
        });
    });
  };

  return (
    <AuthContext.Provider value={{ user: localAuth, login, logout }}>
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
