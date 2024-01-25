import { useEffect } from "react";
import styles from "./QuestionnaireList.module.css";
import { Link } from "react-router-dom";
import Button from "../../../ui/Button/Button";
import { dateToDateTimeString } from "../../../../core/utils/timeData";
import { SpecialistRoutes } from "../../../../core/config/routes";
import { DatabaseCategories } from "../../../../core/config/questionCategories";

const QuestionnaireList = ({ questionnaires, date }) => {
  const filteredQuestionnaires = questionnaires.filter((questionnaire) => {
    if (!date) return true;
    if (!questionnaire.date) return false;
    return (
      new Date(questionnaire.date).toLocaleDateString("nl-BE") ===
      date.toLocaleDateString("nl-BE")
    );
  });

  return (
    <>
      {filteredQuestionnaires.length ? (
        filteredQuestionnaires.map((questionnaire, index) => (
          <div key={index} className={styles.questionnaire}>
            <div>
              <div className={styles.datetime}>
                {dateToDateTimeString(new Date(questionnaire.date))}
              </div>
              <div className={styles.category}>
                {questionnaire.categoryName && DatabaseCategories[questionnaire.categoryName]}
              </div>
            </div>
            {/* TODO: changeroute */}
            <Link to={SpecialistRoutes.QuestionnaireDetails} state={{ questionnaire: questionnaire }}>
              <Button variant="tertiary" size="superSmall">
                Details
              </Button>
            </Link>
          </div>
        ))
      ) : (
        <div className={styles.noQuestionnaires}>
          Geen vragenlijsten gevonden op {date.toLocaleDateString("nl-BE")}
        </div>
      )}
    </>
  );
};

export default QuestionnaireList;
