import { toast } from "react-toastify";
import {
  getImpact,
  getMovementWeek,
  getPainMonth,
  getUserData,
} from "../../../../core/utils/apiCalls";
import { useQuery } from "@tanstack/react-query";
import { useUser } from "../../../app/auth/AuthProvider";
import ScrollableScreen from "../../../ui/ScrollableScreen/ScrollableScreen";
import TopBar from "../../../ui/TopBar/TopBar";
import TabBarNav from "../../../ui/TabBarNav/TabBarNav";
import styles from "./Progress.module.css";
import MovingInfluenceCard, {
  MovingInfluenceCardSkeleton,
} from "../../../ui/MovingInfluenceCard/MovingInfluenceCard";
import InfoTooltip from "../../../ui/InfoTooltip/InfoTooltip";
import Graph, { GraphSkeleton } from "../../../ui/Graph/Graph";
import clsx from "clsx";
import Modal from "../../../ui/Modal/Modal";
import { X } from "@phosphor-icons/react";
import Button from "../../../ui/Button/Button";
import { useState } from "react";
import { Impacts } from "../../../../core/config/impacts";
import {
  fillMissingDates,
  fillMissingMovementDates,
} from "../../../../core/utils/patientDetails";
import useTitle from "../../../../core/hooks/useTitle";

const Progress = () => {
  const [showModal, setShowModal] = useState(false);
  const [modalContent, setModalContent] = useState("");

  useTitle("Voortgang");
  const user = useUser();

  const { data, isLoading, isError } = useQuery({
    queryKey: ["user"],
    queryFn: () => getUserData(user.id),
    refetchOnWindowFocus: false,
  });
  const { data: impactData } = useQuery({
    queryKey: ["impact", user.id],
    queryFn: () => getImpact(user.id),
  });
  const { data: movementData } = useQuery({
    queryKey: ["movement", user.id],
    queryFn: () => getMovementWeek(user.id),
  });
  const { data: painData } = useQuery({
    queryKey: ["pain", user.id],
    queryFn: () => getPainMonth(user.id),
  });

  if (isLoading) return null;

  if (isError) {
    toast.error("Er is iets misgegaan bij het ophalen van je gegevens.");
    return null;
  }

  return (
    <ScrollableScreen>
      <Modal showModal={showModal} setShowModal={setShowModal}>
        <div className={styles.modalContainer}>
          <div className={styles.modalHeader}>
            <div className={styles.modalTitle}>{modalContent.title}</div>
            <button
              className={`btn-reset ${styles.closeBtn}`}
              onClick={() => setShowModal(false)}
            >
              <X size={32} />
            </button>
          </div>
          <div className={styles.modalText}>{modalContent.text}</div>
          <Button size="full" onClick={() => setShowModal(false)}>
            Ok
          </Button>
        </div>
      </Modal>
      <TopBar coins={data.data.coins} streak={data.data.streak} />
      <div className={styles.graphTitleContainer}>
        Invloed van bewegen
        <InfoTooltip
          text="Deze kaartjes geven de invloed weer van het bewegen op de pijn"
          onClick={() => {
            setShowModal(true);
            setModalContent({
              title: "Invloed van bewegen",
              text: "Deze kaartjes geven de invloed weer van het bewegen op de pijn",
            });
          }}
        />
      </div>
      <div className={styles.movingInfluenceCardsContainer}>
        {impactData ? (
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
        ) : (
          <>
            <MovingInfluenceCardSkeleton />
            <MovingInfluenceCardSkeleton />
            <MovingInfluenceCardSkeleton />
          </>
        )}
      </div>
      {movementData && painData ? (
        <>
          <Graph
            variant={"bar"}
            title="Duur bewegingssessies voorbije week"
            data={fillMissingMovementDates(movementData.data.days)}
            tooltip="Deze grafiek geeft de duur van de bewegingssessies weer per dag van de week uitgedrukt in minuten."
            className={styles.graph}
            setShowModal={setShowModal}
            setModalContent={setModalContent}
          ></Graph>
          <Graph
            variant={"line"}
            title={"Pijn ervaring voorbije maand"}
            data={fillMissingDates(painData.data.days)}
            tooltip="Deze grafiek geeft de pijnervaring weer op een schaal van 0 tot 10."
            className={clsx(styles.graph, styles.lastGraph)}
            setShowModal={setShowModal}
            setModalContent={setModalContent}
          ></Graph>
        </>
      ) : (
        <>
          <GraphSkeleton className={styles.graph} />
          <GraphSkeleton className={clsx(styles.graph, styles.lastGraph)} />
        </>
      )}
      <TabBarNav />
    </ScrollableScreen>
  );
};

export default Progress;
