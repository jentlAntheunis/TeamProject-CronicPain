import styles from "./PageHeading.module.css";
import { ArrowLeft } from "@phosphor-icons/react";
import clsx from "clsx";
import { Link } from "react-router-dom";

const PageHeading = ({ backLink, onBack, children, className }) => (
  <div className={clsx(styles.pageHeading, className)}>
    {(backLink || onBack) && (
      <Link to={backLink} className={`btn-reset ${styles.backBtn}`} onClick={onBack || null}>
        <ArrowLeft size={32} display="block" className={styles.backIcon} />
        <div className={`desktop-only ${styles.backText}`}>Terug</div>
      </Link>
    )}
    <h1 className={styles.heading}>{children}</h1>
  </div>
);

export default PageHeading;
