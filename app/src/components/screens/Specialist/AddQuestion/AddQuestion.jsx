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
import { useEffect, useState } from "react";
import Select from "../../../ui/Select/Select";
import {
  addQuestion,
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

const formSchema = z.object({
  content: z.string().min(5, { message: "Vraag is te kort" }),
  categoryId: z.string().min(1, { message: "Kies een categorie" }),
  scaleId: z.string().min(1, { message: "Kies een antwoordschaal" }),
});

const AddQuestion = () => {
  const [loading, setLoading] = useState(false);

  const user = useUser();

  const navigate = useNavigate();

  const addMutation = useMutation({
    mutationFn: addQuestion,
  });

  const {
    data: scaleData,
    isLoading: scaleLoading,
    isError: scaleError,
  } = useQuery({
    queryKey: ["scale"],
    queryFn: () => getScales(),
  });

  const {
    data: categoryData,
    isLoading: categoryLoading,
    isError: categoryError,
  } = useQuery({
    queryKey: ["category"],
    queryFn: () => getCategories(),
  });

  const defaultValues = {
    content: "",
    categoryId: "",
    scaleId: "",
  };

  const handleSubmit = (data) => {
    setLoading(true);
    try {
      const newData = {
        ...data,
        specialistId: user.id,
      };
      addMutation.mutate(newData);
      toast.success("Vraag toegevoegd");
      setLoading(false);
      navigate(SpecialistRoutes.QuestionsOverview);
    } catch (error) {
      console.log(error);
      toast.error("Er ging iets mis. Probeer het opnieuw.");
      setLoading(false);
    }
  };

  useEffect(() => {
    console.log(scaleData);
  }, [scaleData]);

  useEffect(() => {
    console.log(categoryData);
  }, [categoryData]);

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
            Vraag toevoegen
          </PageHeading>
          <div className="desktop-only">
            <Button variant="secondary" className={styles.csvImport}>
              CSV importeren
            </Button>
          </div>
        </div>
        <Form
          schema={formSchema}
          defaultValues={defaultValues}
          onSubmit={handleSubmit}
          className={styles.formContainer}
        >
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

export default AddQuestion;
