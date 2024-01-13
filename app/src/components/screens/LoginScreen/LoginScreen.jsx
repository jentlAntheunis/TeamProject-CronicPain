import { forwardRef, useState } from "react";
import { PebblesMoods } from "../../../core/config/pebblesMoods";
import Pebbles from "../../ui/Illustrations/Pebbles";
import Wave from "../../ui/Illustrations/Wave";
import styling from "./LoginScreen.module.css";
import { auth } from "../../../core/services/firebase";
import { useSendSignInLinkToEmail } from "react-firebase-hooks/auth";
import { actionCodeSettings } from "../../../core/config/emailAuth";
import { z } from "zod";
import { FormProvider, useForm, useFormContext } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  Form,
  FormControl,
  FormItem,
  FormLabel,
  FormMessage,
} from "../../app/form/Form";
import Input from "../../ui/Input/Input";
import Button from "../../ui/Button/Button";

const LoginScreen = () => {
  const [email, setEmail] = useState("");
  const [sendSignInLink, sending, error] = useSendSignInLinkToEmail(auth);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const success = await sendSignInLink(email, actionCodeSettings);
    if (success) {
      window.localStorage.setItem("emailForSignIn", email);
      setEmail("");
      console.log("email sent to " + email);
    }
  };

  if (sending) return <p>Sending...</p>;

  if (error) return <p>{error.message}</p>;

  return (
    <div className={`full-height padding-mobile ${styling.mainContainer}`}>
      {/* Top part */}
      <div className={styling.titleContainer}>
        <h1>Pebbles</h1>
      </div>

      {/* Bottom part */}
      <div>
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
    </div>
  );
};

const formSchema = z.object({
  email: z.string().email(),
});

const LoginForm = () => {
  const defaultValues = {
    email: "",
  };

  const onSubmit = (data) => console.log(data);

  return (
    <Form schema={formSchema} defaultValues={defaultValues} onSubmit={onSubmit}>
      <FormItem name="email">
        <FormLabel>Email</FormLabel>
        <FormControl>
          <Input placeholder="Email" autoComplete="email" />
        </FormControl>
        <FormMessage />
      </FormItem>
      <Button type="submit">Submit</Button>
    </Form>
  );
};

export default LoginScreen;

const NestedInput = forwardRef(({ ...props }, ref) => {
  return <input ref={ref} {...props} />;
});
NestedInput.displayName = "NestedInput";
