import { useEffect, useState } from "react";
import { PebblesMoods } from "../../../core/config/pebblesMoods";
import Pebbles from "../Illustrations/Pebbles";
import styles from "./Avatar.module.css";
import clsx from "clsx";

const Avatar = ({
  color = "#3B82F6",
  mood = PebblesMoods.Neutral,
  tooltipActive = false,
}) => {
  const [showTooltip, setShowTooltip] = useState(false);

  useEffect(() => {
    // When the user clicks anywhere outside of the tooltip, close it
    const handleClickOutside = () => {
      setShowTooltip(false);
    };

    document.addEventListener("mousedown", handleClickOutside);
  }, [showTooltip]);

  return (
    <div className={styles.container}>
      <div
        className={styles.avatar}
        onClick={tooltipActive ? () => setShowTooltip(true) : null}
      >
        <Pebbles shieldColor={color} mood={mood} size="12rem" />
      </div>
      <div className={styles.tooltipWrapper} style={{ display: showTooltip ? "block" : "none" }}>
        <span className={clsx(styles.tooltip)}>
          Maak mij blij door dagelijks te bewegen of door bonusvragen in te
          vullen!
        </span>
      </div>
    </div>
  );
};

export default Avatar;
