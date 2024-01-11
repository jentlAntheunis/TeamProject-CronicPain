import { Navigate, Outlet, useLocation } from "react-router-dom";
import { AuthRoutes } from "../../../core/config/routes";
import { useAuthState } from "react-firebase-hooks/auth";
import { auth } from "../../../core/services/firebase";

const AuthContainer = () => {
  console.log("test");

  const location = useLocation();
  const [user] = useAuthState(auth);

  if (!user) {
    return (
      <Navigate to={AuthRoutes.Login} state={{ from: location }} replace />
    );
  }

  return <Outlet />;
};

export default AuthContainer;
