import styles from "./PageHeading.module.css";
import { ArrowLeft } from "@phosphor-icons/react";

const PageHeading = ({ children }) => (
  <div className={styles.pageHeading}>
    <button className={`btn-reset ${styles.backBtn}`}>
      <ArrowLeft size={32} display="block" className={styles.backIcon}/>
      <div className={`desktop-only ${styles.backText}`}>Terug</div>
    </button>
    <h1 className={styles.heading}>{children}</h1>
  </div>
);

export default PageHeading;
