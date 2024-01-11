import { Navigate, Outlet, useLocation } from "react-router-dom";
import { AuthRoutes } from "../../../core/config/routes";

const AuthContainer = () => {
  console.log("test");

  const location = useLocation();
  // TODO: Check auth status
  const auth = false;

  if (!auth) {
    return (
      <Navigate to={AuthRoutes.Login} state={{ from: location }} replace />
    );
  }

  return <Outlet />;
};

export default AuthContainer;
