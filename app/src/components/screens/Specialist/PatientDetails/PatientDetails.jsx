import { Link, useNavigate, useParams } from "react-router-dom";
import ScrollableScreen from "../../../ui/ScrollableScreen/ScrollableScreen";
import NavBar from "../../../ui/NavBar/NavBar";
import PageHeading from "../../../ui/PageHeading/PageHeading";
import { SpecialistRoutes } from "../../../../core/config/routes";
import styles from "./PatientDetails.module.css";
import MovingInfluenceCard from "../../../ui/MovingInfluenceCard/MovingInfluenceCard";
import Graph from "../../../ui/Graph/Graph";
import Button from "../../../ui/Button/Button";
import RewardMetric from "../../../ui/RewardMetric/RewardMetric";
import Streaks from "../../../ui/Icons/Streaks";
import InfoTooltip from "../../../ui/InfoTooltip/InfoTooltip";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "../../../ui/Popover/Popover";
import { Calendar } from "../../../ui/Calendar/Calendar";
import { useEffect, useState } from "react";
import { CalendarBlank } from "@phosphor-icons/react";
import clsx from "clsx";
import { useQuery } from "@tanstack/react-query";
import {
  getImpact,
  getMovementWeek,
  getPainMonth,
  getQuestionnaires,
  getUserData,
  validatePatient,
} from "../../../../core/utils/apiCalls";
import { Impacts } from "../../../../core/config/impacts";
import { fillMissingDates, fillMissingMovementDates } from "../../../../core/utils/patientDetails";
import QuestionnaireList from "../../../app/questionnaire/QuestionnaireList/QuestionnaireList";
import useTitle from "../../../../core/hooks/useTitle";
import { useUser } from "../../../app/auth/AuthProvider";

const questionnaires = [
  {
    category: "bewegingsvragen",
    datetime: "03/01/2024 14:03",
  },
  {
    category: "bonusvragen",
    datetime: "04/01/2024 15:12",
  },
  {
    category: "bewegingsvragen",
    datetime: "04/01/2024 10:45",
  },
  {
    category: "bewegingsvragen",
    datetime: "04/01/2024 09:27",
  },
  {
    category: "bonusvragen",
    datetime: "05/01/2024 16:58",
  },
  {
    category: "bewegingsvragen",
    datetime: "05/01/2024 11:30",
  },
  {
    category: "bewegingsvragen",
    datetime: "05/01/2024 13:15",
  },
  {
    category: "bewegingsvragen",
    datetime: "05/01/2024 08:59",
  },
  {
    category: "bonusvragen",
    datetime: "05/01/2024 17:42",
  },
  {
    category: "bonusvragen",
    datetime: "06/01/2024 12:20",
  },
];

const PatientDetails = () => {
  const [date, setDate] = useState();
  let { id } = useParams();
  const user = useUser();
  const navigate = useNavigate();

  // Queries
  const { data: validatedData } = useQuery({
    queryKey: ["validated", id],
    queryFn: () => validatePatient(user.id, id),
  });
  const { data: patientData } = useQuery({
    queryKey: ["user", id],
    queryFn: () => getUserData(id),
  });
  const { data: movementData } = useQuery({
    queryKey: ["movement", id],
    queryFn: () => getMovementWeek(id),
  });
  const { data: painData } = useQuery({
    queryKey: ["pain", id],
    queryFn: () => getPainMonth(id),
  });
  const { data: impactData } = useQuery({
    queryKey: ["impact", id],
    queryFn: () => getImpact(id),
  });
  const { data: questionnairesData } = useQuery({
    queryKey: ["questionnaires", id],
    queryFn: () => getQuestionnaires(id),
  });

  useEffect(() => {
    // If patient is not validated, navigate to overview
    if (validatedData && !validatedData.data) {
      navigate(SpecialistRoutes.PatientsOverview);
    }
  }, [validatedData, navigate]);

  useTitle(
    patientData
      ? patientData.data.lastName + " " + patientData.data.firstName
      : "Patiënt details"
  );

  if (!validatedData) return null;

  // Sort questionnaires by date descending
  const sortedQuestionnaires = questionnairesData?.data.sort((a, b) => {
    const dateA = new Date(a.date);
    const dateB = new Date(b.date);
    return dateB - dateA;
  });

  return (
    <ScrollableScreen>
      <NavBar />
      <div className="container">
        <PageHeading backLink={SpecialistRoutes.PatientsOverview}>
          {patientData && (
            <div className={styles.nameAndStreaksContainer}>
              {patientData.data.lastName + " " + patientData.data.firstName}
              <RewardMetric
                number={patientData.data.streak}
                className={styles.streaks}
              >
                <Streaks />
              </RewardMetric>
            </div>
          )}
        </PageHeading>
        <div className={styles.movingInfluenceContainer}>
          <div className={styles.titleAndTooltipContainer}>
            Invloed van bewegen
            <InfoTooltip text="Deze kaartjes geven de invloed weer van het bewegen op de pijn" />
          </div>
          <div className={styles.movingInfluenceCardsContainer}>
            {impactData && (
              <>
                <MovingInfluenceCard
                  variant={Impacts.Positive}
                  data={impactData.data}
                />
                <MovingInfluenceCard
                  variant={Impacts.Neutral}
                  data={impactData.data}
                />
                <MovingInfluenceCard
                  variant={Impacts.Negative}
                  data={impactData.data}
                />
              </>
            )}
          </div>
        </div>
        <div className={styles.graphs}>
          {movementData && painData && (
            <>
              <Graph
                variant={"bar"}
                title="Duur bewegingssessies voorbije week"
                data={fillMissingMovementDates(movementData.data.days)}
                tooltip="Deze grafiek geeft de duur van de bewegingssessies weer per dag van de week uitgedrukt in minuten."
              ></Graph>
              <Graph
                variant={"line"}
                title={"Pijn ervaring voorbije maand"}
                data={fillMissingDates(painData.data.days)}
                tooltip="Deze grafiek geeft de pijnervaring weer op een schaal van 0 tot 10."
              ></Graph>
            </>
          )}
        </div>
        <div className={styles.questionnairesContainer}>
          <div className={styles.titleAndSearchContainer}>
            <div className={styles.h5}>Antwoorden</div>
            <Popover>
              <PopoverTrigger asChild>
                <Button
                  variant="input"
                  size="input"
                  className={clsx(!date && styles.noValue)}
                >
                  <CalendarBlank size={16} />
                  {date ? date.toLocaleDateString("en-GB") : "Kies een datum"}
                </Button>
              </PopoverTrigger>
              <PopoverContent>
                <Calendar
                  mode="single"
                  selected={date}
                  onSelect={setDate}
                  className="rounded-md border"
                />
              </PopoverContent>
            </Popover>
          </div>
          <div className={styles.questionnaires}>
            {questionnairesData && (
              <QuestionnaireList
                questionnaires={sortedQuestionnaires}
                date={date}
              />
            )}
          </div>
        </div>
      </div>
    </ScrollableScreen>
  );
};

export default PatientDetails;
