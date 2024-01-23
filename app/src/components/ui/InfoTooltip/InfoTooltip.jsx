import styles from "./InfoTooltip.module.css";
import { Info } from "@phosphor-icons/react";

const InfoTooltip = ({ text }) => {
  return (
    <div className={styles.tooltip}>
      <Info size={22} className={styles.tooltipIcon} />
      <span className={styles.tooltiptext}>{text}</span>
    </div>
  );
};

export default InfoTooltip;
