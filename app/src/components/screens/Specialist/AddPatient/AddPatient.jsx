import { Form, FormControl, FormItem, FormLabel, FormMessage } from "../../../app/form/Form";
import Button from "../../../ui/Button/Button";
import FullHeightScreen from "../../../ui/FullHeightScreen/FullHeightScreen";
import Input from "../../../ui/Input/Input";
import PageHeading from "../../../ui/PageHeading/PageHeading";
import { z } from "zod";

import styles from "./AddPatient.module.css";

const formSchema = z.object({
  email: z.string().email(),
});

const AddPatient = () => {
  return (
    <FullHeightScreen>
      <PageHeading>Patiënt toevoegen</PageHeading>
      <Form schema={formSchema}>
      <FormItem name="last-name">
        <FormLabel>Achternaam</FormLabel>
        <FormControl>
          <Input placeholder="Vul een achternaam in" />
        </FormControl>
        <FormMessage />
      </FormItem>
      <FormItem name="first-name">
        <FormLabel>Voornaam</FormLabel>
        <FormControl>
          <Input placeholder="Vul een voornaam in" />
        </FormControl>
        <FormMessage />
      </FormItem>
      <FormItem name="email">
        <FormLabel>E-mail</FormLabel>
        <FormControl>
          <Input placeholder="dirkjanssens@voorbeeld.be" autoComplete="email" />
        </FormControl>
        <FormMessage />
      </FormItem>
      <Button
        type="submit"
        size="full"
        className={styles.submit}
      >
        Toevoegen
      </Button>
    </Form>
    </FullHeightScreen>
  );
};

export default AddPatient;
