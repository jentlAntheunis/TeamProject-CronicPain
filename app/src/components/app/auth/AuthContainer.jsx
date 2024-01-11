import PropTypes from "prop-types";
import { Navigate, Outlet, useLocation } from "react-router-dom";
import { AuthRoutes } from "../../../core/config/routes";

const AuthContainer = () => {
  const location = useLocation();
  // TODO: Check auth status
  const auth = true;
  console.log("auth");
  if (!auth) {
    return (
      <Navigate to={AuthRoutes.Login} state={{ from: location }} replace />
    );
  }

  return <Outlet />;
};

export default AuthContainer;
