import styles from "./PageHeading.module.css";
import { ArrowLeft } from "@phosphor-icons/react";

const PageHeading = ({ children }) => (
  <div className={styles.pageHeading}>
    <button className={`btn-reset ${styles.arrow}`}>
      <ArrowLeft size={32} />
    </button>
    <h1 className={styles.heading}>{children}</h1>
  </div>
);

export default PageHeading;
