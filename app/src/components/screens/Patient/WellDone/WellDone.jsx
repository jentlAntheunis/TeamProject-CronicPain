import Avatar from "../../../ui/Avatar/Avatar";
import Button from "../../../ui/Button/Button";
import FullHeightScreen from "../../../ui/FullHeightScreen/FullHeightScreen";
import styles from "./WellDone.module.css";

const WellDone = () => {
  return (
    <FullHeightScreen>
      <div className={styles.layout}>
        <div>
          <Avatar />
          <div className={styles.textContainer}>
            <div className={styles.goedGedaan}>Goed gedaan!</div>
            <div className={styles.tijd}>
              Je bewoog voor 6 minuten en 24 seconden
            </div>
            <div className={styles.uitleg}>
              Je zult nu opnieuw <strong>dezelfde vragen</strong> krijgen om een
              beter inzicht te krijgen in de impact van deze bewegingssessie
            </div>
          </div>
        </div>
        <Button size="full">Ik snap het</Button>
      </div>
    </FullHeightScreen>
  );
};

export default WellDone;
