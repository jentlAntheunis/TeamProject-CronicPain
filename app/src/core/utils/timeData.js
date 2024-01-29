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

// date to dd/mm/yyyy hh:mm
const dateToDateTimeString = (date) => {
  const day = date.getDate().toString().padStart(2, "0");
  const month = (date.getMonth() + 1).toString().padStart(2, "0");
  const year = date.getFullYear().toString();
  const hours = date.getHours().toString().padStart(2, "0");
  const minutes = date.getMinutes().toString().padStart(2, "0");

  return `${day}/${month}/${year} ${hours}:${minutes}`;
}

export { secondsToMinutesSeconds, minutesSecondsToText, timeToStringValue, dateToDateTimeString };
