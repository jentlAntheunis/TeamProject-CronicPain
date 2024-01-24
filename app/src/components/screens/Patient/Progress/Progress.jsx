import { toast } from "react-toastify";
import { getUserData } from "../../../../core/utils/apiCalls";
import { useQuery } from "@tanstack/react-query";
import { useUser } from "../../../app/auth/AuthProvider";
import ScrollableScreen from "../../../ui/ScrollableScreen/ScrollableScreen";
import TopBar from "../../../ui/TopBar/TopBar";
import TabBarNav from "../../../ui/TabBarNav/TabBarNav";
import styles from "./Progress.module.css";
import MovingInfluenceCard from "../../../ui/MovingInfluenceCard/MovingInfluenceCard";
import InfoTooltip from "../../../ui/InfoTooltip/InfoTooltip";
import Graph from "../../../ui/Graph/Graph";
import clsx from "clsx";
import Modal from "../../../ui/Modal/Modal";
import { X } from "@phosphor-icons/react";
import Button from "../../../ui/Button/Button";
import { useState } from "react";

const Progress = () => {
  const [showModal, setShowModal] = useState(false);

  const user = useUser();

  const { data, isLoading, isError } = useQuery({
    queryKey: ["user"],
    queryFn: () => getUserData(user.id),
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
            <div className={styles.modalTitle}>Invloed van bewegen</div>
            <button
              className={`btn-reset ${styles.closeBtn}`}
              onClick={() => setShowModal(false)}
            >
              <X size={32} />
            </button>
          </div>
          <div className={styles.modalText}>
            Deze kaartjes geven de invloed weer van het bewegen op de pijn
          </div>
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
          onClick={() => setShowModal(true)}
        />
      </div>
      <div className={styles.movingInfluenceCardsContainer}>
        <MovingInfluenceCard variant={"positive"} />
        <MovingInfluenceCard variant={"neutral"} />
        <MovingInfluenceCard variant={"negative"} />
      </div>
      <Graph
        variant={"bar"}
        title={"Duur bewegingssessies voorbije week"}
        className={styles.graph}
        setShowModal={setShowModal}
      />
      <Graph
        variant={"line"}
        title={"Pijn ervaring voorbije maand"}
        className={clsx(styles.graph, styles.lastGraph)}
        setShowModal={setShowModal}
      />
      <TabBarNav />
    </ScrollableScreen>
  );
};

export default Progress;
