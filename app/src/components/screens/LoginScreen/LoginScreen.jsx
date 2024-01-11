import { PebblesMoods } from "../../../core/config/pebblesMoods";
import Pebbles from "../../ui/Illustrations/Pebbles";
import Wave from "../../ui/Illustrations/Wave";
import styling from "./LoginScreen.module.css";

const LoginScreen = () => {
  return (
    <div>
      {/* Top part */}
      <div>
        <h1>Pebbles</h1>
      </div>

      {/* Bottom part */}
      <div>
        <form action=""></form>
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
