import FullHeightScreen from "../../../ui/FullHeightScreen/FullHeightScreen";
import useStore from "../../../../core/hooks/useStore";
import QuestionTopBar from "../../../ui/QuestionTopBar/QuestionTopBar";
import Button from "../../../ui/Button/Button";
import styles from "./QuestionaireScreen.module.css";
import Slider from "../../../ui/Slider/Slider";
import { useEffect, useState } from "react";
import { defaultAnswer, orderOptions } from "../../../../core/utils/questions";
import { capitalize } from "../../../../core/utils/formatText";
import { useNavigate } from "react-router-dom";
import { PatientRoutes } from "../../../../core/config/routes";
import RecieveCoinsModal from "../../../ui/Modal/RecieveCoinsModal";
import QuestionCategories from "../../../../core/config/questionCategories";
import { addCoins } from "../../../../core/utils/apiCalls";
import { useUser } from "../../../app/auth/AuthProvider";
import { toast } from "react-toastify";

const QuestionaireScreen = () => {
  // States
  const [sliderValue, setSliderValue] = useState(0);
  const [showModal, setShowModal] = useState(false);
  const [loading, setLoading] = useState(false);
  // amount of coins
  const [amount, setAmount] = useState(0);

  // Hooks
  const user = useUser();
  const {
    questions,
    currentQuestion,
    questionaireIndex,
    questionaireId,
    questionaireCategory,
    answers,
    incrementCurrentQuestion,
    decrementCurrentQuestion,
    resetCurrentQuestion,
    incrementQuestionaireIndex,
    addAnswer,
    replaceLastAnswer,
  } = useStore();

  const navigate = useNavigate();

  useEffect(() => {
    // If answer already exists, set slider to that value
    if (answers[currentQuestion]) {
      const selectedOption = questions[currentQuestion].scale.options.find(
        (option) => option.id === answers[currentQuestion].optionId
      );
      if (selectedOption) {
        setSliderValue(parseInt(selectedOption.position) - 1);
      } else {
        setSliderValue(defaultAnswer(questions[currentQuestion]));
      }
    } else {
      setSliderValue(defaultAnswer(questions[currentQuestion]));
    }
  }, [currentQuestion, questions, answers]);

  const handleNextQuestion = async () => {
    // Add answer to answers array
    const answer = {
      questionId: questions[currentQuestion].id,
      optionId: orderOptions(questions[currentQuestion].scale.options)[
        sliderValue
      ].id,
    };
    // Check if last question
    if (currentQuestion === questions.length - 1) {
      // Check if first questionaire from movement questionaire
      if (
        questionaireCategory === QuestionCategories.Movement &&
        questionaireIndex === 0
      ) {
        addAnswer(answer, currentQuestion);
        navigate(PatientRoutes.MovementSuggestions);
      } else {
        // End of questionaire
        const coins =
          questionaireCategory === QuestionCategories.Movement ? 10 : 5;
        // TODO: send answers to backend
        const data = {
          questionnaireId: questionaireId,
          questionnaireIndex: questionaireIndex,
          answers: [...answers, answer],
        };
        console.log(data);
        setLoading(true);
        try {
          setAmount(coins);
          await addCoins(user.id, coins);
          setLoading(false);
          setShowModal(true);
        } catch (error) {
          setLoading(false);
          console.log(error);
          toast.error(
            "Er is iets misgegaan bij het opslaan van je antwoorden."
          );
        }
      }
    } else {
      // When not last question
      addAnswer(answer, currentQuestion);
      incrementCurrentQuestion();
    }
  };

  return (
    <FullHeightScreen>
      <div className={styles.container}>
        <div>
          <QuestionTopBar
            questionNumber={currentQuestion + 1}
            totalQuestions={questions.length}
            previousQuestion={decrementCurrentQuestion}
            questionaireIndex={questionaireIndex}
          />
          <h1 className={styles.question}>
            {questions[currentQuestion].content}
          </h1>
        </div>
        <div className={styles.sliderValue}>
          {capitalize(
            orderOptions(questions[currentQuestion].scale.options)[sliderValue]
              .content
          )}
        </div>
        <div>
          <Slider
            max={questions[currentQuestion].scale.options.length - 1}
            value={sliderValue}
            setValue={setSliderValue}
            minLabel={
              orderOptions(questions[currentQuestion].scale.options)[0].content
            }
            maxLabel={
              orderOptions(questions[currentQuestion].scale.options)[
                questions[currentQuestion].scale.options.length - 1
              ].content
            }
          />
          <Button
            onClick={handleNextQuestion}
            size="full"
            className={styles.button}
            disabled={loading}
          >
            {currentQuestion === questions.length - 1 ? "Verstuur" : "Volgende"}
          </Button>
        </div>
      </div>
      <RecieveCoinsModal
        showModal={showModal}
        setShowModal={setShowModal}
        amount={amount}
        linkTo={PatientRoutes.Streaks}
      />
    </FullHeightScreen>
  );
};

export default QuestionaireScreen;
