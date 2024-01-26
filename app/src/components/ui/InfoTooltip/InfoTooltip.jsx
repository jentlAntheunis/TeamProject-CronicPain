import styles from "./InfoTooltip.module.css";
import { Info } from "@phosphor-icons/react";

const InfoTooltip = ({ text, onClick }) => {
  return (
    <div className={styles.tooltip} onClick={onClick}>
      <Info size={22} className={styles.tooltipIcon} />
      <span className={styles.tooltiptext}>{text}</span>
    </div>
  );
};

export default InfoTooltip;
