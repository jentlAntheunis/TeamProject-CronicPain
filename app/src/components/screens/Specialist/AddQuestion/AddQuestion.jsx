import { z } from "zod";
import ScrollableScreen from "../../../ui/ScrollableScreen/ScrollableScreen";
import NavBar from "../../../ui/NavBar/NavBar";
import PageHeading from "../../../ui/PageHeading/PageHeading";
import { SpecialistRoutes } from "../../../../core/config/routes";
import Button from "../../../ui/Button/Button";
import styles from "./AddQuestion.module.css";
import { Form, FormControl, FormItem, FormLabel, FormMessage } from "../../../app/form/Form";
import Input from "../../../ui/Input/Input";
import { useState } from "react";
import Select from "../../../ui/Select/Select";

const formSchema = z.object({
  question: z.string().min(5, { message: "Vraag is te kort" }),
  category: z.string().min(1, { message: "Kies een categorie" }),
  scale: z.string().min(1, { message: "Kies een antwoordschaal" }),
});

const AddQuestion = () => {
  const [loading, setLoading] = useState(false);

  const defaultValues = {
    question: "",
    category: "",
    scale: "",
  };

  const handleSubmit = (data) => {
    console.log(data);
  };

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
            <FormItem name="question">
              <FormLabel>Vraag</FormLabel>
              <FormControl>
                <Input placeholder="Vul een vraag in" />
              </FormControl>
              <FormMessage />
            </FormItem>
            <FormItem name="category">
              <FormLabel>Categorie</FormLabel>
              <FormControl>
                <Select placeholder="Kies een categorie" options={["Bewegingsvragen", "Bonusvragen"]} />
              </FormControl>
              <FormMessage />
            </FormItem>
            <FormItem name="scale">
              <FormLabel>Antwoordschaal</FormLabel>
              <FormControl>
                <Select placeholder="Kies een antwoordschaal" options={["oneens -> eens", "niet -> altijd"]} />
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
