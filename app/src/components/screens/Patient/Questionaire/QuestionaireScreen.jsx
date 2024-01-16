import { useState } from "react";
import { Answers } from "../../../../core/config/questions";
import FullHeightScreen from "../../../ui/FullHeightScreen/FullHeightScreen";

const ExampleQuestions = [
  {
    question:
      "Als ik me over de pijn heen zou zetten, dan zou hij erger worden.",
    options: [
      {
        content: "helemaal oneens",
        position: 0,
      },
      {
        content: "oneens",
        position: 1,
      },
      {
        content: "eens",
        position: 2,
      },
      {
        content: "helemaal eens",
        position: 3,
      },
    ],
  },
  {
    question: "Ik ben bang dat de pijn erger wordt.",
    options: [
      {
        content: "helemaal oneens",
        position: 0,
      },
      {
        content: "oneens",
        position: 1,
      },
      {
        content: "eens",
        position: 2,
      },
      {
        content: "helemaal eens",
        position: 3,
      },
    ],
  },
  {
    question: "Ik ben bang dat de pijn nooit meer overgaat.",
    options: [
      {
        content: "helemaal oneens",
        position: 0,
      },
      {
        content: "oneens",
        position: 1,
      },
      {
        content: "eens",
        position: 2,
      },
      {
        content: "helemaal eens",
        position: 3,
      },
    ],
  },
  {
    question: "Ik ben bang dat de pijn iets ernstigs betekent.",
    options: [
      {
        content: "helemaal oneens",
        position: 0,
      },
      {
        content: "oneens",
        position: 1,
      },
      {
        content: "eens",
        position: 2,
      },
      {
        content: "helemaal eens",
        position: 3,
      },
    ],
  },
  {
    question: "Ik ben bang dat ik iets ernstigs heb.",
    options: [
      {
        content: "helemaal oneens",
        position: 0,
      },
      {
        content: "oneens",
        position: 1,
      },
      {
        content: "eens",
        position: 2,
      },
      {
        content: "helemaal eens",
        position: 3,
      },
    ],
  },
];

const QuestionaireScreen = () => {
  const [question, setQuestion] = useState(0);

  return (
    <FullHeightScreen>
      <h1>QuestionaireScreen</h1>
    </FullHeightScreen>
  );
};

export default QuestionaireScreen;
