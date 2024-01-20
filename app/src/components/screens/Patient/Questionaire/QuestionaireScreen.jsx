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

const QuestionaireScreen = () => {
  // States
  const [sliderValue, setSliderValue] = useState(0);
  const [showModal, setShowModal] = useState(false);

  // Hooks
  const {
    questions,
    currentQuestion,
    questionaireIndex,
    questionaireId,
    incrementCurrentQuestion,
    decrementCurrentQuestion,
    resetCurrentQuestion,
    incrementQuestionaireIndex,
    addAnswer,
  } = useStore();

  const navigate = useNavigate();

  useEffect(() => {
    setSliderValue(defaultAnswer(questions[currentQuestion]));
  }, [currentQuestion, questions]);

  const handleNextQuestion = (e) => {
    e.preventDefault();
    console.log(questionaireIndex);
    if (currentQuestion === questions.length - 1) {
      if (questionaireIndex === 0) {
        navigate(PatientRoutes.MovementSuggestions);
      } else {
        setShowModal(true);
      }
    } else {
      addAnswer({
        questionId: questions[currentQuestion].id,
        optionId: orderOptions(questions[currentQuestion].scale.options)[sliderValue].id,
        questionaireId: questionaireId,
      })
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
          >
            {currentQuestion === questions.length - 1 ? "Verstuur" : "Volgende"}
          </Button>
        </div>
      </div>
      <RecieveCoinsModal
        showModal={showModal}
        setShowModal={setShowModal}
        amount={10}
        linkTo={PatientRoutes.Streaks}
      />
    </FullHeightScreen>
  );
};

export default QuestionaireScreen;
