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
import clsx from "clsx";

const Questions = () => {
  const questions = [
    {
      category: "bewegingsvragen",
      question: "Hoe lang ervaart u al chronische pijn?",
    },
    {
      category: "bewegingsvragen",
      question: "Welke behandelingen heeft u al geprobeerd?",
    },
    {
      category: "bewegingsvragen",
      question: "Hoe beïnvloedt chronische pijn uw dagelijks leven?",
    },
    {
      category: "bewegingsvragen",
      question: "Heeft u last van slaapproblemen door de pijn?",
    },
    {
      category: "bewegingsvragen",
      question: "Welke medicijnen gebruikt u voor de pijnbestrijding?",
    },
    {
      category: "bewegingsvragen",
      question: "Heeft u al alternatieve therapieën geprobeerd?",
    },
    {
      category: "bewegingsvragen",
      question: "Hoe gaat u om met de emotionele impact van chronische pijn?",
    },
    {
      category: "bewegingsvragen",
      question:
        "Heeft u ondersteuning van een zorgverlener voor uw pijnmanagement?",
    },
    {
      category: "bewegingsvragen",
      question: "Wat zijn uw belangrijkste triggers voor pijnverergering?",
    },
    {
      category: "bewegingsvragen",
      question: "Hoe beïnvloedt chronische pijn uw werk of studie?",
    },
    {
      category: "bewegingsvragen",
      question: "Heeft u ervaring met mindfulness of ontspanningstechnieken?",
    },
    {
      category: "bonusvragen",
      question: "Welke hulpmiddelen gebruikt u om met de pijn om te gaan?",
    },
    {
      category: "bonusvragen",
      question: "Heeft u last van sociale isolatie door de pijn?",
    },
    {
      category: "bonusvragen",
      question:
        "Hoe beïnvloedt chronische pijn uw stemming en mentale gezondheid?",
    },
    {
      category: "bonusvragen",
      question: "Heeft u advies voor andere chronische pijnpatiënten?",
    },
  ];

  const [searchInput, setSearchInput] = useState("");

  const handleSearchChange = (event) => {
    setSearchInput(event.target.value);
  };

  // TODO: change to config categories
  const [filters, setFilters] = useState(["bewegingsvragen", "bonusvragen"]);

  const handleFilterClick = (filter) => {
    if (filters.includes(filter)) {
        setFilters(filters.filter((f) => f !== filter));
    } else {
      setFilters([...filters, filter]);
    }
  };

  const filteredQuestions = questions
    .filter((question) => filters.includes(question.category))
    .filter((question) =>
      question.question.toLowerCase().includes(searchInput.toLowerCase())
    )
    .map((question) => question.question);

  return (
    <ScrollableScreen>
      <NavBar />
      <div className="container">
        <PageHeading>Vragen</PageHeading>
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
        <div className={styles.filters}>
          <button
            className={clsx("btn-reset", styles.filter, {
              [styles.active]: filters.includes("bewegingsvragen"),
            })}
            onClick={() => handleFilterClick("bewegingsvragen")}
          >
            Bewegingsvragen
          </button>
          <button
            className={clsx("btn-reset", styles.filter, {
              [styles.active]: filters.includes("bonusvragen"),
            })}
            onClick={() => handleFilterClick("bonusvragen")}
          >
            Bonusvragen
          </button>
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
          {filters.length === 0 ? (
            <div className={styles.noQuestions}>
              Selecteer een filter om vragen te zien
            </div>
          ) : filteredQuestions.length === 0 && (
            <div className={styles.noQuestions}>
              Geen vragen gevonden met deze zoekterm
            </div>
          )}
        </div>
      </div>
    </ScrollableScreen>
  );
};

export default Questions;
