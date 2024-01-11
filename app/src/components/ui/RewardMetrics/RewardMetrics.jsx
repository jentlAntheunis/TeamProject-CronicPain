import styles from './RewardMetrics.module.css';

const RewardMetrics = ({children, number}) => (
    <span className={styles.background}>
      {children}
      <span className={styles.font}>{number}</span>
    </span>
)

export default RewardMetrics;
