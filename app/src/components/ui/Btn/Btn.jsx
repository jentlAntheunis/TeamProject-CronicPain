import styles from "./Btn.module.css";

const Btn = ({ children, text, type, className }) => {
  let btnStyle = styles.btnPrimary;

  if (type === "secondary") {
    btnStyle = styles.btnSecondary;
  }

  return (
    <button className={`${btnStyle} ${styles.btn} btn-reset ${className}`}>
      {text}
      {children}
    </button>
  );
};

export default Btn;
