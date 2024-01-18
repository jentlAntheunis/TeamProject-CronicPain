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
  const {
    questions,
    currentQuestion,
    incrementCurrentQuestion,
    decrementCurrentQuestion,
    addAnswer,
  } = useStore();

  useEffect(() => {
    console.log(defaultAnswer(questions[currentQuestion]));
    setSliderValue(defaultAnswer(questions[currentQuestion]));
    console.log(questions);
  }, [currentQuestion]);

  const [sliderValue, setSliderValue] = useState(0);

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
            onClick={incrementCurrentQuestion}
            size="full"
            className={styles.button}
          >
            Volgende
          </Button>
        </div>
      </div>
    </FullHeightScreen>
  );
};

export default QuestionaireScreen;
