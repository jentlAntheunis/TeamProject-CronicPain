const AuthRoutes = {
  Login: "/login",
};

const PatientRoutes = {
  Dashboard: "/patient/dashboard",
  Progress: "/patient/progress",
  Shop: "/patient/shop",
  Questionaire: '/patient/questionaire',
  Streaks: "/patient/streaks",
  WellDone: '/patient/well-done',
  MovementSuggestions: '/patient/movement-suggestions',
  TimeTracker: '/patient/time-tracker',
};

const SpecialistRoutes = {
  PatientsOverview: "/specialist/patients",
  AddPatient: "/specialist/patients/add",
  AddPatientCsv: "/specialist/patients/add/csv",
  PatientDetails: "/specialist/patients/:id",
  QuestionsOverview: "/specialist/questions",
  AddQuestion: "/specialist/questions/add",
  AddQuestionCsv: "/specialist/questions/add/csv",
  QuestionnaireDetails: "/specialist/questionnaire/details",
};

export { AuthRoutes, PatientRoutes, SpecialistRoutes };
