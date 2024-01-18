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
import {
  sendMailToPatient,
  storePatient,
} from "../../../../core/utils/apiCalls";
import { useMutation } from "@tanstack/react-query";
import { useUser } from "../../../app/auth/AuthProvider";
import { useState } from "react";
import { toast } from "react-toastify";

const formSchema = z.object({
  lastName: z.string().min(2, { message: "Achternaam is te kort" }),
  firstName: z.string().min(2, { message: "Voornaam is te kort" }),
  email: z.string().email({ message: "Geen geldig e-mailadres" }),
});

const AddPatient = () => {
  const { mutate, isError, isLoading } = useMutation(storePatient);
  const [loading, setLoading] = useState(false);
  const user = useUser();

  const defaultValues = {
    lastName: "",
    fistName: "",
    email: "",
  };

  console.log(user);

  const handleSubmit = (data) => {
    console.log(data);
    setLoading(true);
    sendMailToPatient({ specialistId: user.id, ...data })
      .then(() => {
        setLoading(false);
        toast.success("E-mail verstuurd");
      })
      .catch((error) => {
        setLoading(false);
        toast.error(error.message);
      });
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
