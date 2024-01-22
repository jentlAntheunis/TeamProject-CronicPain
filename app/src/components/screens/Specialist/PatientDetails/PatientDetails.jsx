import { useParams } from "react-router-dom";
import ScrollableScreen from "../../../ui/ScrollableScreen/ScrollableScreen";
import NavBar from "../../../ui/NavBar/NavBar";
import PageHeading from "../../../ui/PageHeading/PageHeading";
import { SpecialistRoutes } from "../../../../core/config/routes";
import styles from "./PatientDetails.module.css";
import MovingInfluenceCard from "../../../ui/MovingInfluenceCard/MovingInfluanceCard";
import Graph from "../../../ui/Graph/Graph";

const PatientDetails = () => {
  let { id } = useParams();
  console.log(id);

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
            <Graph variant={"bar"} title={"Overzicht tijdsduur beweging voorbije week"}></Graph>
            <Graph variant={"line"} title={"Pijn ervaring voorbije maand"}></Graph>
        </div>
        <div className={styles.questionnairesContainer}>
          <div>
            <div className={styles.h5}>Vragenlijsten</div>
            <div>search</div>
            <div className={styles.questionnaires}>
              <div className={styles.questionnaire}></div>
            </div>
          </div>
        </div>
      </div>
    </ScrollableScreen>
  );
};

export default PatientDetails;
