import clsx from "clsx";
import Button from "../Button/Button";
import styles from "./NavBar.module.css";
import { NavLink } from "react-router-dom";
import { auth } from "../../../core/services/firebase";

const handleLogout = () => {
  auth.signOut();
};

const NavBar = () => {
  return (
    <div className={`desktop-only ${styles.navBarContainer}`}>
      <div className={styles.navBar}>
        <div className={styles.navBarLeft}>
          <div className={styles.navBarLogo}>Pebbles</div>
          <div className={styles.navBarSpacer}></div>
          <div className={styles.navBarSpecialist}>Willem Devriend</div>
        </div>
        <div className={styles.navBarMenu}>
          <NavLink
            to="/patients"
            className={({ isActive }) =>
              clsx(isActive && styles.active, styles.navBarMenuItem)
            }
          >
            Patiënten
          </NavLink>
          <NavLink
            to="/questions"
            className={({ isActive }) =>
              clsx(isActive && styles.active, styles.navBarMenuItem)
            }
          >
            Vragen
          </NavLink>
          <Button variant="tertiary" size="small" onClick={handleLogout}>
            Uitloggen
          </Button>
        </div>
      </div>
    </div>
  );
};

export default NavBar;
