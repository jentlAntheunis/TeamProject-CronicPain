import clsx from "clsx";
import Button from "../Button/Button";
import styles from "./NavBar.module.css";
import { Link, NavLink } from "react-router-dom";
import { auth } from "../../../core/services/firebase";
import Modal from "../Modal/Modal.jsx";
import { useState } from "react";
import { useAuthContext } from "../../app/auth/AuthProvider.jsx";
import { SpecialistRoutes } from "../../../core/config/routes.js";

const NavBar = () => {
  const [showModal, setShowModal] = useState(false);
  const { user, logout } = useAuthContext();

  const handleLogout = () => {
    setShowModal(false);
    logout();
  };

  return (
    <div className={`desktop-only ${styles.navBarContainer}`}>
      <div className={styles.navBarLeft}>
        <Link to={SpecialistRoutes.PatientsOverview} className={styles.removeTextDecoration}>
          <div className={styles.navBarLogo}>Pebbles</div>
        </Link>
        {user && (
          <>
            <div className={styles.navBarSpacer}></div>
            <div className={styles.navBarSpecialist}>
              {user.firstName + " " + user.lastName}
            </div>
          </>
        )}
      </div>
      {user && (
        <div className={styles.navBarMenu}>
          <NavLink
            to={SpecialistRoutes.PatientsOverview}
            className={({ isActive }) =>
              clsx(isActive && styles.active, styles.navBarMenuItem)
            }
          >
            Patiënten
          </NavLink>
          <NavLink
            to={SpecialistRoutes.QuestionsOverview}
            className={({ isActive }) =>
              clsx(isActive && styles.active, styles.navBarMenuItem)
            }
          >
            Vragen
          </NavLink>
          <Button
            variant="tertiary"
            size="small"
            onClick={() => setShowModal(true)}
          >
            Uitloggen
          </Button>
        </div>
      )}
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
