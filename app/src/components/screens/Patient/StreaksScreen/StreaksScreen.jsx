import FullHeightScreen from "../../../ui/FullHeightScreen/FullHeightScreen";
import { CheckCircle, X, XCircle } from "@phosphor-icons/react";
import Streaks from "../../../ui/Icons/Streaks";
import Button from "../../../ui/Button/Button";
import styles from "./StreaksScreen.module.css";

const StreaksScreen = () => {
  return (
    <FullHeightScreen className={styles.streaksContainer}>
      <button className={`btn-reset ${styles.closeBtn}`}>
        <X size={32} />
      </button>
      <div className={styles.center}>
        <div>
          <div className={styles.streaks}>
            <div>3</div>
            <Streaks size={50} />
          </div>
          <div className={styles.streaksText}>dagen op een rij!</div>
        </div>
        <div className={styles.stats}>
          <div className={styles.weekdays}>
            <div className={styles.day}>
              Ma
              <CheckCircle size={32} weight="fill" className={styles.StreakColor}/>
            </div>
            <div className={styles.day}>
              Di
              <XCircle size={32} weight="fill" className={styles.noStreakColor}/>
            </div>
            <div className={styles.day}>
              Wo
              <XCircle size={32} weight="fill" className={styles.noStreakColor}/>
            </div>
            <div className={styles.day}>
              Do
              <XCircle size={32} weight="fill" className={styles.noStreakColor}/>
            </div>
            <div className={styles.day}>
              Vr
              <CheckCircle size={32} weight="fill" className={styles.StreakColor}/>
            </div>
            <div className={styles.day}>
              Za
              <CheckCircle size={32} weight="fill" className={styles.StreakColor}/>
            </div>
            <div className={styles.day}>
              Zo
              <CheckCircle size={32} weight="fill" className={styles.StreakColor}/>
            </div>
          </div>
          <div className={styles.streaksInfo}>
            Behoud je voortgang door dagelijks te sporten of een vragenlijst in
            te vullen!
          </div>
        </div>
      </div>
      <Button size="full">Begrepen</Button>
    </FullHeightScreen>
  );
};

export default StreaksScreen;
