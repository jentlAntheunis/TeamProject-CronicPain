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

const Progress = () => {
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
      <TopBar coins={data.data.coins} streak={data.data.streak} />
      <div className={styles.graphTitleContainer}>
        Invloed van bewegen
        <InfoTooltip text="Deze kaartjes geven de invloed weer van het bewegen op de pijn" />
      </div>
      <div className={styles.movingInfluenceCardsContainer}>
        <MovingInfluenceCard variant={"positive"} />
        <MovingInfluenceCard variant={"neutral"} />
        <MovingInfluenceCard variant={"negative"} />
      </div>
      <Graph variant={"bar"} title={"Duur bewegingssessies voorbije week"} className={styles.graph} />
      <Graph variant={"line"} title={"Pijn ervaring voorbije maand"} className={clsx(styles.graph, styles.lastGraph)} />
      <TabBarNav />
    </ScrollableScreen>
  );
};

export default Progress;
