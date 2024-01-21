import Button from "../Button/Button";
import Modal from "../Modal/Modal";
import { X } from "@phosphor-icons/react";
import styles from "./DailyPain.module.css";
import Slider from "../Slider/Slider";

import { useState } from "react";
import { orderOptions } from "../../../core/utils/questions";
import { capitalize } from "../../../core/utils/formatText";
import { toast } from "react-toastify";

const DailyPain = ({ question, setQuestion }) => {
  const [sliderValue, setSliderValue] = useState(0);
  const [loading, setLoading] = useState(false);

  const handleClick = async () => {
    setLoading(true);
    try {
      // TODO: send answer to backend
      const data = {
        // TODO: add answer
      }
      setLoading(false);
      localStorage.setItem("lastLaunch", new Date().getDate().toString());
    } catch (error) {
      setLoading(false);
      toast.error("Er is iets misgegaan bij het opslaan van je antwoord.");
      console.error(error);
    }
    setQuestion(null);
  }

  return (
    <Modal showModal={question}>
      {question && (
        <div className={styles.layout}>
          <div className={styles.question}>{question?.content}</div>
          <div className={styles.sliderValue}>
            {capitalize(
              orderOptions(question.scale.options)[sliderValue].content
            )}
          </div>
          <div>
            <Slider
              max={question.scale.options.length - 1}
              value={sliderValue}
              setValue={setSliderValue}
              minLabel={orderOptions(question.scale.options)[0].content}
              maxLabel={
                orderOptions(question.scale.options)[
                  question.scale.options.length - 1
                ].content
              }
            />
            <Button onClick={handleClick} disabled={loading} size="full" className={styles.button}>
              Ok
            </Button>
          </div>
        </div>
      )}
    </Modal>
  );
};

export default DailyPain;
