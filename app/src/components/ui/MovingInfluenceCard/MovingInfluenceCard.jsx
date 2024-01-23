import { cva } from "class-variance-authority";
import styles from "./MovingInfluenceCard.module.css";
import Wave from "../Illustrations/Wave";

const cardVariants = cva(styles.movingInfluenceCard, {
  variants: {
    variant: {
      positive: styles.positive,
      neutral: styles.neutral,
      negative: styles.negative,
    },
  },
  defaultVariants: {
    variant: "neutral",
  },
});

const MovingInfluenceCard = ({ variant }) => {
  let text;
  if (variant === "positive") {
    text = "Positief";
  } else if (variant === "neutral") {
    text = "Neutraal";
  } else if (variant === "negative") {
    text = "Negatief";
  }

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
        <div className={styles.small}>15 keer</div>
      </div>
      <div className={styles.percentage}>75%</div>
    </div>
  );
};

export default MovingInfluenceCard;
