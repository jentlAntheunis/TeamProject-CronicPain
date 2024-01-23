const AuthRoutes = {
  Login: "/login",
};

const PatientRoutes = {
  Dashboard: "/patient/",
  Progress: "/patient/progress",
  Shop: "/patient/shop",
  Questionaire: '/patient/questionaire',
  Streaks: "/patient/streaks",
  WellDone: '/patient/well-done',
  MovementSuggestions: '/patient/movement-suggestions',
  TimeTracker: '/patient/time-tracker',
};

const SpecialistRoutes = {
  Index: "/specialist/",
  PatientsOverview: "/specialist/patients",
  AddPatient: "/specialist/patients/add",
  AddPatientCsv: "/specialist/patients/add/csv",
  PatientDetails: "/specialist/patients/:id",
  QuestionsOverview: "/specialist/questions",
  AddQuestion: "/specialist/questions/add",
  QuestionnaireDetails: "/questionnaire/details",
};

export { AuthRoutes, PatientRoutes, SpecialistRoutes };
