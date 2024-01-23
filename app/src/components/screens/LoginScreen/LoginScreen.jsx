import { forwardRef, useState } from "react";
import { PebblesMoods } from "../../../core/config/pebblesMoods";
import Pebbles from "../../ui/Illustrations/Pebbles";
import Wave from "../../ui/Illustrations/Wave";
import styling from "./LoginScreen.module.css";
import { auth } from "../../../core/services/firebase";
import { useSendSignInLinkToEmail } from "react-firebase-hooks/auth";
import { actionCodeSettings } from "../../../core/config/emailAuth";
import { z } from "zod";
import { toast } from "react-toastify";
import { useQuery } from "@tanstack/react-query";

import {
  Form,
  FormControl,
  FormItem,
  FormLabel,
  FormMessage,
} from "../../app/form/Form";
import Input from "../../ui/Input/Input";
import Button from "../../ui/Button/Button";
import FullHeightScreen from "../../ui/FullHeightScreen/FullHeightScreen";
import { checkIfUserExists } from "../../../core/utils/apiCalls";

const LoginScreen = () => {
  return (
    <FullHeightScreen className={`margins-desktop ${styling.mainContainer}`}>
        {/* Top part */}
        <div className={styling.titleContainer}>
          <h1>Pebbles</h1>
        </div>

        {/* Bottom part */}
        <div className={styling.formContainer}>
          <div className={`desktop-only ${styling.formTitle}`}>Inloggen</div>
          <LoginForm />
        </div>

        {/* Pebbles */}
        <div className={styling.pebbles}>
          <Pebbles mood={PebblesMoods.Bubbles} size="13rem" className="mobile-only" />
          <Pebbles size="28.5rem" className="desktop-only" />
        </div>

      {/* Water */}
      <div className={styling.waterBackground}>
        <Wave />
        <div className={styling.water}></div>
      </div>
    </FullHeightScreen>
  );
};

const formSchema = z.object({
  email: z.string().email({ message: "Geen geldig e-mailadres" }),
});

const LoginForm = () => {
  // Hooks
  const [sendSignInLink, sending, error] = useSendSignInLinkToEmail(auth);

  // States
  const [message, setMessage] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  const defaultValues = {
    email: "",
  };

  if (error) {
    toast("Er is iets fout gegaan, probeer opnieuw of neem contact op", {
      type: "error",
    });
  }

  const onSubmit = async ({ email }) => {
    setMessage("");
    setIsLoading(true);
    try {
      const response = await checkIfUserExists(email);
      setIsLoading(false);
      if (response) {
        const success = await sendSignInLink(email, actionCodeSettings);
        if (success) {
          window.localStorage.setItem("emailForSignIn", email);
          toast("E-mail verstuurd naar " + email + ", check je inbox!", {
            type: "success",
          });
        }
      } else {
        setMessage("Gebruiker bestaat niet");
      }
    } catch (error) {
      setIsLoading(false);
      console.error(error);
      toast("Er is iets fout gegaan, probeer opnieuw of neem contact op", {
        type: "error",
      });
    }
  };

  return (
    <Form schema={formSchema} defaultValues={defaultValues} onSubmit={onSubmit}>
      <FormItem name="email">
        <FormLabel>E-mail</FormLabel>
        <FormControl>
          <Input
            placeholder="dirkjanssens@voorbeeld.be"
            autoComplete="email"
            className={styling.emailInput}
          />
        </FormControl>
        <FormMessage>{message}</FormMessage>
      </FormItem>
      <Button
        type="submit"
        size="full"
        disabled={sending || isLoading}
        className={styling.submit}
      >
        Log in
      </Button>
    </Form>
  );
};

export default LoginScreen;

const NestedInput = forwardRef(({ ...props }, ref) => {
  return <input ref={ref} {...props} />;
});
NestedInput.displayName = "NestedInput";
