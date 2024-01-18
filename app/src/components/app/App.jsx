import { Routes } from "react-router-dom";
import OnboardingLayout from "./auth/OnboardingLayout";
import { Route } from "react-router-dom";

const App = () => (
  <Routes>
    {/* Authentication Paths */}
    <Route path="/" element={<OnboardingLayout />} />
  </Routes>
);

export default App;
