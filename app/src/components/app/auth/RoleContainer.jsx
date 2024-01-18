import PropTypes from "prop-types";
import { Navigate } from "react-router-dom";
import { useUser } from "./AuthProvider";

const RoleContainer = ({ roles = [], children }) => {
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
