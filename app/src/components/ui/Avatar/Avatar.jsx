import { PebblesMoods } from "../../../core/config/pebblesMoods";
import Pebbles from "../Illustrations/Pebbles";
import styles from "./Avatar.module.css";

const Avatar = () => (
  <div className={styles.container}>
    <div className={styles.avatar}>
      <Pebbles mood={PebblesMoods.Neutral} size="12rem" />
    </div>
  </div>
);

export default Avatar;
