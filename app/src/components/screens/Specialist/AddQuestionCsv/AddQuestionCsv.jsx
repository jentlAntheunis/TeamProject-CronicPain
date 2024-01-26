import { useEffect, useState } from "react";
import { SpecialistRoutes } from "../../../../core/config/routes";
import {
  Form,
  FormControl,
  FormItem,
  FormLabel,
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
import Input from "../../../ui/Input/Input";
import Select from "../../../ui/Select/Select";

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
  category: z.string().min(1, { message: "Kies een categorie" }),
  scale: z.string().min(1, { message: "Kies een antwoordschaal" }),
});

const AddQuestionCsv = () => {
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);

  // const { mutateAsync } = useMutation({
  //   mutationFn: storePatientList, TODO
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
          };
        });

        setLoading(true);
        try {
          console.log({
            questions,
            categoryId: 1,
            scaleId: 1,
            specialistId: user.id,
          });
          // await mutateAsync({
          //   questions,
          //   categoryId: TODO,
          //   scaleId: TODO,
          //   specialistId: user.id,
          // });
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
          Upload een csv bestand met enkel de <strong>vragen</strong>. Het
          bestand mag <strong>geen kolomnaam</strong> bevatten.
        </p>
        <Form
          schema={formSchema}
          defaultValues={defaultValues}
          onSubmit={handleSubmit}
          className={styles.formContainer}
        >
          <div className={styles.formItems}>
            <FormItem name="csv">
              <FormControl>
                <FileInput />
              </FormControl>
              <FormMessage>{error}</FormMessage>
            </FormItem>
            <FormItem name="category">
              <FormLabel>Categorie</FormLabel>
              <FormControl>
                <Select
                  placeholder="Kies een categorie"
                  options={["Bewegingsvragen", "Bonusvragen"]}
                />
              </FormControl>
              <FormMessage />
            </FormItem>
            <FormItem name="scale">
              <FormLabel>Antwoordschaal</FormLabel>
              <FormControl>
                <Select
                  placeholder="Kies een antwoordschaal"
                  options={["oneens -> eens", "niet -> altijd"]}
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          </div>
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
