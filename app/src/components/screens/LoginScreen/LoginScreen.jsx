import { useState } from "react";
import { PebblesMoods } from "../../../core/config/pebblesMoods";
import Pebbles from "../../ui/Illustrations/Pebbles";
import Wave from "../../ui/Illustrations/Wave";
import styling from "./LoginScreen.module.css";
import { auth } from "../../../core/services/firebase";
import {
  useSendSignInLinkToEmail,
} from "react-firebase-hooks/auth";
import { actionCodeSettings } from "../../../core/config/emailAuth";

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
    <div>
      {/* Top part */}
      <div>
        <h1>Pebbles</h1>
      </div>

      {/* Bottom part */}
      <div>
        <form onSubmit={handleSubmit}>
          <input
            type="email"
            name="email"
            id="email"
            placeholder="dirkjanssens@voorbeeld.be"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
          <button type="submit">Log in</button>
        </form>
      </div>

      {/* Pebbles */}
      <div className={styling.pebbles}>
        <Pebbles mood={PebblesMoods.Bubbles} width="2rem" />
      </div>

      {/* Water */}
      <div className={styling.waterBackground}>
        <Wave />
        <div className={styling.water}></div>
      </div>
    </div>
  );
};

export default LoginScreen;
