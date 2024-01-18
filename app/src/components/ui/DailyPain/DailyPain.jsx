import Button from "../Button/Button";
import Modal from "../Modal/Modal";
import { X } from "@phosphor-icons/react";
import styles from "./DailyPain.module.css";
import Slider from "../Slider/Slider";

import { useState } from "react";

const DailyPain = () => {
  const [sliderValue, setSliderValue] = useState(0);

  return (
    <Modal showModal={false}>
      <div className={styles.btnContainer}>
        <button className={`btn-reset`}>
          <X size={32} display="block" />
        </button>
      </div>
      <div className={styles.layout}>
        <div className={styles.question}>Hoeveel pijn ervaar je momenteel?</div>
        <div className={styles.sliderValue}>{sliderValue}</div>
        <div>
          <Slider
            max={10}
            value={sliderValue}
            setValue={setSliderValue}
            minLabel="geen pijn"
            maxLabel="veel pijn"
          />
          <Button size="full" className={styles.button}>
            Ok
          </Button>
        </div>
      </div>
    </Modal>
  );
};

export default DailyPain;
