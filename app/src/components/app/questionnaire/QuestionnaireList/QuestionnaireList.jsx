import { useEffect } from "react";
import styles from "./QuestionnaireList.module.css";
import { Link } from "react-router-dom";
import Button from "../../../ui/Button/Button";
import { dateToDateTimeString } from "../../../../core/utils/timeData";

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
              <div className={styles.category}>{questionnaire.category}</div>
            </div>
            {/* TODO: changeroute */}
            <Link to={"/nogintevullen"}>
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
