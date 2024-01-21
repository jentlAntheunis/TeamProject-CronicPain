import {
  Form,
  FormControl,
  FormItem,
  FormLabel,
  FormMessage,
} from "../../../app/form/Form";
import Button from "../../../ui/Button/Button";
import Input from "../../../ui/Input/Input";
import PageHeading from "../../../ui/PageHeading/PageHeading";
import { z } from "zod";

import styles from "./AddPatient.module.css";
import NavBar from "../../../ui/NavBar/NavBar";
import {
  checkIfUserExists,
  sendMailToPatient,
  storePatient,
} from "../../../../core/utils/apiCalls";
import { useMutation } from "@tanstack/react-query";
import { useUser } from "../../../app/auth/AuthProvider";
import { useState } from "react";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";
import { SpecialistRoutes } from "../../../../core/config/routes";
import ScrollableScreen from "../../../ui/ScrollableScreen/ScrollableScreen";

const formSchema = z.object({
  lastName: z.string().min(2, { message: "Achternaam is te kort" }),
  firstName: z.string().min(2, { message: "Voornaam is te kort" }),
  email: z.string().email({ message: "Geen geldig e-mailadres" }),
});

const AddPatient = () => {
  const storeMutation = useMutation({
    mutationFn: storePatient,
  });
  const sendMailMutation = useMutation({
    mutationFn: sendMailToPatient,
  });
  const user = useUser();

  if (user) {
    console.log(user);
  }

  const navigate = useNavigate();

  const [loading, setLoading] = useState(false);

  const defaultValues = {
    lastName: "",
    fistName: "",
    email: "",
  };

  const handleSubmit = async (data) => {
    setLoading(true);

    try {
      const response = await checkIfUserExists(data.email);

      // if user doesn't exist, add user to database and send email
      if (!response) {
        const dataObject = { ...data, specialistId: user.id };

        await storeMutation.mutateAsync(dataObject);

        await sendMailMutation.mutateAsync(dataObject);

        toast.success("Gebruiker toegevoegd en mail verzonden");
        setLoading(false);
        navigate(SpecialistRoutes.Index);
      } else {
        setLoading(false);
        toast.error("Gebruiker met dit e-mailadres bestaat al");
      }
    } catch (error) {
      console.error(error);
      toast.error("Er ging iets mis. Probeer het opnieuw.");
      setLoading(false);
    }
  };

  return (
    <ScrollableScreen>
      <NavBar />
      <div className="container">
        <div className={styles.header}>
          <PageHeading>Patiënt toevoegen</PageHeading>
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

export default AddPatient;
