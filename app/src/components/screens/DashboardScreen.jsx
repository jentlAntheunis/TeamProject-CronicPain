import Avatar from "../ui/Avatar/Avatar.jsx";
import TabBarNav from "../ui/TabBarNav/TabBarNav.jsx";
import TopBar from "../ui/TopBar/TopBar.jsx";
import { Play, ClipboardText } from "@phosphor-icons/react";
import styles from "./DashboardScreen.module.css";
import FullHeightScreen from "../ui/FullHeightScreen/FullHeightScreen.jsx";
import Button from "../ui/Button/Button.jsx";
import { useAuthState, useIdToken } from "react-firebase-hooks/auth";
import { auth } from "../../core/services/firebase.js";
import { login } from "../../core/utils/apiCalls.js";
import axios from "axios";

const DashboardScreen = () => {
  const [user] = useIdToken(auth);

  if (user) {
    console.log(user);
  }

  const handleClick = async () => {
    axios
      .get("https://localhost:7121/users/exists/elicopter%40example.mail")
      .then((res) => {
        console.log(res);
      });
    // await login("elicopter@example.mail");
  };

  return (
    <FullHeightScreen>
      <div className={styles.screen}>
        <TopBar />
        <Avatar />
        <Button onClick={handleClick}>Test API</Button>
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
