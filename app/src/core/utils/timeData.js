const secondsToMinutesSeconds = (totalSeconds) => {
  const minutes = Math.floor(totalSeconds / 60);
  const seconds = totalSeconds % 60;

  return { minutes, seconds };
}

const minutesSecondsToText = ({ minutes, seconds }) => {
  const minutesLabel = minutes === 1 ? "minuut" : "minuten";
  const secondsLabel = seconds === 1 ? "seconde" : "seconden";

  const minutesText = minutes > 0 && `${minutes} ${minutesLabel}`;
  const secondsText = `${seconds} ${secondsLabel}`;

  const result = minutesText ? `${minutesText} en ${secondsText}` : secondsText;

  return result;
}

const timeToStringValue = (value) => {
  return value.toString().padStart(2, "0");
}

export { secondsToMinutesSeconds, minutesSecondsToText, timeToStringValue };
