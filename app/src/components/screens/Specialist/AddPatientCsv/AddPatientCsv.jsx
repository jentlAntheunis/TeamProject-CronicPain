import { useState } from "react";
import { SpecialistRoutes } from "../../../../core/config/routes";
import {
  Form,
  FormControl,
  FormItem,
  FormMessage,
} from "../../../app/form/Form";
import Button from "../../../ui/Button/Button";
import FileInput from "../../../ui/FileInput/FileInput";
import NavBar from "../../../ui/NavBar/NavBar";
import PageHeading from "../../../ui/PageHeading/PageHeading";
import ScrollableScreen from "../../../ui/ScrollableScreen/ScrollableScreen";
import styles from "./AddPatientCsv.module.css";
import { z } from "zod";
import Papa from "papaparse";
import { validatePatientCsv } from "../../../../core/utils/csv";

const formSchema = z.object({
  csv: z
    .any()
    .refine((data) => {
      // check if any file is uploaded
      if (data.length === 0) {
        return false;
      }
      return true;
    }, "Geen bestand geüpload")
    .refine((data) => {
      // check if file is csv
      const file = data[0];
      if (!file) {
        return false;
      }
      const allowedExtensions = [".csv"];
      const fileExtension = file.name.substring(file.name.lastIndexOf("."));
      return allowedExtensions.includes(fileExtension);
    }, "Geen geldig bestandstype"),
});

const AddPatientCsv = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const defaultValues = {
    csv: "",
  };

  const handleSubmit = ({ csv }) => {
    setLoading(true);
    setError(null);
    const file = csv[0];
    Papa.parse(file, {
      complete: function (results) {
        console.log(results);
        const data = results.data;

        try {
          validatePatientCsv(data);
        } catch (error) {
          setError(error.message);
          setLoading(false);
          return;
        }

        // transform data to array of objects
        const patients = data.map((patient) => {
          return {
            lastName: patient[0],
            firstName: patient[1],
            email: patient[2],
          };
        });

        // TODO: send to backend
        console.log(patients);
        setLoading(false);
      },
    });
  };

  return (
    <ScrollableScreen>
      <NavBar />
      <div className="container">
        <PageHeading backLink={SpecialistRoutes.AddPatient}>
          Patiënten toevoegen op basis van CSV
        </PageHeading>
        <p className={styles.infoText}>
          Upload een csv bestand met enkel de{" "}
          <strong>familienaam, voornaam en e-mailadres</strong> van de patiënten
          in deze volgorde. Het bestand mag <strong>geen kolomnamen</strong>{" "}
          bevatten.
        </p>
        <Form
          schema={formSchema}
          defaultValues={defaultValues}
          onSubmit={handleSubmit}
        >
          <FormItem name="csv">
            <FormControl>
              <FileInput />
            </FormControl>
            <FormMessage>{error}</FormMessage>
          </FormItem>
          <div className={`mobile-only ${styles.removePadding}`}>
            <Button type="submit" size="full" disabled={loading}>
              Toevoegen
            </Button>
          </div>
          <div className={`desktop-only ${styles.removePadding}`}>
            <Button type="submit" size="default" disabled={loading}>
              Toevoegen
            </Button>
          </div>
        </Form>
      </div>
    </ScrollableScreen>
  );
};

export default AddPatientCsv;
