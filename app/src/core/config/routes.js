const AuthRoutes = {
  Login: "/login",
};

const PatientRoutes = {
  Dashboard: "/",
  Progress: "/progress",
  Shop: "/shop",
  Questionaire: '/questionaire',
  Streaks: "/streaks",
  WellDone: '/well-done',
  MovementSuggestions: '/movement-suggestions',
  TimeTracker: '/time-tracker',
};

const SpecialistRoutes = {
  Index: "/",
  PatientsOverview: "/patients",
  AddPatient: "/patients/add",
  QuestionsOverview: "/questions",
  AddQuestion: "/questions/add",
  // TODO: change route to "/questionnaire/:id"
  QuestionnaireDetails: "/questionnaire/details",
};

export { AuthRoutes, PatientRoutes, SpecialistRoutes };
