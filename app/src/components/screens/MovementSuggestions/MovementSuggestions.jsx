import Button from "../../ui/Button/Button";
import FullHeightScreen from "../../ui/FullHeightScreen/FullHeightScreen";
import PageHeading from "../../ui/PageHeading/PageHeading";
import styles from "./MovementSuggestions.module.css";

const MovementSuggestions = () => {
  return (
    <FullHeightScreen className={styles.screen}>
      <div className={styles.layout}>
      <PageHeading>Kies een activiteit</PageHeading>
        <div className={styles.suggestions}>
          <button className={`btn-reset ${styles.suggestion}`}>
            Spieroefeningen
          </button>
          <button className={`btn-reset ${styles.suggestion}`}>Wandelen</button>
          <button className={`btn-reset ${styles.suggestion}`}>
            Huishoudelijke taken
          </button>
          <button className={`btn-reset ${styles.suggestion}`}>
            Vrij bewegen
          </button>
        </div>
      <Button size="full">Volgende</Button>
      </div>
    </FullHeightScreen>
  );
};

export default MovementSuggestions;
