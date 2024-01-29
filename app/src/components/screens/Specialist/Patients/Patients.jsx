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
import PatientList from "../../../app/patients/PatientList";
import useTitle from "../../../../core/hooks/useTitle";

const Patients = () => {
  const [searchInput, setSearchInput] = useState("");

  useTitle("Patiënten");

  const handleSearchChange = (event) => {
    setSearchInput(event.target.value);
  };

  return (
    <ScrollableScreen>
      <NavBar />
      <div className="container">
        <div className="desktop-only">
        <PageHeading>Patiënten</PageHeading>
        </div>
        <div className={styles.searchAndAdd}>
          <Search
            name="fullNameSearch"
            value={searchInput}
            onChange={handleSearchChange}
            placeholder="Zoek patiënt"
          />
          <div className="desktop-only">
            <Link
              to={SpecialistRoutes.AddPatient}
              className={styles.addPatient}
            >
              <Button>
                <Plus size={18} weight="bold" />
                Voeg patiënt toe
              </Button>
            </Link>
          </div>
        </div>
        <PatientList search={searchInput} />
      </div>
      <div className={`mobile-only ${styles.addPatientMobile}`}>
        <Link to={SpecialistRoutes.AddPatient}>
          <Button className={styles.addPatient}>
            <Plus size={18} weight="bold" />
            Voeg patiënt toe
          </Button>
        </Link>
      </div>
    </ScrollableScreen>
  );
};

export default Patients;
