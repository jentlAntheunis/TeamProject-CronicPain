import Avatar from "../../../ui/Avatar/Avatar";
import Button from "../../../ui/Button/Button";
import FullHeightScreen from "../../../ui/FullHeightScreen/FullHeightScreen";
import styles from "./WellDone.module.css";

const WellDone = () => {
  const totalSeconds = 299; // Deze waarde krijgen we van de timer
  const minutes = Math.floor(totalSeconds / 60);
  const seconds = totalSeconds % 60;
  const minutesText = minutes === 1 ? "minuut" : "minuten";
  const timeText = `${minutes > 0 ? `${minutes} ${minutesText} en` : ''} ${seconds} seconden`;
  let text;
  let celebration;
  let uitleg;
  if (totalSeconds > 5 * 60) {
    celebration = "Goed gedaan!";
    text = `Je bewoog voor ${timeText}`;
    uitleg = (
      <>
        Je zult nu opnieuw <strong>dezelfde vragen</strong> krijgen om een beter
        inzicht te krijgen in de impact van deze bewegingssessie
      </>
    );
  } else {
    celebration = "Een goed begin!";
    text =
    `Je bewoog slechts ${timeText}, hierdoor zal je geen muntjes krijgen`;
    uitleg = "";
  }

  return (
    <FullHeightScreen>
      <div className={styles.layout}>
        <div>
          <Avatar />
          <div className={styles.textContainer}>
            <div className={styles.goedGedaan}>{celebration}</div>
            <div className={styles.tijd}>{text}</div>
            <div className={styles.uitleg}>{uitleg}</div>
          </div>
        </div>
        <Button size="full">Ik snap het</Button>
      </div>
    </FullHeightScreen>
  );
};

export default WellDone;
