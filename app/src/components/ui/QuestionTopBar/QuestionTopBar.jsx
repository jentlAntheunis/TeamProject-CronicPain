import { ArrowLeft } from "@phosphor-icons/react";
import styles from "./QuestionTopBar.module.css";
import ProgressBar from "../ProgressBar/ProgressBar";
import { useNavigate } from "react-router-dom";
import { PatientRoutes } from "../../../core/config/routes";

const QuestionTopBar = ({
  questionNumber,
  totalQuestions,
  previousQuestion,
  questionaireIndex = 0,
}) => {
  const navigate = useNavigate();

  const handleBack = () => {
    // if first question, go to previous screen, else go to previous question
    if (questionNumber === 1) {
      if (questionaireIndex === 0) {
        navigate(PatientRoutes.Dashboard);
      } else {
        navigate(PatientRoutes.WellDone);
      }
    } else {
      previousQuestion();
    }
  };

  return (
    <div className={styles.container}>
      <button className="btn-reset" onClick={handleBack}>
        <ArrowLeft size={32} display="block" />
      </button>
      <ProgressBar progress={(questionNumber / totalQuestions) * 100} />
      <p className={styles.questionCount}>
        {questionNumber}/{totalQuestions}
      </p>
    </div>
  );
};

export default QuestionTopBar;
