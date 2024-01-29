import clsx from "clsx";
import styles from "./RewardMetric.module.css";

const RewardMetric = ({ children, number, className }) => (
  <span className={clsx(styles.background, className)}>
    {children}
    <span className={styles.font}>{number}</span>
  </span>
);

export default RewardMetric;
