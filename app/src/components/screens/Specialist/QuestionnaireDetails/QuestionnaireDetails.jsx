import { Navigate, useLocation } from "react-router-dom";
import { SpecialistRoutes } from "../../../../core/config/routes";
import NavBar from "../../../ui/NavBar/NavBar";
import PageHeading from "../../../ui/PageHeading/PageHeading";
import ScrollableScreen from "../../../ui/ScrollableScreen/ScrollableScreen";
import styles from "./QuestionnaireDetails.module.css";

const questions = [
  {
    question: "Hoe lang ervaart u al chronische pijn?",
    answers: ["Minder dan 6 maanden", "6 maanden tot 1 jaar"],
  },
  {
    question: "Welke behandelingen heeft u al geprobeerd?",
    answers: ["Medicatie", "Fysiotherapie"],
  },
  {
    question: "Hoe beïnvloedt chronische pijn uw dagelijks leven?",
    answers: ["Ik kan niet meer werken", "Ik kan niet meer sporten"],
  },
  {
    question: "Heeft u last van slaapproblemen door de pijn?",
    answers: ["Ja", "Nee"],
  },
  {
    question: "Welke medicijnen gebruikt u voor de pijnbestrijding?",
    answers: ["Paracetamol", "Ibuprofen"],
  },
  {
    question: "Heeft u al alternatieve therapieën geprobeerd?",
    answers: ["Acupunctuur", "Massage"],
  },
  {
    question: "Hoe gaat u om met de emotionele impact van chronische pijn?",
    answers: [
      "Ik heb last van depressieve gevoelens",
      "Ik heb last van angstgevoelens",
    ],
  },
  {
    question:
      "Heeft u ondersteuning van een zorgverlener voor uw pijnmanagement?",
    answers: ["Ja", "Nee"],
  },
  {
    question: "Wat zijn uw belangrijkste triggers voor pijnverergering?",
    answers: ["Stress", "Beweging"],
  },
  {
    question: "Hoe beïnvloedt chronische pijn uw werk of studie?",
    answers: ["Ik kan niet meer werken", "Ik kan niet meer studeren"],
  },
  {
    question: "Heeft u ervaring met mindfulness of ontspanningstechnieken?",
    answers: ["Ja", "Nee"],
  },
  {
    question: "Welke hulpmiddelen gebruikt u om met de pijn om te gaan?",
    answers: ["Pijnmedicatie", "Ontspanningsoefeningen"],
  },
  {
    question: "Heeft u last van sociale isolatie door de pijn?",
    answers: ["Ja", "Nee"],
  },
  {
    question:
      "Hoe beïnvloedt chronische pijn uw stemming en mentale gezondheid?",
    answers: [
      "Ik heb last van depressieve gevoelens",
      "Ik heb last van angstgevoelens",
    ],
  },
  {
    question: "Heeft u advies voor andere chronische pijnpatiënten?",
    answers: ["Blijf bewegen", "Zoek hulp"],
  },
];

const QuestionnaireDetails = () => {
  const { state } = useLocation();

  if (!state) return <Navigate to={SpecialistRoutes.PatientsOverview} />;

  return (
    <ScrollableScreen>
      <NavBar />
      <div className="container">
        {/* TODO: change backLink  */}
        <PageHeading backLink={SpecialistRoutes.PatientsOverview}>
          {/* TODO: dynamically set title */}
          03/01/2024 14:03
        </PageHeading>
        {/* TODO: dynamically set category */}
        <h2 className={styles.category}>Bewegingsvragen</h2>
        {/* Questions */}
        <div className={styles.questionContainer}>
          {questions.map((question, index) => (
            <div className={styles.question} key={index}>
              <p className={styles.questionNumber}>Vraag {index + 1}</p>
              <p className={styles.questionText}>{question.question}</p>
              <div className={styles.answers}>
                {question.answers.length > 1 ? (
                  <>
                    <div>
                      <p className={styles.answerLabel}>Antwoord voor:</p>
                      <p className={styles.answerText}>{question.answers[0]}</p>
                    </div>
                    <div>
                      <p className={styles.answerLabel}>Antwoord na:</p>
                      <p className={styles.answerText}>{question.answers[1]}</p>
                    </div>
                  </>
                ) : (
                  <div>
                    <p className={styles.answerLabel}>Antwoord:</p>
                    <p className={styles.answerText}>{question.answers[0]}</p>
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
