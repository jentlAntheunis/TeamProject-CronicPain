import { ArrowLeft, Play, Stop } from "@phosphor-icons/react";
import Button from "../../ui/Button/Button";
import FullHeightScreen from "../../ui/FullHeightScreen/FullHeightScreen";
import styles from "./TimeTracker.module.css";
import { useStopwatch } from "react-timer-hook";

const TimeTracker = () => {
  return (
    <FullHeightScreen className={styles.screen}>
      <button className={`btn-reset ${styles.backBtn}`}>
        <ArrowLeft size={32} />
      </button>
      <MyStopwatch />
    </FullHeightScreen>
  );
};

function MyStopwatch() {
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