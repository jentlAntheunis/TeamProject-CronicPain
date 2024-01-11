import Coin from "../Icons/Coin";
import Streaks from "../Icons/Streaks";
import RewardMetric from "../RewardMetric/RewardMetric.jsx";

import styles from "./TopBar.module.css";
import { DotsThreeVertical } from "@phosphor-icons/react";

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
      <DotsThreeVertical size={32} weight="bold" />
    </div>
  );
};

export default TopBar;
