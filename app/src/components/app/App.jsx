import { Navigate, Outlet, Route, Routes } from "react-router-dom";
import OnboardingLayout from "./auth/OnboardingLayout";
import LoginScreen from "./screens/LoginScreen";
import RegisterScreen from "./screens/RegisterScreen";
import { AuthRoutes } from "../../core/config/routes";
import AuthContainer from "./auth/AuthContainer";
import RoleContainer from "./auth/RoleContainer";
import { UserRoles } from "../../core/config/userRoles";

const App = () => (
  <Routes>
    {/* Authentication Paths */}
    <Route
      path={AuthRoutes.Index}
      element={<Navigate to={AuthRoutes.Login} />}
    />
    <Route path={AuthRoutes.Index} element={<OnboardingLayout />}>
      <Route path={AuthRoutes.Login} element={<LoginScreen />} />
      <Route path={AuthRoutes.Register} element={<RegisterScreen />} />
      <Route path="*" element={<Navigate to={AuthRoutes.Login} />} />
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
