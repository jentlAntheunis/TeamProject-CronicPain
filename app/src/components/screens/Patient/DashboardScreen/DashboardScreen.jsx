import Avatar from "../../../ui/Avatar/Avatar.jsx";
import TabBarNav from "../../../ui/TabBarNav/TabBarNav.jsx";
import TopBar from "../../../ui/TopBar/TopBar.jsx";
import { Play, ClipboardText, X } from "@phosphor-icons/react";
import styles from "./DashboardScreen.module.css";
import FullHeightScreen from "../../../ui/FullHeightScreen/FullHeightScreen.jsx";
import Button from "../../../ui/Button/Button.jsx";
import { useAuthState } from "react-firebase-hooks/auth";
import { auth } from "../../../../core/services/firebase.js";
import { useNavigate } from "react-router-dom";
import { PatientRoutes } from "../../../../core/config/routes.js";
import { useState } from "react";
import BottomSheet from "../../../ui/BottomSheet/BottomSheet.jsx";
import clsx from "clsx";
import useStore from "../../../../core/hooks/useStore.jsx";
import { ExampleQuestions } from "../../../../core/config/questions.js";
import DailyPain from "../../../ui/DailyPain/DailyPain.jsx";
import { getUserData } from "../../../../core/utils/apiCalls.js";
import { useUser } from "../../../app/auth/AuthProvider.jsx";
import { toast } from "react-toastify";
import { useQuery } from "@tanstack/react-query";

const DashboardScreen = () => {
  // state management
  const [sheetOpen, setSheetOpen] = useState(false);
  const {
    removeAnswers,
    resetCurrentQuestion,
    resetQuestionaireIndex,
    resetMovementTime,
  } = useStore();
  const setQuestions = useStore((state) => state.setQuestions);

  // hooks
  const user = useUser();
  const navigate = useNavigate();

  // fetch data
  const { data, isLoading, isError } = useQuery({
    queryKey: ["user"],
    queryFn: () => getUserData(user.id),
  });

  if (isLoading) return null;

  if (isError) {
    toast.error("Er is iets misgegaan bij het ophalen van je gegevens.");
    return null;
  }

  const handleStartMovement = () => {
    removeAnswers();
    resetCurrentQuestion();
    resetQuestionaireIndex();
    resetMovementTime();
    setQuestions(ExampleQuestions);
    navigate(PatientRoutes.Questionaire);
  };

  return (
    <FullHeightScreen>
      <DailyPain />
      <div className={styles.screen}>
        <TopBar coins={data.data.coins} streak={data.data.streak} />
        <Avatar />
        <div className={styles.btnContainer}>
          <Button
            variant="primary"
            size="full"
            onClick={() => setSheetOpen(true)}
          >
            Ik wil bewegen! <Play size={22} weight="bold" />
          </Button>
          <Button variant="secondary" size="full">
            Vul bonusvragen in <ClipboardText size={22} weight="bold" />
          </Button>
        </div>
      </div>
      <TabBarNav />
      <BottomSheet open={sheetOpen} setOpen={setSheetOpen}>
        <h1 className={styles.sheetTitle}>Super!</h1>
        <button
          className={clsx("btn-reset", styles.sheetClose)}
          onClick={() => setSheetOpen(false)}
        >
          <X size={32} />
        </button>
        <p className={styles.sheetText}>
          Voor je begint heb ik nog een vragenlijst voor jou.
        </p>
        <Button
          variant="primary"
          size="full"
          onClick={() => handleStartMovement()}
        >
          Start vragenlijst
        </Button>
      </BottomSheet>
    </FullHeightScreen>
  );
};

export default DashboardScreen;
