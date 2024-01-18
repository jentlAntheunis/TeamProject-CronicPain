import Avatar from "../ui/Avatar/Avatar.jsx";
import TabBarNav from "../ui/TabBarNav/TabBarNav.jsx";
import TopBar from "../ui/TopBar/TopBar.jsx";
import { Play, ClipboardText } from "@phosphor-icons/react";
import styles from "./DashboardScreen.module.css";
import FullHeightScreen from "../ui/FullHeightScreen/FullHeightScreen.jsx";
import Button from "../ui/Button/Button.jsx";
import DailyPain from "../ui/DailyPain/DailyPain.jsx";

const DashboardScreen = () => {
  return (
    <FullHeightScreen>
      <DailyPain />
      <div className={styles.screen}>
        <TopBar />
        <Avatar />
        <div className={styles.btnContainer}>
          <Button variant="primary" size="full">
            Ik wil bewegen! <Play size={22} weight="bold" />
          </Button>
          <Button variant="secondary" size="full">
            Vul bonusvragen in <ClipboardText size={22} weight="bold" />
          </Button>
        </div>
      </div>
      <TabBarNav />
    </FullHeightScreen>
  );
};

export default DashboardScreen;
