import { PebblesMoods } from "../../../core/config/pebblesMoods";
import Pebbles from "../Illustrations/Pebbles";
import styles from "./Avatar.module.css";

const Avatar = ({ color="#3B82F6" }) => (
  <div className={styles.container}>
    <div className={styles.avatar}>
      <Pebbles shieldColor={color} size="12rem" />
    </div>
  </div>
);

export default Avatar;
