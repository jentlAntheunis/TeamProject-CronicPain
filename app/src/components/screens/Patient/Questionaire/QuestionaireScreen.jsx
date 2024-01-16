import FullHeightScreen from "../../../ui/FullHeightScreen/FullHeightScreen";
import useStore from "../../../../core/hooks/useStore";
import QuestionTopBar from "../../../ui/QuestionTopBar/QuestionTopBar";
import Button from "../../../ui/Button/Button";
import style from "./QuestionaireScreen.module.css";

const QuestionaireScreen = () => {
  const {
    questions,
    currentQuestion,
    incrementCurrentQuestion,
    decrementCurrentQuestion,
    addAnswer,
  } = useStore();

  console.log(questions);

  return (
    <FullHeightScreen>
      <div className={style.container}>
        <div>
          <QuestionTopBar
            questionNumber={currentQuestion + 1}
            totalQuestions={questions.length}
            previousQuestion={decrementCurrentQuestion}
          />
          <h1 className={style.question}>
            {questions[currentQuestion].question}
          </h1>
        </div>
        <Button onClick={incrementCurrentQuestion} size="full">
          Volgende
        </Button>
      </div>
    </FullHeightScreen>
  );
};

export default QuestionaireScreen;
