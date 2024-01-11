import styles from './RewardMetrics.module.css';

const RewardMetrics = ({children, number}) => (
    <span className={styles.background}>
      {children}
      <span>{number}</span>
    </span>
)

export default RewardMetrics;
