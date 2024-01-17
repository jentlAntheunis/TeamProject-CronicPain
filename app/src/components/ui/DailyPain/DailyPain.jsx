import Button from "../Button/Button";
import Modal from "../Modal/Modal";
import { X } from "@phosphor-icons/react";
import styles from "./DailyPain.module.css";
import Slider from "../Slider/Slider";

const DailyPain = () => {
  return (
    <Modal showModal={true}>
      <div className={styles.layout}>
        <button className={styles.btn}>
          <X size={32} display="block" />
        </button>
        <div className={styles.question}>Hoeveel pijn ervaar je momenteel?</div>
        <Slider max={10} />
        <Button size="full">Ok</Button>
      </div>
    </Modal>
  );
};

export default DailyPain;
