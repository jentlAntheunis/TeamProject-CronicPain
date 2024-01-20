import { ArrowLeft, Play, Stop } from "@phosphor-icons/react";
import Button from "../../ui/Button/Button";
import FullHeightScreen from "../../ui/FullHeightScreen/FullHeightScreen";
import styles from "./TimeTracker.module.css";
import { useStopwatch } from "react-timer-hook";
import { Link } from "react-router-dom";
import { PatientRoutes } from "../../../core/config/routes";
import { useNavigate } from "react-router-dom";
import useStore from "../../../core/hooks/useStore";
import { timeToStringValue } from "../../../core/utils/timeData";
import { useWakeLock } from "react-screen-wake-lock";

const TimeTracker = () => {
  return (
    <FullHeightScreen className={styles.screen}>
      <Link
        to={PatientRoutes.MovementSuggestions}
        className={`btn-reset ${styles.backBtn}`}
      >
        <ArrowLeft size={32} />
      </Link>
      <MyStopwatch />
    </FullHeightScreen>
  );
};

const MyStopwatch = () => {
  // state management
  const {
    questionaireId,
    questionaireIndex,
    answers,
    incrementQuestionaireIndex,
    resetCurrentQuestion,
    resetEverything,
    removeAnswers,
  } = useStore();
  const setMovementTime = useStore((state) => state.setMovementTime);

  const navigate = useNavigate();
  const {
    totalSeconds,
    seconds,
    minutes,
    hours,
    isRunning,
    start,
    pause,
    reset,
  } = useStopwatch();
  const { isSupported, request, release } = useWakeLock();

  // https://www.npmjs.com/package/react-timer-hook

  const handleButtonClick = () => {
    if (isRunning) {
      release();
      pause();

      if (minutes >= 5) {
        setMovementTime(totalSeconds);
        // TODO: store time in database
        // TODO: store questionaire in database
        const data = {
          questionnaireId: questionaireId,
          questionnaireIndex: questionaireIndex,
          answers: [...answers],
        };
        console.log(data);
        removeAnswers();
        incrementQuestionaireIndex();
      } else {
        resetEverything();
      }
      resetCurrentQuestion();
      navigate(PatientRoutes.WellDone);
    } else {
      !isSupported && console.warn("Wake Lock API not supported");
      request();
      start();
    }
  };

  return (
    <>
      <div className={styles.counter}>
        <span>{timeToStringValue(hours)}</span>:
        <span>{timeToStringValue(minutes)}</span>:
        <span>{timeToStringValue(seconds)}</span>
      </div>
      <Button
        size="full"
        onClick={handleButtonClick}
        className={isRunning ? styles.startStopBtn : ""}
      >
        {isRunning ? "Stop met bewegen" : "Start met bewegen"}
        {isRunning ? (
          <Stop size={22} weight="bold" />
        ) : (
          <Play size={22} weight="bold" />
        )}
      </Button>
    </>
  );
};

export default TimeTracker;
