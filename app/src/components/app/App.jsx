import { Outlet, Route, Routes } from "react-router-dom";
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
import MovementSuggestions from "../screens/Patient/MovementSuggestions/MovementSuggestions";
import TimeTracker from "../screens/Patient/TimeTracker/TimeTracker";
import Patients from "../screens/Specialist/Patients/Patients";
import Questions from "../screens/Specialist/Questions/Questions";
import AddPatientCsv from "../screens/Specialist/AddPatientCsv/AddPatientCsv";
import QuestionnaireContainer from "./questionnaire/QuestionnaireContainer";
import PatientDetails from "../screens/Specialist/PatientDetails/PatientDetails";
import AddQuestion from "../screens/Specialist/AddQuestion/AddQuestion";
import QuestionnaireDetails from "../screens/Specialist/QuestionnaireDetails/QuestionnaireDetails";
import Shop from "../screens/Patient/Shop/Shop";
import RoleRouter from "./auth/RoleRouter";
import Progress from "../screens/Patient/Progress/Progress";
import AddQuestionCsv from "../screens/Specialist/AddQuestionCsv/AddQuestionCsv";
import ExtraInfoScreen from "../screens/Patient/ExtraInfo/ExtraInfoScreen";

const App = () => (
  <Routes>
    <Route path="*" element={<RoleRouter />} />
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
        <Route
          path={SpecialistRoutes.PatientsOverview}
          element={<Patients />}
        />
        <Route path={SpecialistRoutes.AddPatient} element={<AddPatient />} />
        <Route
          path={SpecialistRoutes.AddPatientCsv}
          element={<AddPatientCsv />}
        />
        <Route
          path={SpecialistRoutes.QuestionsOverview}
          element={<Questions />}
        />
        {/* <Route path={SpecialistRoutes.AddQuestion} element={<AddQuestion />} /> */}
        <Route
          path={SpecialistRoutes.PatientDetails}
          element={<PatientDetails />}
        />
        <Route
          path={SpecialistRoutes.QuestionsOverview}
          element={<Questions />}
        />
        <Route
          path={SpecialistRoutes.QuestionnaireDetails}
          element={<QuestionnaireDetails />}
        />
        <Route path={SpecialistRoutes.AddQuestion} element={<AddQuestion />} />
        <Route
          path={SpecialistRoutes.AddQuestionCsv}
          element={<AddQuestionCsv />}
        />
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
        <Route path={PatientRoutes.Streaks} element={<StreaksScreen />} />
        <Route path={PatientRoutes.Shop} element={<Shop />} />
        <Route path={PatientRoutes.Progress} element={<Progress />} />
        <Route path={PatientRoutes.ExtraInfo} element={<ExtraInfoScreen />} />

        {/* Questionnaire Paths */}
        <Route
          element={
            <QuestionnaireContainer>
              <Outlet />
            </QuestionnaireContainer>
          }
        >
          <Route
            path={PatientRoutes.Questionaire}
            element={<QuestionaireScreen />}
          />
          <Route
            path={PatientRoutes.MovementSuggestions}
            element={<MovementSuggestions />}
          />
          <Route path={PatientRoutes.TimeTracker} element={<TimeTracker />} />
          <Route path={PatientRoutes.WellDone} element={<WellDone />} />
        </Route>
      </Route>
    </Route>
  </Routes>
);

export default App;
