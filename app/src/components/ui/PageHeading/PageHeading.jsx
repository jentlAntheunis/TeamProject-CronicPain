import styles from "./PageHeading.module.css";
import { ArrowLeft } from "@phosphor-icons/react";
import { Link } from "react-router-dom";

const PageHeading = ({ backLink, onBack, children }) => (
  <div className={styles.pageHeading}>
    {backLink ? (
      <Link to={backLink} className={`btn-reset ${styles.backBtn}`}>
        <ArrowLeft size={32} display="block" className={styles.backIcon} />
        <div className={`desktop-only ${styles.backText}`}>Terug</div>
      </Link>
    ) : (
      <button className={`btn-reset ${styles.backBtn}`} onClick={onBack}>
        <ArrowLeft size={32} display="block" className={styles.backIcon} />
        <div className={`desktop-only ${styles.backText}`}>Terug</div>
      </button>
    )}
    <h1 className={styles.heading}>{children}</h1>
  </div>
);

export default PageHeading;
