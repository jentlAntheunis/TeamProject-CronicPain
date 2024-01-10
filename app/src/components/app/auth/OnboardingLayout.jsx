import { Navigate, Outlet, useLocation } from "react-router-dom";

const OnboardingLayout = () => {
  const location = useLocation();
  // TODO: Check auth status
  const auth = false;

  if (!auth) {
    return <Outlet />;
  }

  // check if a previous path was available
  const fromPath = location.state?.from?.pathname || "/";

  return <Navigate to={fromPath} replace={true} />;
};

export default OnboardingLayout;
