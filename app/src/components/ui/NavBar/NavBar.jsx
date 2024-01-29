import clsx from "clsx";
import Button from "../Button/Button";
import styles from "./NavBar.module.css";
import { Link, NavLink } from "react-router-dom";
import Modal from "../Modal/Modal.jsx";
import { useState } from "react";
import { useAuthContext } from "../../app/auth/AuthProvider.jsx";
import { SpecialistRoutes } from "../../../core/config/routes.js";
import { SignOut } from "@phosphor-icons/react";

const NavBar = () => {
  const [showModal, setShowModal] = useState(false);
  const { user, logout } = useAuthContext();

  const handleLogout = () => {
    setShowModal(false);
    logout();
  };

  return (
    <div className={styles.navBarContainer}>
      <div className="mobile-only">
        <Button
          className={`btn-reset ${styles.mobileLogOut}`}
          onClick={() => setShowModal(true)}
          disabled={showModal}
          size="square"
          variant="tertiary"
        >
          <SignOut size={20} className={styles.dots} />
        </Button>
      </div>
      <div className="desktop-only">
        <div className={styles.navBarLeft}>
          <Link
            to={SpecialistRoutes.PatientsOverview}
            className={styles.removeTextDecoration}
          >
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
          <div className="desktop-only">
            <Button
              variant="tertiary"
              size="small"
              onClick={() => setShowModal(true)}
            >
              Uitloggen
            </Button>
          </div>
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
