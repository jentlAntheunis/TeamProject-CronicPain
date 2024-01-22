import { Navigate, Outlet, Route, Routes } from "react-router-dom";
import OnboardingLayout from "./auth/OnboardingLayout";
import AuthContainer from "./auth/AuthContainer";
import RoleContainer from "./auth/RoleContainer";
import { UserRoles } from "../../core/config/userRoles";
import LoginScreen from "../screens/LoginScreen/LoginScreen";
import AddPatient from "../screens/Specialist/AddPatient/AddPatient";
import {
  AuthRoutes,
  PatientRoutes,
  SpecialistRoutes,
} from "../../core/config/routes";
import QuestionaireScreen from "../screens/Patient/Questionaire/QuestionaireScreen";
import DashboardScreen from "../screens/Patient/DashboardScreen/DashboardScreen";
import WellDone from "../screens/Patient/WellDone/WellDone";
import StreaksScreen from "../screens/Patient/StreaksScreen/StreaksScreen";
import MovementSuggestions from "../screens/MovementSuggestions/MovementSuggestions";
import TimeTracker from "../screens/TimeTracker/TimeTracker";
import Patients from "../screens/Specialist/Patients/Patients";
import Questions from "../screens/Specialist/Questions/Questions";
import AddQuestion from "../screens/Specialist/AddQuestion/AddQuestion";

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
        <Route path={SpecialistRoutes.PatientsOverview} element={<Patients />} />
        <Route path={SpecialistRoutes.AddPatient} element={<AddPatient />} />
        <Route path={SpecialistRoutes.QuestionsOverview} element={<Questions />} />
        <Route path={SpecialistRoutes.AddQuestion} element={<AddQuestion />} />
        <Route path="*" element={<Navigate to={SpecialistRoutes.PatientsOverview} />} />
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
        <Route path={PatientRoutes.Dashboard} element={<DashboardScreen />} />
        <Route
          path={PatientRoutes.Questionaire}
          element={<QuestionaireScreen />}
        />
        <Route path={PatientRoutes.MovementSuggestions} element={<MovementSuggestions />} />
        <Route path={PatientRoutes.TimeTracker} element={<TimeTracker />} />
        <Route path={PatientRoutes.Streaks} element={<StreaksScreen />} />
        <Route path={PatientRoutes.WellDone} element={<WellDone />} />
        <Route path="*" element={<Navigate to={PatientRoutes.Dashboard} />} />
      </Route>
    </Route>
  </Routes>
);

export default App;
