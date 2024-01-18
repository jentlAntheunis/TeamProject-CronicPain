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
import NavBar from "../../../ui/navBar/navBar";

const formSchema = z.object({
  email: z.string().email(),
});

const AddPatient = () => {
  return (
    <FullHeightScreen className={`margins-desktop ${styles.screen}`}>
      <NavBar />
      <PageHeading>Patiënt toevoegen</PageHeading>
      <div className="desktop-only">
        <Button variant="secondary" className={styles.csvImport}>
          CSV importeren
        </Button>
      </div>
      <Form schema={formSchema} className={styles.formContainer}>
        <div className={styles.formItems}>
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
              <Input
                placeholder="dirkjanssens@voorbeeld.be"
                autoComplete="email"
              />
            </FormControl>
            <FormMessage />
          </FormItem>
        </div>
        <div className={`mobile-only ${styles.removePadding}`}>
          <Button type="submit" size="full">
            Toevoegen
          </Button>
        </div>
        <div className={`desktop-only ${styles.removePadding}`}>
          <Button type="submit" size="default">
            Toevoegen
          </Button>
        </div>
      </Form>
    </FullHeightScreen>
  );
};

export default AddPatient;
