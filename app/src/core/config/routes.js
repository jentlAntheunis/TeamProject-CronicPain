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
  PatientDetails: "/patients/:id",
  QuestionsOverview: "/questions",
  AddQuestion: "/questions/add",
};

export { AuthRoutes, PatientRoutes, SpecialistRoutes };
