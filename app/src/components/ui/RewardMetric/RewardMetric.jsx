import styles from './RewardMetric.module.css';

const RewardMetric = ({children, number}) => (
    <span className={styles.background}>
      {children}
      <span className={styles.font}>{number}</span>
    </span>
)

export default RewardMetric;
