import { z } from "zod";
import ScrollableScreen from "../../../ui/ScrollableScreen/ScrollableScreen";
import NavBar from "../../../ui/NavBar/NavBar";
import PageHeading from "../../../ui/PageHeading/PageHeading";
import { SpecialistRoutes } from "../../../../core/config/routes";
import Button from "../../../ui/Button/Button";
import styles from "./AddQuestion.module.css";
import {
  Form,
  FormControl,
  FormItem,
  FormLabel,
  FormMessage,
} from "../../../app/form/Form";
import Input from "../../../ui/Input/Input";
import { useEffect, useRef, useState } from "react";
import Select from "../../../ui/Select/Select";
import { Link, useLocation } from "react-router-dom";
import {
  addQuestion,
  editQuestion,
  getCategories,
  getScales,
} from "../../../../core/utils/apiCalls";
import { useMutation, useQuery } from "@tanstack/react-query";
import { toast } from "react-toastify";
import {
  DatabaseCategories,
  DatabaseScales,
} from "../../../../core/config/questionCategories";
import { useUser } from "../../../app/auth/AuthProvider";
import { useNavigate } from "react-router-dom";
import useTitle from "../../../../core/hooks/useTitle";
import { useFormContext } from "react-hook-form";

const formSchema = z.object({
  content: z.string().min(5, { message: "Vraag is te kort" }),
  categoryId: z.string().min(1, { message: "Kies een categorie" }),
  scaleId: z.string().min(1, { message: "Kies een antwoordschaal" }),
});

const AddQuestion = () => {
  const [loading, setLoading] = useState(false);
  const [formValues, setFormValues] = useState({});

  const { state } = useLocation();
  const user = useUser();
  const navigate = useNavigate();

  let title = "Vraag toevoegen";
  if (state) {
    title = "Vraag aanpassen";
  }
  useTitle(title);

  const addMutation = useMutation({
    mutationFn: addQuestion,
  });

  const editMutation = useMutation({
    mutationFn: editQuestion,
  });

  const { data: scaleData, isError: scaleError } = useQuery({
    queryKey: ["scale"],
    queryFn: () => getScales(),
    refetchOnWindowFocus: false,
  });

  const { data: categoryData, isError: categoryError } = useQuery({
    queryKey: ["category"],
    queryFn: () => getCategories(),
    refetchOnWindowFocus: false,
  });

  let defaultValues = {
    content: "",
    categoryId: "",
    scaleId: "",
  };

  if (state) {
    defaultValues = {
      content: state.content,
      categoryId: state.categoryId,
      scaleId: state.scaleId,
    };
  }

  const handleSubmit = async (data) => {
    setLoading(true);
    try {
      if (state) {
        await editMutation.mutateAsync({data: data, questionId: state.id});
        toast.success("Vraag aangepast");
      } else {
        const newData = {
          ...data,
          specialistId: user.id,
        };
        await addMutation.mutateAsync(newData);
        toast.success("Vraag toegevoegd");
      }
      setLoading(false);
      navigate(SpecialistRoutes.QuestionsOverview);
    } catch (error) {
      console.log(error);
      toast.error("Er ging iets mis. Probeer het opnieuw.");
      setLoading(false);
    }
  };

  if (!categoryData || !scaleData) return;

  if (categoryError || scaleError) {
    toast.error("Er is iets misgegaan bij het ophalen van je gegevens.");
    return null;
  }

  return (
    <ScrollableScreen className={styles.screenContainer}>
      <NavBar />
      <div className="container">
        <div className={styles.header}>
          <PageHeading backLink={SpecialistRoutes.QuestionsOverview}>
            {state ? "Vraag aanpassen" : "Vraag toevoegen"}
          </PageHeading>
          {!state && (
            <div className="desktop-only">
              <Link to={SpecialistRoutes.AddQuestionCsv}>
                <Button variant="secondary" className={styles.csvImport}>
                  CSV importeren
                </Button>
              </Link>
            </div>
          )}
        </div>
        <Form
          schema={formSchema}
          defaultValues={defaultValues}
          onSubmit={handleSubmit}
          className={styles.formContainer}
        >
          <FormWatcher onValuesChange={setFormValues} />
          <div className={styles.formItems}>
            <FormItem name="content">
              <FormLabel>Vraag</FormLabel>
              <FormControl>
                <Input placeholder="Vul een vraag in" />
              </FormControl>
              <FormMessage />
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
          <div className={`mobile-only ${styles.removePadding}`}>
            <Button
              type="submit"
              size="full"
              disabled={
                loading ||
                JSON.stringify(formValues) == JSON.stringify(defaultValues)
              }
            >
              {state ? "Aanpassen" : "Toevoegen"}
            </Button>
          </div>
          <div className={`desktop-only ${styles.removePadding}`}>
            <Button
              type="submit"
              size="default"
              disabled={
                loading ||
                JSON.stringify(formValues) == JSON.stringify(defaultValues)
              }
            >
              {state ? "Aanpassen" : "Toevoegen"}
            </Button>
          </div>
        </Form>
      </div>
    </ScrollableScreen>
  );
};

const FormWatcher = ({ onValuesChange }) => {
  const { watch } = useFormContext();
  const values = watch();

  const prevValuesRef = useRef();
  useEffect(() => {
    if (JSON.stringify(prevValuesRef.current) !== JSON.stringify(values)) {
      onValuesChange(values);
    }
    prevValuesRef.current = values;
  }, [values, onValuesChange]);

  return null;
};

export default AddQuestion;
