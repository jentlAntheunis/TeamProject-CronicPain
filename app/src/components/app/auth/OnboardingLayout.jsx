import {
  isSignInWithEmailLink,
  signInWithEmailAndPassword,
  signInWithEmailLink,
} from "firebase/auth";
import { useEffect, useState } from "react";
import { Navigate, Outlet, useLocation } from "react-router-dom";
import { auth } from "../../../core/services/firebase";
import {
  useAuthState,
  useSignInWithEmailAndPassword,
} from "react-firebase-hooks/auth";
import CenteredInfo from "../../ui/Centered/CenteredInfo";
import { toast } from "react-toastify";
import { set } from "react-hook-form";
import firebaseTimeToCurrentTime from "../../../core/config/timestamp";

const OnboardingLayout = () => {
  // TODO: Check auth status
  const [loadingLogin, setLoadingLogin] = useState(true);
  const [token, setToken] = useState(null);

  const [user, loading] = useAuthState(auth);

  useEffect(() => {
    if (loading) {
      return;
    }
    const getToken = async () => {
      setLoadingLogin(true);
      signInWithEmailAndPassword(auth, "test@pebbles-health.be", "password123")
        .then((userCredential) => {
          // Signed in
          const user = userCredential.user;
          getTokenFromUser(user);
        })
        .catch((error) => {
          setLoadingLogin(false);
          toast.error(error.message);
        });
    };
    const getTokenFromUser = async (user) => {
      setLoadingLogin(true);
      user.getIdTokenResult().then((token) => {
        setToken(token);
        setLoadingLogin(false);
      });
    };

    if (user) {
      getTokenFromUser(user);
      return;
    }

    getToken();
  }, [user, loading]);

  if (loadingLogin || loading || !user || !token) {
    return null;
  }

  console.log(token);

  return (
    <>
      <div style={{ width: "100vw", overflow: "hidden" }}>
        <p>Token:</p>
        <pre style={{ display: "inline-block", wordBreak: "break-all" }}>{token.token}</pre>
      </div>
      <div>
        <p>Expiration</p>
        <pre>{firebaseTimeToCurrentTime(token.expirationTime)}</pre>
      </div>
    </>
  );
};

export default OnboardingLayout;
