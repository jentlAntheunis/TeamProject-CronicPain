import { useEffect } from "react";
import styles from "./QuestionnaireList.module.css";
import { Link } from "react-router-dom";
import Button from "../../../ui/Button/Button";
import { dateToDateTimeString } from "../../../../core/utils/timeData";
import { SpecialistRoutes } from "../../../../core/config/routes";
import Skeleton from "react-loading-skeleton";

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
            <Link
              to={SpecialistRoutes.QuestionnaireDetails}
              state={{ questionnaire: questionnaire }}
            >
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

export const QuestionnaireListSkeleton = () => (
  <>
    <div className={styles.questionnaire}>
      <div>
        <div className={styles.datetime}>
          <Skeleton width={150} height={22} />
        </div>
        <div className={styles.category}>
          <Skeleton width={100} />
        </div>
      </div>
      <Skeleton width={85} height={40} borderRadius={8} />
    </div>
    <div className={styles.questionnaire}>
      <div>
        <div className={styles.datetime}>
          <Skeleton width={150} height={22} />
        </div>
        <div className={styles.category}>
          <Skeleton width={100} />
        </div>
      </div>
      <Skeleton width={85} height={40} borderRadius={8} />
    </div>
    <div className={styles.questionnaire}>
      <div>
        <div className={styles.datetime}>
          <Skeleton width={150} height={22} />
        </div>
        <div className={styles.category}>
          <Skeleton width={100} />
        </div>
      </div>
      <Skeleton width={85} height={40} borderRadius={8} />
    </div>
    <div className={styles.questionnaire}>
      <div>
        <div className={styles.datetime}>
          <Skeleton width={150} height={22} />
        </div>
        <div className={styles.category}>
          <Skeleton width={100} />
        </div>
      </div>
      <Skeleton width={85} height={40} borderRadius={8} />
    </div>
  </>
);

export default QuestionnaireList;
