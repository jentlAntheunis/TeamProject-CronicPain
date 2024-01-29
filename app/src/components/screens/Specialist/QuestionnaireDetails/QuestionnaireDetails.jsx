import { Navigate, useLocation } from "react-router-dom";
import { SpecialistRoutes } from "../../../../core/config/routes";
import NavBar from "../../../ui/NavBar/NavBar";
import PageHeading from "../../../ui/PageHeading/PageHeading";
import ScrollableScreen from "../../../ui/ScrollableScreen/ScrollableScreen";
import styles from "./QuestionnaireDetails.module.css";
import { dateToDateTimeString } from "../../../../core/utils/timeData";
import { DatabaseCategories } from "../../../../core/config/questionCategories";
import useTitle from "../../../../core/hooks/useTitle";

const QuestionnaireDetails = () => {
  const { state } = useLocation();

  useTitle("Details vragenlijst");

  if (!state) return <Navigate to={SpecialistRoutes.PatientsOverview} />;

  return (
    <ScrollableScreen>
      <div className="desktop-only">
        <NavBar />
      </div>
      <div className="container">
        <PageHeading
          backLink={`${SpecialistRoutes.PatientsOverview}/${state.questionnaire.patientId}`}
        >
          {dateToDateTimeString(new Date(state.questionnaire.date))}
        </PageHeading>
        <h2 className={styles.category}>
          {state.questionnaire.categoryName &&
            DatabaseCategories[state.questionnaire.categoryName]}
        </h2>
        {/* Questions */}
        <div className={styles.questionContainer}>
          {state.questionnaire.questions.map((question, index) => (
            <div className={styles.question} key={index}>
              <p className={styles.questionNumber}>Vraag {index + 1}</p>
              <p className={styles.questionText}>{question.content}</p>
              <div className={styles.answers}>
                {question.answers.length > 1 ? (
                  <>
                    <div>
                      <p className={styles.answerLabel}>Antwoord voor:</p>
                      <p className={styles.answerText}>
                        {
                          question.answers.find(
                            (answer) => answer.questionnaireIndex === 0
                          ).optionContent
                        }
                      </p>
                    </div>
                    <div>
                      <p className={styles.answerLabel}>Antwoord na:</p>
                      <p className={styles.answerText}>
                        {
                          question.answers.find(
                            (answer) => answer.questionnaireIndex === 1
                          ).optionContent
                        }
                      </p>
                    </div>
                  </>
                ) : (
                  <div>
                    <p className={styles.answerLabel}>Antwoord:</p>
                    <p className={styles.answerText}>
                      {question.answers[0].optionContent}
                    </p>
                  </div>
                )}
              </div>
            </div>
          ))}
        </div>
      </div>
    </ScrollableScreen>
  );
};

export default QuestionnaireDetails;
