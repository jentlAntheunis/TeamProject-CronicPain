import clsx from "clsx";
import styles from "./BottomSheet.module.css";

const BottomSheet = ({ open, setOpen, children }) => {
  if (!open) return null;

  return (
    <div className={styles.container}>
      <div className={styles.background} onClick={() => setOpen(false)}></div>
      <div className={clsx(styles.bottomSheet, open && styles.opened)}>
        <div className={styles.bottomSheetInner}>
          {children}
        </div>
      </div>
    </div>
  );
};

export default BottomSheet;
