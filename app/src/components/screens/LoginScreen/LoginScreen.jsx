import { forwardRef } from "react";
import { PebblesMoods } from "../../../core/config/pebblesMoods";
import Pebbles from "../../ui/Illustrations/Pebbles";
import Wave from "../../ui/Illustrations/Wave";
import styling from "./LoginScreen.module.css";
import { auth } from "../../../core/services/firebase";
import { useSendSignInLinkToEmail } from "react-firebase-hooks/auth";
import { actionCodeSettings } from "../../../core/config/emailAuth";
import { z } from "zod";
import { toast } from "react-toastify";

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

const LoginScreen = () => {
  return (
    <FullHeightScreen className={styling.mainContainer}>
        {/* Top part */}
        <div className={styling.titleContainer}>
          <h1>Pebbles</h1>
        </div>

        {/* Bottom part */}
        <div className={styling.formContainer}>
          <LoginForm />
        </div>

        {/* Pebbles */}
        <div className={styling.pebbles}>
          <Pebbles mood={PebblesMoods.Bubbles} size="13rem" />
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
  const [sendSignInLink, sending, error] = useSendSignInLinkToEmail(auth);

  const defaultValues = {
    email: "",
  };

  if (error) {
    toast("Er is iets fout gegaan, probeer opnieuw of neem contact op", {
      type: "error",
    });
  }

  const onSubmit = async ({ email }) => {
    const success = await sendSignInLink(email, actionCodeSettings);
    if (success) {
      window.localStorage.setItem("emailForSignIn", email);
      toast("E-mail verstuurd naar " + email + ", check je inbox!", {
        type: "success",
      });
    }
  };

  return (
    <Form schema={formSchema} defaultValues={defaultValues} onSubmit={onSubmit}>
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
        disabled={sending}
        className={styling.submit}
      >
        Submit
      </Button>
    </Form>
  );
};

export default LoginScreen;

const NestedInput = forwardRef(({ ...props }, ref) => {
  return <input ref={ref} {...props} />;
});
NestedInput.displayName = "NestedInput";
