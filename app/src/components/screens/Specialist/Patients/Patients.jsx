import { Plus } from "@phosphor-icons/react";
import Button from "../../../ui/Button/Button";
import NavBar from "../../../ui/NavBar/NavBar";
import PageHeading from "../../../ui/PageHeading/PageHeading";
import styles from "./Patients.module.css";
import Search from "../../../ui/Search/Search";
import ScrollableScreen from "../../../ui/ScrollableScreen/ScrollableScreen";

const Patients = () => {
  const patientData = [
    { name: "Dirk Janssens" },
    { name: "Jos Vermeiren" },
    { name: "Rita Van de Velde" },
    { name: "Luc De Vos" },
    { name: "An Bari" },
    { name: "John Doe" },
    { name: "Jane Smith" },
    { name: "Michael Johnson" },
    { name: "Emily Davis" },
    { name: "David Wilson" },
    { name: "Olivia Taylor" },
    { name: "Daniel Anderson" },
    { name: "Sophia Martinez" },
    { name: "Matthew Thompson" },
    { name: "Ava Garcia" },
    { name: "William Brown" },
    { name: "Isabella Thomas" },
    { name: "James Lee" },
    { name: "Mia Clark" },
    { name: "Benjamin Lewis" },
    { name: "Charlotte Hall" },
    { name: "Joseph Wright" },
    { name: "Amelia Turner" },
    { name: "Henry Adams" },
  ];

  return (
    <ScrollableScreen>
      <NavBar />
      <div className="container">
        <PageHeading>Patiënten</PageHeading>
        <div className={styles.searchAndAdd}>
          <Search />
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
    </ScrollableScreen>
  );
};

export default Patients;
