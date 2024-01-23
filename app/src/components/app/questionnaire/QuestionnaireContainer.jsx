import { Navigate, Outlet } from "react-router-dom";
import useStore from "../../../core/hooks/useStore"
import { PatientRoutes } from "../../../core/config/routes";

const QuestionnaireContainer = ({ children }) => {
  const { questionaireId } = useStore();

  if (questionaireId === null) {
    return <Navigate to={PatientRoutes.Dashboard} />;
  }

  return children;
}

export default QuestionnaireContainer;
