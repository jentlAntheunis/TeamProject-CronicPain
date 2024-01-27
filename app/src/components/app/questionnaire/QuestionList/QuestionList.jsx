import Button from "../../../ui/Button/Button";
import styles from "./QuestionList.module.css";
import Skeleton from "react-loading-skeleton";

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
        const indexMatch = question
          .toLowerCase()
          .indexOf(search.toLowerCase());
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
            <Button variant="tertiary" size="superSmall">
              Aanpassen
            </Button>
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

const QuestionListSkeleton = () => (
  <>
    <div className={styles.question}>
      <div className={styles.questionName}>
        <Skeleton width={100} />
      </div>
      <Skeleton width={110} height={40} borderRadius={8} />
    </div>
    <div className={styles.question}>
      <div className={styles.questionName}>
        <Skeleton width={100} />
      </div>
      <Skeleton width={110} height={40} borderRadius={8} />
    </div>
    <div className={styles.question}>
      <div className={styles.questionName}>
        <Skeleton width={100} />
      </div>
      <Skeleton width={110} height={40} borderRadius={8} />
    </div>
  </>
);

export default QuestionList;
