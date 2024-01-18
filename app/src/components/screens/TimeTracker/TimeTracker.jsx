import { ArrowLeft, Play, Stop } from "@phosphor-icons/react";
import Button from "../../ui/Button/Button";
import FullHeightScreen from "../../ui/FullHeightScreen/FullHeightScreen";
import styles from "./TimeTracker.module.css";
import { useStopwatch } from "react-timer-hook";
import { Link } from "react-router-dom";
import { PatientRoutes } from "../../../core/config/routes";
import { useNavigate } from "react-router-dom";
import useStore from "../../../core/hooks/useStore";

const TimeTracker = () => {
  return (
    <FullHeightScreen className={styles.screen}>
      <Link to={PatientRoutes.MovementSuggestions} className={`btn-reset ${styles.backBtn}`}>
        <ArrowLeft size={32} />
      </Link>
      <MyStopwatch />
    </FullHeightScreen>
  );
};

function MyStopwatch() {
  // state management
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


  // https://www.npmjs.com/package/react-timer-hook

  const formatTime = (value) => {
    return value.toString().padStart(2, "0");
  };

  const handleButtonClick = () => {
    if (isRunning) {
      pause();
      setMovementTime(totalSeconds);
      navigate(PatientRoutes.WellDone);
    } else {
      start();
    }
  };

  return (
    <>
      <div className={styles.counter}>
        <span>{formatTime(hours)}</span>:<span>{formatTime(minutes)}</span>:
        <span>{formatTime(seconds)}</span>
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
}

export default TimeTracker;
