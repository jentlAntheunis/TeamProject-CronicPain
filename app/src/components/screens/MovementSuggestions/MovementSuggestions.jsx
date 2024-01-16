import Button from "../../ui/Button/Button";
import FullHeightScreen from "../../ui/FullHeightScreen/FullHeightScreen";
import PageHeading from "../../ui/PageHeading/PageHeading";
import styles from "./MovementSuggestions.module.css";

const MovementSuggestions = () => {
  const suggestions = [
    "Spieroefeningen",
    "Wandelen",
    "Huishoudelijke taken",
    "Vrij bewegen",
    // Add more suggestions here
  ];

  return (
    <FullHeightScreen className={styles.screen}>
      <div className={styles.layout}>
        <PageHeading>Kies een activiteit</PageHeading>
        <div className={styles.suggestions}>
          {suggestions.map((suggestion, index) => (
            <button key={index} className={`btn-reset ${styles.suggestion}`}>
              {suggestion}
            </button>
          ))}
        </div>
        <Button size="full">Volgende</Button>
      </div>
    </FullHeightScreen>
  );
};

export default MovementSuggestions;
