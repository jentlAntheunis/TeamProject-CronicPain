import { useQuery } from "@tanstack/react-query";
import { getAllQuestions } from "../../../../core/utils/apiCalls";
import Button from "../../../ui/Button/Button";
import styles from "./QuestionList.module.css";
import { toast } from "react-toastify";

const QuestionList = ({ search, filters }) => {
  const { data, isLoading, isError } = useQuery({
    queryKey: ["questions"],
    queryFn: () => getAllQuestions(),
  });

  if (isLoading) return null;

  if (isError) {
    return toast("Er is iets misgelopen bij het ophalen van de vragen", {
      type: "error",
    });
  }

  const filteredQuestions = data.data
  .filter((question) => filters.includes(question.categoryName))
  .filter((question) =>
    question.content.toLowerCase().includes(search.toLowerCase())
  )
  .map((question) => question.content);

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

export default QuestionList;
