import { Plus } from "@phosphor-icons/react";
import Button from "../../../ui/Button/Button";
import NavBar from "../../../ui/NavBar/NavBar";
import PageHeading from "../../../ui/PageHeading/PageHeading";
import styles from "./Patients.module.css";
import Search from "../../../ui/Search/Search";
import ScrollableScreen from "../../../ui/ScrollableScreen/ScrollableScreen";
import { Link } from "react-router-dom";
import { useState } from "react";

const Patients = () => {
  const patientData = [
    { name: "Janssens Dirk" },
    { name: "Vermeiren Jos" },
    { name: "Van de Velde Rita" },
    { name: "De Vos Luc" },
    { name: "Bari An" },
    { name: "Doe John" },
    { name: "Smith Jane" },
    { name: "Johnson Michael" },
    { name: "Davis Emily" },
    { name: "Wilson David" },
    { name: "Taylor Olivia" },
    { name: "Anderson Daniel" },
    { name: "Martinez Sophia" },
    { name: "Thompson Matthew" },
    { name: "Garcia Ava" },
    { name: "Brown William" },
    { name: "Thomas Isabella" },
    { name: "Lee James" },
    { name: "Clark Mia" },
    { name: "Lewis Benjamin" },
    { name: "Hall Charlotte" },
    { name: "Wright Joseph" },
    { name: "Turner Amelia" },
    { name: "Adams Henry" },
  ];

  const [searchInput, setSearchInput] = useState('');

  const handleSearchChange = (event) => {
    console.log("change");
    setSearchInput(event.target.value);
  };

  const filteredPatients = patientData.filter(patient =>
    patient.name.toLowerCase().includes(searchInput.toLowerCase())
  );

  const sortedPatients = filteredPatients.sort((a, b) => a.name.localeCompare(b.name));

  return (
    <ScrollableScreen>
      <NavBar />
      <div className="container">
        <PageHeading>Patiënten</PageHeading>
        <div className={styles.searchAndAdd}>
          <Search name="fullNameSearch" value={searchInput} onChange={handleSearchChange} />
          <Link to="/add-patient">
            <Button>
              <Plus size={18} weight="bold" />
              Voeg patiënt toe
            </Button>
          </Link>
        </div>
        <div className={styles.patients}>
          {sortedPatients.map((patient, index) => (
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
