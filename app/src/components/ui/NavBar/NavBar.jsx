import clsx from "clsx";
import Button from "../Button/Button";
import styles from "./NavBar.module.css";
import { NavLink } from "react-router-dom";
import { auth } from "../../../core/services/firebase";
import Modal from "../Modal/Modal.jsx";
import { useState } from "react";
import { useAuthContext } from "../../app/auth/AuthProvider.jsx";

const NavBar = () => {
  const [showModal, setShowModal] = useState(false);
  const { logout } = useAuthContext();

  const handleLogout = () => {
    setShowModal(false);
    logout();
  };

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
          <Button variant="tertiary" size="small" onClick={() => setShowModal(true)}>
            Uitloggen
          </Button>
        </div>
      </div>
      <Modal showModal={showModal} setShowModal={setShowModal}>
        <div>
          <div className={styles.modalText}>
            Weet je zeker dat je wilt uitloggen?
          </div>
          <div className={styles.modalButtons}>
            <Button
              variant="tertiary"
              size="small"
              className="btn-reset"
              onClick={() => setShowModal(false)}
            >
              Annuleren
            </Button>
            <Button size="small" className="btn-reset" onClick={handleLogout}>
              Uitloggen
            </Button>
          </div>
        </div>
      </Modal>
    </div>
  );
};

export default NavBar;
