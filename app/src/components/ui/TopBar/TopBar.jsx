import { useState } from "react";
import Coin from "../Icons/Coin";
import Streaks from "../Icons/Streaks";
import RewardMetric from "../RewardMetric/RewardMetric.jsx";

import styles from "./TopBar.module.css";
import { DotsThreeVertical, SignOut, Scroll } from "@phosphor-icons/react";

const Menu = ({ showMenu }) => {
  if (!showMenu) {
    return null;
  }

  return (
    <div className={styles.menu}>
      <button className={`btn-reset ${styles.menuItem}`}>
        Uitloggen
        <SignOut size={24} />
      </button>
      <button className={`btn-reset ${styles.menuItem}`}>
        Credits
        <Scroll size={24} />
        </button>
    </div>
  );
};

const TopBar = () => {
  const [showMenu, setShowMenu] = useState(false);

  const onClickDots = () => {
    setShowMenu(!showMenu);
  };

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
      <div className={styles.menuContainer}>
        <DotsThreeVertical
          size={32}
          weight="bold"
          className={styles.dots}
          onClick={onClickDots}
        />
        <Menu showMenu={showMenu} />
      </div>
    </div>
  );
};

export default TopBar;
