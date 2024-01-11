import styles from "./TabBarNav.module.css";
import { House, ChartBar, Storefront } from "@phosphor-icons/react";
import { route } from "../../../core/utils/routing.js";
import { PatientRoutes } from "../../../core/config/routes.js";

const navItems = [
  {
      href: route(PatientRoutes.Dashboard),
      isActive: location.pathname.includes(PatientRoutes.Dashboard),
      label: "Start",
  },
  {
      href: route(PatientRoutes.progress),
      isActive: location.pathname.includes(PatientRoutes.progress),
      label: "Voortgang",
  },
  {
      href: route(PatientRoutes.shop),
      isActive: location.pathname.includes(PatientRoutes.shop),
      label: "Winkel",
  }
]

const TabBarNav = () => {
  return (
    <div className={styles.tabBarNav}>
      {navItems.map((item) => (
        <button
          key={item.label}
          className={`${styles.navBtn} btn-reset ${
            item.isActive ? styles.active : ""
          }`}
        >
          {item.label === "Start" && <House size={24} weight="fill" />}
          {item.label === "Voortgang" && <ChartBar size={24} />}
          {item.label === "Winkel" && <Storefront size={24} />}
          {item.label}
        </button>
      ))}
    </div>
  );
};

export default TabBarNav;
