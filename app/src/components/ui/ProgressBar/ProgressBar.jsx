import styles from "./ProgressBar.module.css";

const ProgressBar = ({ progress }) => (
  <div className={styles.progressBar}>
    <div className={styles.progressInner} style={{ width: `${progress}%` }} />
  </div>
);

export default ProgressBar;
