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

const Patients = () => {
  const [searchInput, setSearchInput] = useState("");

  const handleSearchChange = (event) => {
    setSearchInput(event.target.value);
  };

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
        <PatientList search={searchInput} />
      </div>
    </ScrollableScreen>
  );
};

export default Patients;
