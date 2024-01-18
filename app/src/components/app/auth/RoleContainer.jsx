import PropTypes from "prop-types";
import { Navigate } from "react-router-dom";
import { UserRoles } from "../../../core/config/userRoles";
import { useUser } from "./AuthProvider";

const RoleContainer = ({ roles = [], children }) => {
  // TODO: Check auth status
  const user = useUser();

  if (!roles.includes(user.role)) {
    return <Navigate to="/" />;
  }

  return children;
};

RoleContainer.propTypes = {
  roles: PropTypes.array,
  children: PropTypes.node,
};

export default RoleContainer;
