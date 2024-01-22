import { Plus } from "@phosphor-icons/react";
import Button from "../../../ui/Button/Button";
import NavBar from "../../../ui/NavBar/NavBar";
import PageHeading from "../../../ui/PageHeading/PageHeading";
import styles from "./Patients.module.css";
import Search from "../../../ui/Search/Search";
import ScrollableScreen from "../../../ui/ScrollableScreen/ScrollableScreen";
import { Link } from "react-router-dom";
import { useState } from "react";
import { SpecialistRoutes } from "../../../../core/config/routes";

const Patients = () => {
  const patientData = [
    "Janssens Dirk",
    "Vermeiren Jos",
    "Van de Velde Rita",
    "De Vos Luc",
    "Bari An",
    "Doe John",
    "Smith Jane",
    "Johnson Michael",
    "Davis Emily",
    "Wilson David",
    "Taylor Olivia",
    "Anderson Daniel",
    "Martinez Sophia",
    "Thompson Matthew",
    "Garcia Ava",
    "Brown William",
    "Thomas Isabella",
    "Lee James",
    "Clark Mia",
    "Lewis Benjamin",
    "Hall Charlotte",
    "Wright Joseph",
    "Turner Amelia",
    "Adams Henry",
  ];

  const [searchInput, setSearchInput] = useState("");

  const handleSearchChange = (event) => {
    setSearchInput(event.target.value);
  };

  const filteredPatients = patientData.filter((patient) =>
    patient.toLowerCase().includes(searchInput.toLowerCase())
  );

  const sortedPatients = filteredPatients.sort((a, b) => a.localeCompare(b));

  return (
    <ScrollableScreen>
      <NavBar />
      <div className="container">
        <PageHeading>Patiënten</PageHeading>
        <div className={styles.searchAndAdd}>
          <Search
            name="fullNameSearch"
            value={searchInput}
            onChange={handleSearchChange}
            placeholder="Zoek patiënt"
          />
          <Link to={SpecialistRoutes.AddPatient}>
            <Button>
              <Plus size={18} weight="bold" />
              Voeg patiënt toe
            </Button>
          </Link>
        </div>
        <div className={styles.patients}>
          {sortedPatients.map((patient, index) => {
            const indexMatch = patient
              .toLowerCase()
              .indexOf(searchInput.toLowerCase());
            const nameBeforeMatch = patient.slice(0, indexMatch);
            const nameMatch = patient.slice(
              indexMatch,
              indexMatch + searchInput.length
            );
            const nameAfterMatch = patient.slice(
              indexMatch + searchInput.length
            );
            return (
              <div className={styles.patient} key={index}>
                <div className={styles.patientName}>
                  {nameBeforeMatch}
                  <span className={styles.match}>{nameMatch}</span>
                  {nameAfterMatch}
                </div>
                <Link to={SpecialistRoutes.PatientDetails}>
                  <Button variant="tertiary" size="superSmall">
                    Details
                  </Button>
                </Link>
              </div>
            );
          })}
          {sortedPatients.length === 0 && (
            <div className={styles.noQuestions}>
              Geen patiënten gevonden met de naam &quot;{searchInput}&quot;
            </div>
          )}
        </div>
      </div>
    </ScrollableScreen>
  );
};

export default Patients;
