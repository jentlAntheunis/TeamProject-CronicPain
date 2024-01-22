import { Link, useParams } from "react-router-dom";
import ScrollableScreen from "../../../ui/ScrollableScreen/ScrollableScreen";
import NavBar from "../../../ui/NavBar/NavBar";
import PageHeading from "../../../ui/PageHeading/PageHeading";
import { SpecialistRoutes } from "../../../../core/config/routes";
import styles from "./PatientDetails.module.css";
import MovingInfluenceCard from "../../../ui/MovingInfluenceCard/MovingInfluanceCard";
import Graph from "../../../ui/Graph/Graph";
import Button from "../../../ui/Button/Button";

const PatientDetails = () => {
  let { id } = useParams();

  const questionnaires = [
    {
      category: "bewegingsvragen",
      datetime: "03/01/2024 14:03",
    },
    {
      category: "bonusvragen",
      datetime: "04/01/2024 15:12",
    },
    {
      category: "bewegingsvragen",
      datetime: "04/01/2024 10:45",
    },
    {
      category: "bewegingsvragen",
      datetime: "04/01/2024 09:27",
    },
    {
      category: "bonusvragen",
      datetime: "05/01/2024 16:58",
    },
    {
      category: "bewegingsvragen",
      datetime: "05/01/2024 11:30",
    },
    {
      category: "bewegingsvragen",
      datetime: "05/01/2024 13:15",
    },
    {
      category: "bewegingsvragen",
      datetime: "05/01/2024 08:59",
    },
    {
      category: "bonusvragen",
      datetime: "05/01/2024 17:42",
    },
    {
      category: "bonusvragen",
      datetime: "06/01/2024 12:20",
    },
  ];

  const sortedQuestionnaires = [...questionnaires].sort((a, b) => {
    const dateA = new Date(a.datetime);
    const dateB = new Date(b.datetime);
    return dateA - dateB;
  });

  return (
    <ScrollableScreen>
      <NavBar />
      <div className="container">
        <PageHeading backLink={SpecialistRoutes.PatientsOverview}>
          {id}
        </PageHeading>
        <div className={styles.movingInfluenceContainer}>
          <div className={styles.h5}>Invloed van bewegen</div>
          <div className={styles.movingInfluenceCardsContainer}>
            <MovingInfluenceCard variant={"positive"} />
            <MovingInfluenceCard variant={"neutral"} />
            <MovingInfluenceCard variant={"negative"} />
          </div>
        </div>
        <div className={styles.graphs}>
          <Graph
            variant={"bar"}
            title={"Overzicht tijdsduur beweging voorbije week"}
          ></Graph>
          <Graph
            variant={"line"}
            title={"Pijn ervaring voorbije maand"}
          ></Graph>
        </div>
        <div className={styles.questionnairesContainer}>
          <div className={styles.titleAndSearchContainer}>
            <div className={styles.h5}>Vragenlijsten</div>
            <input
              type="date"
              name="date"
              id="date"
              className={styles.searchOnDate}
            />
          </div>
          <div className={styles.questionnaires}>
            {sortedQuestionnaires.map((questionnaire, index) => (
              <div key={index} className={styles.questionnaire}>
                <div>
                  <div className={styles.datetime}>{questionnaire.datetime}</div>
                  <div className={styles.category}>{questionnaire.category}</div>
                </div>
                <Link to={"/nogintevullen"}>
                  <Button variant="tertiary" size="superSmall">
                    Details
                  </Button>
                </Link>
              </div>
            ))}
          </div>
        </div>
      </div>
    </ScrollableScreen>
  );
};

export default PatientDetails;
