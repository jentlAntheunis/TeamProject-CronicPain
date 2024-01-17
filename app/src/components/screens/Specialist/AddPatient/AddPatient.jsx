import {
  Form,
  FormControl,
  FormItem,
  FormLabel,
  FormMessage,
} from "../../../app/form/Form";
import Button from "../../../ui/Button/Button";
import FullHeightScreen from "../../../ui/FullHeightScreen/FullHeightScreen";
import Input from "../../../ui/Input/Input";
import PageHeading from "../../../ui/PageHeading/PageHeading";
import { z } from "zod";

import styles from "./AddPatient.module.css";

const formSchema = z.object({
  lastName: z.string().min(2, { message: "Achternaam is te kort" }),
  firstName: z.string().min(2, { message: "Voornaam is te kort" }),
  email: z.string().email({ message: "Geen geldig e-mailadres" }),
});

const AddPatient = () => {
  const defaultValues = {
    lastName: "",
    fistName: "",
    email: "",
  };

  const handleSubmit = (data) => {
    console.log(data);
  };

  return (
    <FullHeightScreen className={styles.screen}>
      <PageHeading>Patiënt toevoegen</PageHeading>
      <Form
        schema={formSchema}
        defaultValues={defaultValues}
        onSubmit={handleSubmit}
        className={styles.formContainer}
      >
        <div className={styles.formItems}>
          <FormItem name="lastName">
            <FormLabel>Achternaam</FormLabel>
            <FormControl>
              <Input placeholder="Vul een achternaam in" />
            </FormControl>
            <FormMessage />
          </FormItem>
          <FormItem name="firstName">
            <FormLabel>Voornaam</FormLabel>
            <FormControl>
              <Input placeholder="Vul een voornaam in" />
            </FormControl>
            <FormMessage />
          </FormItem>
          <FormItem name="email">
            <FormLabel>E-mail</FormLabel>
            <FormControl>
              <Input placeholder="dirkjanssens@voorbeeld.be" />
            </FormControl>
            <FormMessage />
          </FormItem>
        </div>
        <Button type="submit" size="full" className={styles.submit}>
          Toevoegen
        </Button>
      </Form>
    </FullHeightScreen>
  );
};

export default AddPatient;
