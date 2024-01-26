import Button from "../../../ui/Button/Button";
import styles from "./QuestionList.module.css";
import { Link } from "react-router-dom";
import { SpecialistRoutes } from "../../../../core/config/routes";

const QuestionList = ({ questions, search, filters }) => {
  const filteredQuestions = questions
    .filter((question) => filters.includes(question.category))
    .filter((question) =>
      question.question.toLowerCase().includes(search.toLowerCase())
    )
    .map((question) => question.question);

  return (
    <>
      {filteredQuestions.map((question, index) => {
        const indexMatch = question.toLowerCase().indexOf(search.toLowerCase());
        const nameBeforeMatch = question.slice(0, indexMatch);
        const nameMatch = question.slice(
          indexMatch,
          indexMatch + search.length
        );
        const nameAfterMatch = question.slice(indexMatch + search.length);
        return (
          <div className={styles.question} key={index}>
            <div className={styles.questionName}>
              {nameBeforeMatch}
              <span className={styles.match}>{nameMatch}</span>
              {nameAfterMatch}
            </div>
            <Link to={SpecialistRoutes.AddQuestion} state={{question: question}}>
              <Button variant="tertiary" size="superSmall">
                Aanpassen
              </Button>
            </Link>
          </div>
        );
      })}
      {filters.length === 0 ? (
        <div className={styles.noQuestions}>
          Selecteer een filter om vragen te zien
        </div>
      ) : (
        filteredQuestions.length === 0 && (
          <div className={styles.noQuestions}>
            Geen vragen gevonden met deze zoekterm
          </div>
        )
      )}
    </>
  );
};

export default QuestionList;
