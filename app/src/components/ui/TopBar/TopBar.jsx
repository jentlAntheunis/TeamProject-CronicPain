import Coin from "../Icons/Coin";
import Streaks from "../Icons/Streaks";
import RewardMetric from "../RewardMetric/RewardMetric.jsx";

import styles from "./TopBar.module.css";
import Menu from "../Icons/Menu";

const TopBar = () => {
  return (
    <div className={styles.spaceBetween}>
      <div className={styles.flex}>
        <RewardMetric number={300}>
          <Coin />
        </RewardMetric>
        <RewardMetric number={2}>
          <Streaks />
        </RewardMetric>
      </div>
      <Menu />
    </div>
  );
};

export default TopBar;
