import { useQuery } from "@tanstack/react-query";
import { PatientRoutes } from "../../../../core/config/routes";
import useStore from "../../../../core/hooks/useStore";
import { getUserData } from "../../../../core/utils/apiCalls";
import {
  minutesSecondsToText,
  secondsToMinutesSeconds,
} from "../../../../core/utils/timeData";
import Avatar from "../../../ui/Avatar/Avatar";
import Button from "../../../ui/Button/Button";
import FullHeightScreen from "../../../ui/FullHeightScreen/FullHeightScreen";
import styles from "./WellDone.module.css";
import { useNavigate } from "react-router-dom";
import { useUser } from "../../../app/auth/AuthProvider";
import { toast } from "react-toastify";
import { PebblesMoods } from "../../../../core/config/pebblesMoods";
import useTitle from "../../../../core/hooks/useTitle";

const WellDone = () => {
  // state management
  const { movementTime } = useStore();

  useTitle("Klaar met bewegen");
  const user = useUser();
  const navigate = useNavigate();

  const {
    data: userData,
    isLoading: userLoading,
    isError: userError,
  } = useQuery({
    queryKey: ["user"],
    queryFn: () => getUserData(user.id),
    refetchOnWindowFocus: false,
  });

  if (!userData) return;
  if (userLoading) return null;

  if (userError) {
    toast.error("Er is iets misgegaan bij het ophalen van je gegevens.");
    return null;
  }

  const { minutes, seconds } = secondsToMinutesSeconds(movementTime);
  const timeText = minutesSecondsToText({ minutes, seconds });

  const isLongSession = minutes >= 5;
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

  const pebblesMood = isLongSession ? PebblesMoods.Happy : PebblesMoods.Neutral;

  const handleClick = () => {
    if (isLongSession) {
      navigate(PatientRoutes.Questionaire);
    } else {
      navigate(PatientRoutes.Dashboard);
    }
  };

  return (
    <FullHeightScreen>
      <div className={styles.layout}>
        <div>
          <Avatar color={userData.data.avatar.color.hex} mood={pebblesMood} />
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
