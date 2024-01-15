import styling from "./CenteredInfo.module.css";

const CenteredInfo = ({ children }) => (
  <div className={styling.container}>
    <div className={styling.overlay}>
      {children}
    </div>
  </div>
);

export default CenteredInfo;
