import { isSignInWithEmailLink, signInWithEmailLink } from "firebase/auth";
import { useEffect, useState } from "react";
import { Navigate, Outlet, useLocation } from "react-router-dom";
import { auth } from "../../../core/services/firebase";
import { useAuthState } from "react-firebase-hooks/auth";
import CenteredInfo from "../../ui/Centered/CenteredInfo";
import { toast } from "react-toastify";

const OnboardingLayout = () => {
  const location = useLocation();
  const search = location.search;
  // TODO: Check auth status
  const [user, loading] = useAuthState(auth);

  // useStates
  const [loginLoading, setLoginLoading] = useState(false);
  const [error, setError] = useState(false);

  useEffect(() => {
    if (!user) {
      if (isSignInWithEmailLink(auth, window.location.href)) {
        let email = window.localStorage.getItem("emailForSignIn");
        if (!email) {
          // ask user for email
          email = window.prompt("Geef uw e-mailadres voor bevestiging");
        }
        // sign in user
        setLoginLoading(true);
        signInWithEmailLink(auth, email, window.location.href)
          .then(() => {
            window.localStorage.removeItem("emailForSignIn");
            console.log("signed in");
            setLoginLoading(false);
            setError(false);
          })
          .catch((error) => {
            console.log(error.message);
            setLoginLoading(false);
            setError(true);
          });
      }
    }
  }, [user, location, search]);

  if (loading) {
    return null;
  }

  if (loginLoading) {
    return (
      <CenteredInfo>
        <h1>Proberen inloggen...</h1>
      </CenteredInfo>
    );
  }

  if (error) {
    console.error(error.message);
  }

  if (!user) {
    return <Outlet />;
  }

  // check if a previous path was available
  const fromPath = location.state?.from?.pathname || "/";

  return <Navigate to={fromPath} replace={true} />;
};

export default OnboardingLayout;
