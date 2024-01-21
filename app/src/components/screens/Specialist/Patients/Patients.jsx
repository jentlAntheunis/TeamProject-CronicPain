import { Plus } from "@phosphor-icons/react";
import Button from "../../../ui/Button/Button";
import FullHeightScreen from "../../../ui/FullHeightScreen/FullHeightScreen";
import NavBar from "../../../ui/NavBar/NavBar";
import PageHeading from "../../../ui/PageHeading/PageHeading";
import styles from "./Patients.module.css";

const Patients = () => {
  const patientData = [
    { name: "Dirk Janssens" },
    { name: "Jos Vermeiren" },
    { name: "Rita Van de Velde" },
    { name: "Luc De Vos" },
    { name: "An Bari" },
  ];

  return (
    <FullHeightScreen className={`margins-desktop ${styles.screen}`}>
      <NavBar />
      <div className="container">
        <PageHeading>Patiënten</PageHeading>
        <div className={styles.searchAndAdd}>
          <div>Searchfield</div>
          <Button>
            <Plus size={18} weight="bold" />
            Voeg patiënt toe
          </Button>
        </div>
        <div className={styles.patients}>
          {patientData.map((patient, index) => (
            <div className={styles.patient} key={index}>
              <div className={styles.patientName}>{patient.name}</div>
              <Button variant="tertiary" size="superSmall">
                Details
              </Button>
            </div>
          ))}
        </div>
      </div>
    </FullHeightScreen>
  );
};

export default Patients;
