import { useEffect, useState } from "react";
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
import styles from "./AddQuestionCsv.module.css";
import { z } from "zod";
import Papa from "papaparse";
import { validateQuestionCsv } from "../../../../core/utils/csv";
import clsx from "clsx";
import { useMutation } from "@tanstack/react-query";
import { useUser } from "../../../app/auth/AuthProvider";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";

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

const AddQuestionCsv = () => {
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);

  // const { mutateAsync } = useMutation({
  //   mutationFn: storePatientList,
  // });
  const user = useUser();
  const navigate = useNavigate();

  const defaultValues = {
    csv: "",
  };

  const handleSubmit = async ({ csv }) => {
    setError(null);
    const file = csv[0];
    Papa.parse(file, {
      complete: async function (results) {
        let data = results.data;

        try {
          data = validateQuestionCsv(data);
        } catch (error) {
          setError(error.message);
          return;
        }

        // transform data to array of objects
        const questions = data.map((question) => {
          return {
            content: question[0],
            category: question[1],
            scale: question[2],
          };
        });

        setLoading(true);
        try {
          // await mutateAsync({
          //   questions,
          //   specialistId: user.id,
          // });
          console.log(questions);
          setLoading(false);
          toast.success("Vragen toegevoegd");
          navigate(SpecialistRoutes.QuestionsOverview);
        } catch (error) {
          setLoading(false);
          console.error(error);
          toast.error("Er ging iets mis. Probeer het opnieuw.");
        }
      },
    });
  };

  return (
    <ScrollableScreen>
      <NavBar />
      <div className="container">
        <PageHeading backLink={SpecialistRoutes.AddQuestion}>
          Vragen toevoegen op basis van CSV
        </PageHeading>
        <p className={styles.infoText}>
          Upload een csv bestand met enkel de{" "}
          <strong>vraag, categorie en antwoordschaal</strong> in deze volgorde.
          Het bestand mag <strong>geen kolomnamen</strong> bevatten.
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
          <div className={`mobile-only`}>
            <Button type="submit" size="full" disabled={loading}>
              Toevoegen
            </Button>
          </div>
          <div className={clsx("desktop-only", styles.submitBtn)}>
            <Button type="submit" size="default" disabled={loading}>
              Toevoegen
            </Button>
          </div>
        </Form>
      </div>
    </ScrollableScreen>
  );
};

export default AddQuestionCsv;
