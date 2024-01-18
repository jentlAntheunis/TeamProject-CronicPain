const firebaseTimeToCurrentTime = (firebaseTime) => {
  const gmtTimestamp = new Date(firebaseTime);
  const brusselsTimestamp = new Date(gmtTimestamp.toLocaleString("en-US", { timeZone: "Europe/Brussels" }));

  const formattedTimestamp = brusselsTimestamp.toLocaleString("en-GB", {
    day: "2-digit",
    month: "2-digit",
    year: "numeric",
    hour: "2-digit",
    minute: "2-digit",
    timeZone: "Europe/Brussels"
  });

  return formattedTimestamp;
}

export default firebaseTimeToCurrentTime;
