import axios from "axios";
import { isSignInWithEmailLink, signInWithEmailLink } from "firebase/auth";
import { useEffect, useState } from "react";
import { Navigate, Outlet, useLocation } from "react-router-dom";
import { auth } from "../../../core/services/firebase";
import { useAuthState } from "react-firebase-hooks/auth";
import CenteredInfo from "../../ui/Centered/CenteredInfo";
import { toast } from "react-toastify";
import { useAuthContext } from "./AuthProvider";

const OnboardingLayout = () => {
  const location = useLocation();
  const search = location.search;
  // TODO: Check auth status
  const { user, login, logout } = useAuthContext();

  // useStates
  const [loginLoading, setLoginLoading] = useState(false);
  const [error, setError] = useState(false);

  useEffect(() => {
    const handleSignInWithEmailLink = async () => {
      let email = window.localStorage.getItem("emailForSignIn");
      if (!email) {
        // ask user for email
        email = window.prompt("Geef uw e-mailadres voor bevestiging");
      }
      try {
        setLoginLoading(true);
        const { user } = await signInWithEmailLink(
          auth,
          email,
          window.location.href
        );
        await login(user.email);
        window.localStorage.removeItem("emailForSignIn");
        setLoginLoading(false);
        setError(false);
      } catch (error) {
        console.log(error.message);
        setLoginLoading(false);
        setError(true);
      }
    };

    if (!user && isSignInWithEmailLink(auth, window.location.href)) {
      handleSignInWithEmailLink();
    }
  }, [user, location, search]);

  // if (loading) {
  //   return null;
  // }

  if (loginLoading) {
    return (
      <CenteredInfo>
        <h1>Proberen inloggen...</h1>
      </CenteredInfo>
    );
  }

  if (error) {
    toast.error(
      "Er is iets misgelopen bij het inloggen, probeer opnieuw of neem contact op met ons"
    );
  }

  if (!user) {
    return <Outlet />;
  }

  // check if a previous path was available
  const fromPath = location.state?.from?.pathname || "/";

  return <Navigate to={fromPath} replace={true} />;
};

export default OnboardingLayout;
