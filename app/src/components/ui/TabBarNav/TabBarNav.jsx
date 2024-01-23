import styles from "./TabBarNav.module.css";
import { House, ChartBar, Storefront } from "@phosphor-icons/react";
import { PatientRoutes } from "../../../core/config/routes.js";
import { NavLink } from "react-router-dom";
import clsx from "clsx";

const navItems = [
  {
    href: PatientRoutes.Dashboard,
    label: "Start",
  },
  {
    href: PatientRoutes.Progress,
    label: "Voortgang",
  },
  {
    href: PatientRoutes.Shop,
    label: "Winkel",
  },
];

const TabBarNav = () => {
  return (
    <div className={styles.tabBarNav}>
      {navItems.map((item) => (
        <NavLink
          to={item.href}
          key={item.label}
          // className={`${styles.navBtn}`}
          className={({ isActive }) =>
            clsx(isActive && styles.active, styles.navBtn)
          }
        >
          {({ isActive }) => (
            <button key={item.label} className={`${styles.navBtn} btn-reset`}>
              {item.label === "Start" && (
                <House size={24} weight={isActive ? "fill" : "regular"} />
              )}
              {item.label === "Voortgang" && (
                <ChartBar size={24} weight={isActive ? "fill" : "regular"} />
              )}
              {item.label === "Winkel" && (
                <Storefront size={24} weight={isActive ? "fill" : "regular"} />
              )}
              {item.label}
            </button>
          )}
        </NavLink>
      ))}
    </div>
  );
};

export default TabBarNav;
