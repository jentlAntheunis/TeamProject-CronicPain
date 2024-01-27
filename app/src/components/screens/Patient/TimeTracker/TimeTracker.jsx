import { ArrowLeft, Play, Stop } from "@phosphor-icons/react";
import Button from "../../../ui/Button/Button";
import FullHeightScreen from "../../../ui/FullHeightScreen/FullHeightScreen";
import styles from "./TimeTracker.module.css";
import { useStopwatch } from "react-timer-hook";
import { Link } from "react-router-dom";
import { PatientRoutes } from "../../../../core/config/routes";
import { useNavigate } from "react-router-dom";
import useStore from "../../../../core/hooks/useStore";
import { timeToStringValue } from "../../../../core/utils/timeData";
import { useWakeLock } from "react-screen-wake-lock";
import { toast } from "react-toastify";
import { sendAnswers, storeMovement } from "../../../../core/utils/apiCalls";
import { useState } from "react";
import { useUser } from "../../../app/auth/AuthProvider";
import useTitle from "../../../../core/hooks/useTitle";

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
  const [loading, setLoading] = useState(false);

  useTitle("Bewegingssessie");
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
  const user = useUser();
  const { isSupported, request, release } = useWakeLock({
    onRequest: () => console.info("Wake Lock was requested"),
    onRelease: () => console.info("Wake Lock was released"),
    onError: (err) => console.error("Wake Lock request failed", err),
  });

  // https://www.npmjs.com/package/react-timer-hook

  const handleButtonClick = async () => {
    if (isRunning) {
      release();
      pause();

      if (minutes >= 0) {
        setMovementTime(totalSeconds);
        setLoading(true);

        try {
          await storeMovement(user.id, totalSeconds);
          setLoading(false);
          incrementQuestionaireIndex();
        } catch (error) {
          setLoading(false);
          toast.error(
            "Er ging iets mis bij het opslaan van je bewegingssessie."
          );
          console.error(error);
          resetEverything();
          navigate(PatientRoutes.Dashboard);
        }
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
        disabled={loading}
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
