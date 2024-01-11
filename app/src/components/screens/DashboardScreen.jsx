import Avatar from "../ui/Avatar/Avatar.jsx";
import Btn from "../ui/Btn/Btn.jsx";
import TabBarNav from "../ui/TabBarNav/TabBarNav.jsx";
import TopBar from "../ui/TopBar/TopBar.jsx";
import { Play, ClipboardText } from "@phosphor-icons/react";
import styles from "./DashboardScreen.module.css";

const DashboardScreen = () => {
  return (
    <>
      <div className={styles.screen}>
        <TopBar />
        <div className={styles.center}>
          <Avatar />
          <Btn text="Ik wil bewegen!" className={styles.btnSpacing}>
            <Play size={22} weight="bold" />
          </Btn>
          <Btn text="Vul bonusvragen in" type="secondary">
            <ClipboardText size={22} weight="bold" />
          </Btn>
        </div>
      </div>
      <TabBarNav />
    </>
  );
};

export default DashboardScreen;
