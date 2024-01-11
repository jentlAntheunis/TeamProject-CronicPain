import Coin from "../Icons/Coin";
import Streaks from "../Icons/Streaks";
import RewardMetric from "../RewardMetric/RewardMetric.jsx";

import styles from "./RewardMetrics.module.css";

const RewardMetrics = () => {
  return (
    <div className={styles.flex}>
      <RewardMetric number={300}>
        <Coin />
      </RewardMetric>
      <RewardMetric number={2}>
        <Streaks />
      </RewardMetric>
    </div>
  );
};

export default RewardMetrics;
