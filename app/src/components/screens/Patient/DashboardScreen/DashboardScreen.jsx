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
import { useEffect, useState } from "react";
import BottomSheet from "../../../ui/BottomSheet/BottomSheet.jsx";
import clsx from "clsx";
import useStore from "../../../../core/hooks/useStore.jsx";
import DailyPain from "../../../ui/DailyPain/DailyPain.jsx";
import { getBonusQuestionnaire, getDailyQuestionnaire, getMovementQuestionnaire, getUserData } from "../../../../core/utils/apiCalls.js";
import { useUser } from "../../../app/auth/AuthProvider.jsx";
import { toast } from "react-toastify";
import { useQuery } from "@tanstack/react-query";
import QuestionCategories from "../../../../core/config/questionCategories.js";

const DashboardScreen = () => {
  // state management
  const [sheetOpen, setSheetOpen] = useState(false);
  const [loading, setLoading] = useState(false);
  const [dailyQuestion, setDailyQuestion] = useState(null);
  const { resetEverything } = useStore();
  const setQuestions = useStore((state) => state.setQuestions);
  const setQuestionaireId = useStore((state) => state.setQuestionaireId);
  const setQuestionaireCategory = useStore(
    (state) => state.setQuestionaireCategory
  );

  // hooks
  const user = useUser();
  const navigate = useNavigate();

  // fetch data
  const { data, isLoading, isError } = useQuery({
    queryKey: ["user"],
    queryFn: () => getUserData(user.id),
  });

  // check if first launch of the day
  useEffect(() => {
    if (!data) return;

    const dailyQuestionnaire = async () => {
      const lastLaunch = localStorage.getItem("lastLaunch");
      const today = new Date().getDate();
      if (lastLaunch !== today.toString()) {
        try {
          const { data } = await getDailyQuestionnaire(user.id);
          setDailyQuestion(data);
          console.log(data);
        } catch (error) {
          toast.error("Er is iets misgegaan bij het ophalen van de vragenlijst.");
        }
      }
    }

    dailyQuestionnaire();
  }, [data, user.id]);

  if (isLoading) return null;

  if (isError) {
    toast.error("Er is iets misgegaan bij het ophalen van je gegevens.");
    return null;
  }

  const handleStartQuestionnaire = async (questionnaireCategory) => {
    setLoading(true);
    try {
      let result;
      if (questionnaireCategory === QuestionCategories.Movement) {
        result = await getMovementQuestionnaire(user.id);
      } else if (questionnaireCategory === QuestionCategories.Bonus) {
        result = await getBonusQuestionnaire(user.id);
      }
      const { data } = result;
      setLoading(false);
      console.log(data);
      resetEverything();
      setQuestionaireId(data.id);
      setQuestionaireCategory(questionnaireCategory);
      setQuestions(data.questions);
      navigate(PatientRoutes.Questionaire);
    } catch (error) {
      setLoading(false);
      toast.error("Er is iets misgegaan bij het ophalen van de vragenlijst.");
    }
  };

  const handleStartMovement = () => {
    handleStartQuestionnaire(QuestionCategories.Movement);
  };

  const handleStartBonus = () => {
    handleStartQuestionnaire(QuestionCategories.Bonus);
  };

  return (
    <FullHeightScreen>
      <DailyPain question={dailyQuestion} setQuestion={setDailyQuestion} />
      <div className={styles.screen}>
        <TopBar coins={data.data.coins} streak={data.data.streak} />
        <Avatar />
        <div className={styles.btnContainer}>
          <Button
            variant="primary"
            size="full"
            onClick={() => setSheetOpen(true)}
            disabled={loading}
          >
            Ik wil bewegen! <Play size={22} weight="bold" />
          </Button>
          <Button
            variant="secondary"
            size="full"
            onClick={handleStartBonus}
            disabled={loading}
          >
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
          disabled={loading}
        >
          Start vragenlijst
        </Button>
      </BottomSheet>
    </FullHeightScreen>
  );
};

export default DashboardScreen;
