import { useQuery } from "@tanstack/react-query";
import { getAllQuestions } from "../../../../core/utils/apiCalls";
import Button from "../../../ui/Button/Button";
import styles from "./QuestionList.module.css";
import Skeleton from "react-loading-skeleton";
import { toast } from "react-toastify";
import { DatabaseCategoriesSingular } from "../../../../core/config/questionCategories";
import { Link } from "react-router-dom";
import { SpecialistRoutes } from "../../../../core/config/routes";

const QuestionList = ({ search, filters }) => {
  const { data, isLoading, isError } = useQuery({
    queryKey: ["questions"],
    queryFn: () => getAllQuestions(),
  });

  if (isLoading) {
    if (filters.length === 0) {
      return (
        <div className={styles.noQuestions}>
          Selecteer een filter om vragen te zien
        </div>
      );
    } else {
      return <QuestionListSkeleton />;
    }
  }

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
    .map((question) => question);

  return (
    <>
      {filteredQuestions.map((question, index) => {
        const indexMatch = question.content
          .toLowerCase()
          .indexOf(search.toLowerCase());
        const nameBeforeMatch = question.content.slice(0, indexMatch);
        const nameMatch = question.content.slice(
          indexMatch,
          indexMatch + search.length
        );
        const nameAfterMatch = question.content.slice(
          indexMatch + search.length
        );
        return (
          <div className={styles.question} key={index}>
            <div>
              <div className={styles.questionName}>
                {nameBeforeMatch}
                <span className={styles.match}>{nameMatch}</span>
                {nameAfterMatch}
              </div>
              <div className={styles.category}>
                {question.categoryName &&
                  DatabaseCategoriesSingular[question.categoryName]}
              </div>
            </div>
            <Link to={SpecialistRoutes.AddQuestion} state={question}>
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

const QuestionListSkeleton = () => (
  <>
    <div className={styles.question}>
      <div>
        <div className={styles.questionName}>
          <Skeleton width={400} height={30} />
        </div>
        <div className={styles.category}>
          <Skeleton width={125} />
        </div>
      </div>
      <Skeleton width={110} height={40} borderRadius={8} />
    </div>
    <div className={styles.question}>
      <div>
        <div className={styles.questionName}>
          <Skeleton width={400} height={30} />
        </div>
        <div className={styles.category}>
          <Skeleton width={125} />
        </div>
      </div>
      <Skeleton width={110} height={40} borderRadius={8} />
    </div>
    <div className={styles.question}>
      <div>
        <div className={styles.questionName}>
          <Skeleton width={400} height={30} />
        </div>
        <div className={styles.category}>
          <Skeleton width={125} />
        </div>
      </div>
      <Skeleton width={110} height={40} borderRadius={8} />
    </div>
    <div className={styles.question}>
      <div>
        <div className={styles.questionName}>
          <Skeleton width={400} height={30} />
        </div>
        <div className={styles.category}>
          <Skeleton width={125} />
        </div>
      </div>
      <Skeleton width={110} height={40} borderRadius={8} />
    </div>
  </>
);

export default QuestionList;
