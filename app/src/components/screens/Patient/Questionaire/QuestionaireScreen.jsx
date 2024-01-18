import FullHeightScreen from "../../../ui/FullHeightScreen/FullHeightScreen";
import useStore from "../../../../core/hooks/useStore";
import QuestionTopBar from "../../../ui/QuestionTopBar/QuestionTopBar";
import Button from "../../../ui/Button/Button";
import styles from "./QuestionaireScreen.module.css";
import Slider from "../../../ui/Slider/Slider";
import { useEffect, useState } from "react";
import { defaultAnswer } from "../../../../core/utils/questions";
import { capitalize } from "../../../../core/utils/formatText";

const QuestionaireScreen = () => {
  // States
  const [sliderValue, setSliderValue] = useState(0);

  // Hooks
  const {
    questions,
    currentQuestion,
    incrementCurrentQuestion,
    decrementCurrentQuestion,
    addAnswer,
  } = useStore();

  useEffect(() => {
    setSliderValue(defaultAnswer(questions[currentQuestion]));
  }, [currentQuestion, questions]);

  const handleNextQuestion = (e) => {
    e.preventDefault();
    if (currentQuestion === questions.length - 1) {
      
      return;
    } else {
      incrementCurrentQuestion();
    }
  }

  return (
    <FullHeightScreen>
      <div className={styles.container}>
        <div>
          <QuestionTopBar
            questionNumber={currentQuestion + 1}
            totalQuestions={questions.length}
            previousQuestion={decrementCurrentQuestion}
          />
          <h1 className={styles.question}>
            {questions[currentQuestion].question}
          </h1>
        </div>
        <div className={styles.sliderValue}>{capitalize(questions[currentQuestion].options[sliderValue].content)}</div>
        <div>
          <Slider
            max={questions[currentQuestion].options.length - 1}
            value={sliderValue}
            setValue={setSliderValue}
            minLabel={questions[currentQuestion].options[0].content}
            maxLabel={
              questions[currentQuestion].options[
                questions[currentQuestion].options.length - 1
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
    </FullHeightScreen>
  );
};

export default QuestionaireScreen;
