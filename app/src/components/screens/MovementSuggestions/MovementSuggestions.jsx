import { useEffect, useRef, useState } from "react";
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

  const [selectedSuggestion, setSelectedSuggestion] = useState(null);
  const suggestionsContainerRef = useRef(null);

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
        <PageHeading>Kies een activiteit</PageHeading>
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
        <Button size="full" disabled={selectedSuggestion === null}>
          Volgende
        </Button>
      </div>
    </FullHeightScreen>
  );
};

export default MovementSuggestions;
