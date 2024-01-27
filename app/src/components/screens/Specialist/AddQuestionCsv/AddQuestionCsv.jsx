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
import { useMutation, useQuery } from "@tanstack/react-query";
import { useUser } from "../../../app/auth/AuthProvider";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";
import Select from "../../../ui/Select/Select";
import { getCategories, getScales, storeQuestionList } from "../../../../core/utils/apiCalls";
import {
  DatabaseCategories,
  DatabaseScales,
} from "../../../../core/config/questionCategories";
import useTitle from "../../../../core/hooks/useTitle";

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
  categoryId: z.string().min(1, { message: "Kies een categorie" }),
  scaleId: z.string().min(1, { message: "Kies een antwoordschaal" }),
});

const AddQuestionCsv = () => {
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);

  useTitle("Vragen toevoegen");
  const user = useUser();

  const navigate = useNavigate();

  const { mutateAsync } = useMutation({
    mutationFn: storeQuestionList,
  });

  const { data: scaleData, isError: scaleError } = useQuery({
    queryKey: ["scale"],
    queryFn: () => getScales(),
  });

  const { data: categoryData, isError: categoryError } = useQuery({
    queryKey: ["category"],
    queryFn: () => getCategories(),
  });

  const defaultValues = {
    csv: "",
    categoryId: "",
    scaleId: "",
  };

  const handleSubmit = async (data) => {
    setError(null);
    const file = data.csv[0];
    Papa.parse(file, {
      complete: async function (results) {
        let csvData = results.data;

        try {
          csvData = validateQuestionCsv(csvData);
        } catch (error) {
          setError(error.message);
          return;
        }

        // transform data to array of objects
        const questions = csvData.map((question) => {
          return {
            content: question[0],
            categoryId: data.categoryId,
            scaleId: data.scaleId,
            specialistId: user.id,
          };
        });

        setLoading(true);
        try {
          console.log({ data: questions });
          await mutateAsync({ data: questions });
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

  if (!categoryData || !scaleData) return;

  if (categoryError || scaleError) {
    toast.error("Er is iets misgegaan bij het ophalen van je gegevens.");
    return null;
  }

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
            <FormItem name="categoryId">
              <FormLabel>Categorie</FormLabel>
              <FormControl>
                <Select
                  placeholder="Kies een categorie"
                  options={categoryData.data
                    .filter((category) => category.name !== "pijn")
                    .map((category) => ({
                      id: category.id,
                      name: DatabaseCategories[category.name],
                    }))}
                />
              </FormControl>
              <FormMessage />
            </FormItem>
            <FormItem name="scaleId">
              <FormLabel>Antwoordschaal</FormLabel>
              <FormControl>
                <Select
                  placeholder="Kies een antwoordschaal"
                  options={scaleData.data.map((category) => ({
                    id: category.id,
                    name: DatabaseScales[category.name],
                  }))}
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
