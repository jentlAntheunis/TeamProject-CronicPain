import { cva } from "class-variance-authority";
import styles from "./MovingInfluenceCard.module.css";
import Wave from "../Illustrations/Wave";
import { useEffect, useState } from "react";
import { Impacts } from "../../../core/config/impacts";
import { countImpact } from "../../../core/utils/patientDetails";

const cardVariants = cva(styles.movingInfluenceCard, {
  variants: {
    variant: {
      [Impacts.Positive]: styles.positive,
      [Impacts.Neutral]: styles.neutral,
      [Impacts.Negative]: styles.negative,
    },
  },
  defaultVariants: {
    variant: "neutral",
  },
});

const MovingInfluenceCard = ({ variant, data }) => {
  const [text, setText] = useState("");
  const [results, setResults] = useState({});

  useEffect(() => {
    if (variant === Impacts.Positive) {
      setText("Positief");
    } else if (variant === Impacts.Neutral) {
      setText("Neutraal");
    } else if (variant === Impacts.Negative) {
      setText("Negatief");
    }
  }, [variant]);

  useEffect(() => {
    const results = countImpact(data);
    setResults(results);
  }, [data]);

  return (
    <div className={cardVariants({ variant })}>
      <div className={styles.waveContainer}>
        <Wave
          className={styles.wave}
          opacity={1}
          preserveAspectRatio="none"
          height="4rem"
        />
        <div className={styles.waveBackground}></div>
      </div>
      <div className={styles.text}>
        <div className={styles.h6}>{text}</div>
        <div className={styles.small}>{results[variant]} keer</div>
      </div>
      <div className={styles.percentage}>
        {isNaN(results[variant] / Object.keys(data).length)
          ? "0%"
          : `${Math.round(
              (results[variant] / Object.keys(data).length) * 100
            )}%`}
      </div>
    </div>
  );
};

export default MovingInfluenceCard;
