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
import QuestionList from "../../../app/questionnaire/QuestionList/QuestionList";
import useTitle from "../../../../core/hooks/useTitle";

const Questions = () => {
  const [searchInput, setSearchInput] = useState("");

  useTitle("Vragen");

  const handleSearchChange = (event) => {
    setSearchInput(event.target.value);
  };

  const [filters, setFilters] = useState([]);

  const handleFilterClick = (filter) => {
    if (filters.includes(filter)) {
      setFilters(filters.filter((f) => f !== filter));
    } else {
      setFilters([...filters, filter]);
    }
  };

  return (
    <ScrollableScreen>
      <NavBar />
      <div className="container">
        <div className="desktop-only">
          <PageHeading>Vragen</PageHeading>
        </div>
        <div className={styles.searchAndAdd}>
          <Search
            name="QuestionSearch"
            value={searchInput}
            onChange={handleSearchChange}
            placeholder="Zoek vraag"
          />
          <div className="desktop-only">
            <Link to={SpecialistRoutes.AddQuestion}>
              <Button>
                <Plus size={18} weight="bold" />
                Voeg vraag toe
              </Button>
            </Link>
          </div>
        </div>
        <div className={styles.filters}>
          <button
            className={clsx("btn-reset", styles.filter, {
              [styles.active]: filters.includes("beweging"),
            })}
            onClick={() => handleFilterClick("beweging")}
          >
            Bewegingsvragen
          </button>
          <button
            className={clsx("btn-reset", styles.filter, {
              [styles.active]: filters.includes("bonus"),
            })}
            onClick={() => handleFilterClick("bonus")}
          >
            Bonusvragen
          </button>
        </div>
        <QuestionList search={searchInput} filters={filters} />
      </div>
      <div className={`mobile-only ${styles.addQuestionMobile}`}>
        <Link to={SpecialistRoutes.AddQuestion}>
          <Button className={styles.addQuestion}>
            <Plus size={18} weight="bold" />
            Voeg vraag toe
          </Button>
        </Link>
      </div>
    </ScrollableScreen>
  );
};

export default Questions;
