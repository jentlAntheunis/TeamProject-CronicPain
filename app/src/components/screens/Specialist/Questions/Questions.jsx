import { Link } from "react-router-dom";
import NavBar from "../../../ui/NavBar/NavBar";
import PageHeading from "../../../ui/PageHeading/PageHeading";
import ScrollableScreen from "../../../ui/ScrollableScreen/ScrollableScreen";
import Search from "../../../ui/Search/Search";
import styles from "./Questions.module.css";
import Button from "../../../ui/Button/Button";
import { SpecialistRoutes } from "../../../../core/config/routes";
import { Plus } from "@phosphor-icons/react";
import { useState } from "react";

const Questions = () => {
  const questionData = [
    "Hoe lang ervaart u al chronische pijn?",
    "Welke behandelingen heeft u al geprobeerd?",
    "Hoe beïnvloedt chronische pijn uw dagelijks leven?",
    "Heeft u last van slaapproblemen door de pijn?",
    "Welke medicijnen gebruikt u voor de pijnbestrijding?",
    "Heeft u al alternatieve therapieën geprobeerd?",
    "Hoe gaat u om met de emotionele impact van chronische pijn?",
    "Heeft u ondersteuning van een zorgverlener voor uw pijnmanagement?",
    "Wat zijn uw belangrijkste triggers voor pijnverergering?",
    "Hoe beïnvloedt chronische pijn uw werk of studie?",
    "Heeft u ervaring met mindfulness of ontspanningstechnieken?",
    "Welke hulpmiddelen gebruikt u om met de pijn om te gaan?",
    "Heeft u last van sociale isolatie door de pijn?",
    "Hoe beïnvloedt chronische pijn uw stemming en mentale gezondheid?",
    "Heeft u advies voor andere chronische pijnpatiënten?",
  ];

  const [searchInput, setSearchInput] = useState("");

  const handleSearchChange = (event) => {
    setSearchInput(event.target.value);
  };

  const filteredQuestions = questionData.filter((question) =>
    question.toLowerCase().includes(searchInput.toLowerCase())
  );

  const [activeButton, setActiveButton] = useState("bewegingsvragen");

  const handleButtonClick = (button) => {
    setActiveButton(button);
  };

  return (
    <ScrollableScreen>
      <NavBar />
      <div className="container">
        <PageHeading>Vragen</PageHeading>
        <div className={styles.questionTypes}>
          <button
            className={`btn-reset ${styles.questionType} ${
              activeButton === "bewegingsvragen" ? styles.active : ""
            }`}
            onClick={() => handleButtonClick("bewegingsvragen")}
          >
            Bewegingsvragen
          </button>
          <button
            className={`btn-reset ${styles.questionType} ${
              activeButton === "bonusvragen" ? styles.active : ""
            }`}
            onClick={() => handleButtonClick("bonusvragen")}
          >
            Bonusvragen
          </button>
        </div>
        <div className={styles.searchAndAdd}>
          <Search
            name="QuestionSearch"
            value={searchInput}
            onChange={handleSearchChange}
            placeholder="Zoek vraag"
          />
          <Link to={SpecialistRoutes.AddQuestion}>
            <Button>
              <Plus size={18} weight="bold" />
              Voeg vraag toe
            </Button>
          </Link>
        </div>
        <div className={styles.questions}>
          {filteredQuestions.map((question, index) => {
            const indexMatch = question
              .toLowerCase()
              .indexOf(searchInput.toLowerCase());
            const nameBeforeMatch = question.slice(0, indexMatch);
            const nameMatch = question.slice(
              indexMatch,
              indexMatch + searchInput.length
            );
            const nameAfterMatch = question.slice(
              indexMatch + searchInput.length
            );
            return (
              <div className={styles.question} key={index}>
                <div className={styles.questionName}>
                  {nameBeforeMatch}
                  <span className={styles.match}>{nameMatch}</span>
                  {nameAfterMatch}
                </div>
                <Button variant="tertiary" size="superSmall">
                  Aanpassen
                </Button>
              </div>
            );
          })}
        </div>
      </div>
    </ScrollableScreen>
  );
};

export default Questions;
