import { useEffect, useRef, useState } from "react";
import Button from "../../ui/Button/Button";
import FullHeightScreen from "../../ui/FullHeightScreen/FullHeightScreen";
import PageHeading from "../../ui/PageHeading/PageHeading";
import styles from "./MovementSuggestions.module.css";
import { suggestions } from "../../../core/config/movementSuggestions";
import { PatientRoutes } from "../../../core/config/routes";
import { useNavigate } from "react-router-dom";

const MovementSuggestions = () => {
  const [selectedSuggestion, setSelectedSuggestion] = useState(null);
  const suggestionsContainerRef = useRef(null);

  const navigate = useNavigate();

  const handleSuggestionClick = (index) => {
    setSelectedSuggestion(index);
  };

  const handleClickOutsideSuggestions = (event) => {
    if (
      suggestionsContainerRef.current &&
      !suggestionsContainerRef.current.contains(event.target)
    ) {
      setSelectedSuggestion(null);
    }
  };

  useEffect(() => {
    document.addEventListener("click", handleClickOutsideSuggestions);
    return () => {
      document.removeEventListener("click", handleClickOutsideSuggestions);
    };
  }, []);

  return (
    <FullHeightScreen className={styles.screen}>
      <div className={styles.layout}>
        <PageHeading backLink={PatientRoutes.Questionaire}>
          Kies een activiteit
        </PageHeading>
        <div className={styles.suggestions} ref={suggestionsContainerRef}>
          {suggestions.map((suggestion, index) => (
            <button
              key={index}
              className={`btn-reset ${styles.suggestion} ${
                selectedSuggestion === index ? styles.selected : ""
              }`}
              onClick={() => handleSuggestionClick(index)}
            >
              {suggestion}
            </button>
          ))}
        </div>
        <Button
          size="full"
          disabled={selectedSuggestion === null}
          onClick={() => navigate(PatientRoutes.TimeTracker)}
        >
          Volgende
        </Button>
      </div>
    </FullHeightScreen>
  );
};

export default MovementSuggestions;
