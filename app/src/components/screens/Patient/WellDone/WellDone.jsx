import { PatientRoutes } from "../../../../core/config/routes";
import useStore from "../../../../core/hooks/useStore";
import {
  minutesSecondsToText,
  secondsToMinutesSeconds,
} from "../../../../core/utils/timeData";
import Avatar from "../../../ui/Avatar/Avatar";
import Button from "../../../ui/Button/Button";
import FullHeightScreen from "../../../ui/FullHeightScreen/FullHeightScreen";
import styles from "./WellDone.module.css";
import { useNavigate } from "react-router-dom";
import { useLocation } from "react-router-dom";

// TODO: Replace this with the actual value from the timer
const totalSeconds = 846;

const WellDone = () => {
  // state management
  const { movementTime, incrementQuestionaireIndex, resetCurrentQuestion } = useStore();

  const navigate = useNavigate();
  const { state } = useLocation();
  console.log(state);

  const { minutes, seconds } = secondsToMinutesSeconds(movementTime);
  const timeText = minutesSecondsToText({ minutes, seconds });

  const isLongSession = movementTime > 5 * 60;
  const celebration = isLongSession ? "Goed gedaan!" : "Een goed begin!";
  const text = isLongSession
    ? `Je bewoog voor ${timeText}`
    : `Je bewoog slechts ${timeText}, hierdoor zal je geen muntjes krijgen`;

  const uitleg = isLongSession ? (
    <>
      Je zult nu opnieuw <strong>dezelfde vragen</strong> krijgen om een beter
      inzicht te krijgen in de impact van deze bewegingssessie
    </>
  ) : null;

  const handleClick = () => {
    incrementQuestionaireIndex();
    resetCurrentQuestion();
    navigate(PatientRoutes.Questionaire);
  };

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
        <Button size="full" onClick={handleClick}>
          Ik snap het
        </Button>
      </div>
    </FullHeightScreen>
  );
};

export default WellDone;
