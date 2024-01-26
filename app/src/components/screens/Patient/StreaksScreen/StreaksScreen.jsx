import FullHeightScreen from "../../../ui/FullHeightScreen/FullHeightScreen";
import { CheckCircle, X, XCircle } from "@phosphor-icons/react";
import Streaks from "../../../ui/Icons/Streaks";
import Button from "../../../ui/Button/Button";
import styles from "./StreaksScreen.module.css";
import { Link } from "react-router-dom";
import { PatientRoutes } from "../../../../core/config/routes";
import { useUser } from "../../../app/auth/AuthProvider";
import { getStreakHistory, getUserData } from "../../../../core/utils/apiCalls";
import { useQuery } from "@tanstack/react-query";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import useTitle from "../../../../core/hooks/useTitle";

const StreaksScreen = () => {
  const [streakResults, setStreakResults] = useState({});

  useTitle("Streaks");
  const user = useUser();

  const {
    data: userData,
    isLoading: userLoading,
    isError: userError,
  } = useQuery({
    queryKey: ["user"],
    queryFn: () => getUserData(user.id),
  });

  const {
    data: streakData,
    isLoading: streakLoading,
    isError: streakError,
  } = useQuery({
    queryKey: ["streak"],
    queryFn: () => getStreakHistory(user.id),
  });

  useEffect(() => {
    if (!streakData) return;
    const today = new Date();
    const dayNames = ["Zo", "Ma", "Di", "Wo", "Do", "Vr", "Za"];
    const dataDates = new Set(
      streakData.data.days.map((day) =>
        new Date(day.date).toLocaleDateString("nl-NL")
      )
    );
    const combinedResults = {};
    for (let i = 6; i >= 0; i--) {
      const date = new Date();
      date.setDate(today.getDate() - i);
      const dateString = date.toLocaleDateString("nl-NL");
      combinedResults[dayNames[date.getDay()]] = dataDates.has(dateString);
    }
    setStreakResults(combinedResults);
  }, [streakData]);

  if (!userData || !streakData) return;
  if (userLoading || streakLoading) return null;

  if (userError || streakError) {
    toast.error("Er is iets misgegaan bij het ophalen van je gegevens.");
    return null;
  }

  return (
    <FullHeightScreen className={styles.streaksContainer}>
      <Link
        to={PatientRoutes.Dashboard}
        className={`btn-reset ${styles.closeBtn}`}
      >
        <X size={32} />
      </Link>
      <div className={styles.center}>
        <div>
          <div className={styles.streaks}>
            <div>{userData.data.streak}</div>
            <Streaks size={50} />
          </div>
          <div className={styles.streaksText}>dagen op een rij!</div>
        </div>
        <div className={styles.stats}>
          <div className={styles.weekdays}>
            {Object.entries(streakResults).map(([day, result]) => (
              <div key={day} className={styles.day}>
                {day}
                {result ? (
                  <CheckCircle
                    size={32}
                    weight="fill"
                    className={styles.StreakColor}
                  />
                ) : (
                  <XCircle
                    size={32}
                    weight="fill"
                    className={styles.noStreakColor}
                  />
                )}
              </div>
            ))}
          </div>
          <div className={styles.streaksInfo}>
            Behoud je voortgang door dagelijks te sporten of een vragenlijst in
            te vullen!
          </div>
        </div>
      </div>
      <Link to={PatientRoutes.Dashboard}>
        <Button size="full">Begrepen</Button>
      </Link>
    </FullHeightScreen>
  );
};

export default StreaksScreen;
