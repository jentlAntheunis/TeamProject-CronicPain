import { AuthRoutes, PatientRoutes, SpecialistRoutes } from "../../../core/config/routes";
import { UserRoles } from "../../../core/config/userRoles";
import { useUser } from "./AuthProvider";
import { Navigate } from "react-router-dom";

const RoleRouter = () => {
  const user = useUser();

  if (!user) {
    return <Navigate to={AuthRoutes.Login} />;
  }

  if (user.role === UserRoles.Specialist) {
    return <Navigate to={SpecialistRoutes.PatientsOverview} />;
  }

  if (user.role === UserRoles.Patient) {
    return <Navigate to={PatientRoutes.Dashboard} />;
  }
}

export default RoleRouter;
