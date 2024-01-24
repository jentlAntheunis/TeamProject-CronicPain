import Button from "../Button/Button";
import Modal from "../Modal/Modal";
import { X } from "@phosphor-icons/react";
import styles from "./DailyPain.module.css";
import Slider from "../Slider/Slider";

import { useState } from "react";
import { orderOptions } from "../../../core/utils/questions";
import { capitalize } from "../../../core/utils/formatText";
import { toast } from "react-toastify";
import { sendAnswers } from "../../../core/utils/apiCalls";

const DailyPain = ({ question, setQuestion }) => {
  const [sliderValue, setSliderValue] = useState(0);
  const [loading, setLoading] = useState(false);

  const handleClick = async () => {
    setLoading(true);
    try {
      const data = {
        questionnaireId: question.id,
        "questionnaireIndex": 0,
        answers: [
          {
            questionId: question.questions[0].id,
            optionId: orderOptions(question.questions[0].scale.options)[sliderValue].id
          }
        ]
      };
      await sendAnswers(data);
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
          <div className={styles.question}>{question.questions[0].content}</div>
          <div className={styles.sliderValue}>
            {capitalize(
              orderOptions(question.questions[0].scale.options)[sliderValue].content
            )}
          </div>
          <div>
            <Slider
              max={question.questions[0].scale.options.length - 1}
              value={sliderValue}
              setValue={setSliderValue}
              minLabel="Geen pijn"
              maxLabel="Veel pijn"
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
