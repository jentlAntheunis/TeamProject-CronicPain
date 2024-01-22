import { cva } from "class-variance-authority";
import styles from "./MovingInfluanceCard.module.css";

const cardVariants = cva(styles.movingInfluanceCard, {
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

const MovingInfluanceCard = ({ variant }) => {
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
      <div className={styles.text}>
        <div className={styles.h6}>{text}</div>
        <div className={styles.small}>15 keer</div>
      </div>
      <div className={styles.percentage}>75%</div>
    </div>
  );
};

export default MovingInfluanceCard;
