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

const formSchema = z.object({
  csv: z
    .string({ message: "Upload een CSV bestand" })
    .min(1, { message: "Upload een CSV bestand" }),
});

const AddPatientCsv = () => {
  const [loading, setLoading] = useState(false);

  const defaultValues = {
    csv: "",
  };

  const handleSubmit = (data) => {
    console.log(data);
  };

  return (
    <ScrollableScreen>
      <NavBar />
      <div className="container">
        <PageHeading backLink={SpecialistRoutes.AddPatient}>
          Patiënten toevoegen op basis van CSV
        </PageHeading>
        <p className={styles.infoText}>
          Upload een csv bestand met enkel de familienaam, voornaam en
          e-mailadres van de patiënten
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
            <FormMessage />
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
