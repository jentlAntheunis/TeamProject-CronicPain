import Avatar from "../../../ui/Avatar/Avatar.jsx";
import TabBarNav from "../../../ui/TabBarNav/TabBarNav.jsx";
import TopBar from "../../../ui/TopBar/TopBar.jsx";
import { Play, ClipboardText, X } from "@phosphor-icons/react";
import styles from "./DashboardScreen.module.css";
import FullHeightScreen from "../../../ui/FullHeightScreen/FullHeightScreen.jsx";
import Button from "../../../ui/Button/Button.jsx";
import { useNavigate } from "react-router-dom";
import { PatientRoutes } from "../../../../core/config/routes.js";
import { useEffect, useState } from "react";
import BottomSheet from "../../../ui/BottomSheet/BottomSheet.jsx";
import clsx from "clsx";
import useStore from "../../../../core/hooks/useStore.jsx";
import DailyPain from "../../../ui/DailyPain/DailyPain.jsx";
import {
  checkStreak,
  getBonusQuestionnaire,
  getDailyQuestionnaire,
  getMovementQuestionnaire,
  getUserData,
  getPebblesMood,
  checkBonusQuestionnaire,
} from "../../../../core/utils/apiCalls.js";
import { useUser } from "../../../app/auth/AuthProvider.jsx";
import { toast } from "react-toastify";
import { useMutation, useQuery } from "@tanstack/react-query";
import QuestionCategories from "../../../../core/config/questionCategories.js";

const DashboardScreen = () => {
  // state management
  const [sheetOpen, setSheetOpen] = useState(false);
  const [loading, setLoading] = useState(false);
  const [dailyQuestion, setDailyQuestion] = useState(null);
  const [fetchData, setFetchData] = useState(false);
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
  const { data: bonusDone, isError: checkBonusError } = useQuery({
    queryKey: ["bonus", user.id],
    queryFn: () => checkBonusQuestionnaire(user.id),
  });
  const {
    mutate: streaksMutate,
    isLoading: isStreaksLoading,
    isError: isStreaksError,
  } = useMutation({
    mutationKey: ["streaks", user.id],
    mutationFn: () => checkStreak(user.id),
    onSuccess: async () => {
      setFetchData(true);
      await dailyQuestionnaire();
    },
  });
  const { data, isError } = useQuery({
    queryKey: ["user", user.id],
    queryFn: () => getUserData(user.id),
    enabled: !!fetchData,
  });

  const { data: moodData, isError: moodError } = useQuery({
    queryKey: ["mood"],
    queryFn: () => getPebblesMood(user.id),
  });

  useEffect(() => {
    if (checkFirstLaunch()) {
      streaksMutate();
    } else {
      setFetchData(true);
    }
  }, [streaksMutate]);

  const checkFirstLaunch = () => {
    const lastLaunch = localStorage.getItem("lastLaunch");
    const today = new Date().getDate();
    if (lastLaunch !== today.toString()) {
      return true;
    } else {
      return false;
    }
  };

  const dailyQuestionnaire = async () => {
    if (checkFirstLaunch()) {
      try {
        const { data } = await getDailyQuestionnaire(user.id);
        setDailyQuestion(data);
      } catch (error) {
        toast.error("Er is iets misgegaan bij het ophalen van de vragenlijst.");
      }
    }
  };

  if (isError || isStreaksError || moodError || checkBonusError) {
    toast.error("Er is iets misgegaan bij het ophalen van je gegevens.");
    return null;
  }

  if (!data || isStreaksLoading || !moodData || !bonusDone) return null;

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
      // TODO: Check if first movement questionnaire of the day and store in zustand
      setLoading(false);
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
        <Avatar color={data.data.avatar.color.hex} mood={moodData.data} />
        <div className={styles.btnContainer}>
          <Button
            variant="primary"
            size="full"
            onClick={() => setSheetOpen(true)}
            disabled={loading}
          >
            Ik wil bewegen! <Play size={22} weight="bold" />
          </Button>
          {!bonusDone.data && (
            <Button
              variant="secondary"
              size="full"
              onClick={handleStartBonus}
              disabled={loading}
            >
              Vul bonusvragen in <ClipboardText size={22} weight="bold" />
            </Button>
          )}
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
