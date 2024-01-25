import { useUser } from "../auth/AuthProvider";
import { getPatients } from "../../../core/utils/apiCalls";
import { toast } from "react-toastify";
import { useQuery } from "@tanstack/react-query";
import { SpecialistRoutes } from "../../../core/config/routes";
import { Link } from "react-router-dom";
import Button from "../../ui/Button/Button";
import styles from "./PatientList.module.css";

const PatientList = ({ search }) => {
  const user = useUser();

  const { data, isLoading, isError } = useQuery({
    queryKey: ["patients"],
    queryFn: () => getPatients(user.id),
  });

  if (isLoading) return null;

  if (isError) {
    return toast("Er is iets misgelopen bij het ophalen van de patiënten", {
      type: "error",
    });
  }

  const filteredPatients = data.data.filter((patient) => {
    const fullName = patient.lastName + " " + patient.firstName;
    return fullName.toLowerCase().includes(search.toLowerCase());
  });

  const sortedPatients = filteredPatients.sort((a, b) => {
    const fullNameA = a.lastName + " " + a.firstName;
    const fullNameB = b.lastName + " " + b.firstName;
    return fullNameA.localeCompare(fullNameB);
  });

  return (
    <div className={styles.patients}>
      {sortedPatients.map((patient, index) => {
        const fullName = patient.lastName + " " + patient.firstName;
        const indexMatch = fullName.toLowerCase().indexOf(search.toLowerCase());
        const nameBeforeMatch = fullName.slice(0, indexMatch);
        const nameMatch = fullName.slice(
          indexMatch,
          indexMatch + search.length
        );
        const nameAfterMatch = fullName.slice(indexMatch + search.length);
        return (
          <div className={styles.patient} key={index}>
            <div className={styles.patientName}>
              {nameBeforeMatch}
              <span className={styles.match}>{nameMatch}</span>
              {nameAfterMatch}
            </div>
            <Link to={`${SpecialistRoutes.PatientsOverview}/${patient.id}`}>
              <Button variant="tertiary" size="superSmall">
                Details
              </Button>
            </Link>
          </div>
        );
      })}
      {data.data.length === 0 ? (
        <div className={styles.noQuestions}>
          U heeft nog geen patiënten toegevoegd
        </div>
      ) : (
        sortedPatients.length === 0 && (
          <div className={styles.noQuestions}>
            Geen patiënten gevonden met de naam &quot;{search}&quot;
          </div>
        )
      )}
    </div>
  );
};

export default PatientList;
