import { useEffect, useRef, useState } from "react";
import Button from "../../../ui/Button/Button";
import FullHeightScreen from "../../../ui/FullHeightScreen/FullHeightScreen";
import PageHeading from "../../../ui/PageHeading/PageHeading";
import styles from "./MovementSuggestions.module.css";
import { suggestions } from "../../../../core/config/movementSuggestions";
import { PatientRoutes } from "../../../../core/config/routes";
import { Link, useNavigate } from "react-router-dom";
import useTitle from "../../../../core/hooks/useTitle";
import clsx from "clsx";
import { ArrowLeft } from "@phosphor-icons/react";

const MovementSuggestions = () => {
  const [selectedSuggestion, setSelectedSuggestion] = useState(null);
  const suggestionsContainerRef = useRef(null);

  useTitle("Kies een activiteit");
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
      <div className={clsx(styles.layout, "container")}>
        <PageHeading
          backLink={PatientRoutes.Questionaire}
          className="mobile-only"
        >
          Kies een activiteit
        </PageHeading>
        <div className={clsx(styles.header, "desktop-only")}>
          <Link
            to={PatientRoutes.Questionaire}
            className={`btn-reset ${styles.backBtn}`}
          >
            <ArrowLeft size={32} display="block" className={styles.backIcon} />
          </Link>
          <h1 className={styles.headingText}>Kies een activiteit</h1>
        </div>
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
