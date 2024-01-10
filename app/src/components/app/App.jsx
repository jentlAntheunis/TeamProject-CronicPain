import { Navigate, Outlet, Route, Routes } from "react-router-dom";
import OnboardingLayout from "./auth/OnboardingLayout";
import RegisterScreen from "../screens/RegisterScreen";
import { AuthRoutes } from "../../core/config/routes";
import AuthContainer from "./auth/AuthContainer";
import RoleContainer from "./auth/RoleContainer";
import { UserRoles } from "../../core/config/userRoles";
import LoginScreen from "../screens/LoginScreen";

const App = () => (
  <Routes>
    {/* Authentication Paths */}
    <Route path={AuthRoutes.Login} element={<OnboardingLayout />}>
      <Route index element={<LoginScreen />} />
    </Route>

    {/* Routes when logged in */}
    <Route element={<AuthContainer />}>
      {/* Admin */}
      <Route
        element={
          <RoleContainer roles={[UserRoles.Admin]}>
            <Outlet />
          </RoleContainer>
        }
      >
        {/* Admin Paths */}
      </Route>

      {/* Specialist */}
      <Route
        element={
          <RoleContainer roles={[UserRoles.Specialist]}>
            <Outlet />
          </RoleContainer>
        }
      >
        {/* Specialist Paths */}
      </Route>

      {/* Patient */}
      <Route
        element={
          <RoleContainer roles={[UserRoles.Patient]}>
            <Outlet />
          </RoleContainer>
        }
      >
        {/* Patient Paths */}
      </Route>
    </Route>
  </Routes>
);

export default App;
