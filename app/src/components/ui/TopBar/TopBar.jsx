import { forwardRef, useEffect, useRef, useState } from "react";
import Coin from "../Icons/Coin";
import Streaks from "../Icons/Streaks";
import RewardMetric from "../RewardMetric/RewardMetric.jsx";

import styles from "./TopBar.module.css";
import {
  DotsThreeVertical,
  SignOut,
  Info,
} from "@phosphor-icons/react";
import Modal from "../Modal/Modal.jsx";
import Button from "../Button/Button.jsx";
import { useAuthContext } from "../../app/auth/AuthProvider.jsx";
import { useNavigate } from "react-router-dom";
import { PatientRoutes } from "../../../core/config/routes.js";

const TopBar = ({ coins, streak }) => {
  const { logout } = useAuthContext();
  const [showMenu, setShowMenu] = useState(false);
  const [showModal, setShowModal] = useState(false);
  const menu = useRef(null);

  useEffect(() => {
    const closeOpenMenus = (e) => {
      if (showMenu && !menu.current?.contains(e.target)) {
        setShowMenu(false);
      }
    };

    document.addEventListener("mousedown", closeOpenMenus);
    return () => {
      document.removeEventListener("mousedown", closeOpenMenus);
    };
  }, [showMenu]);

  const onClickDots = () => {
    setShowMenu(!showMenu);
  };

  const handleLogout = () => {
    setShowModal(false);
    logout();
  };

  return (
    <div className={styles.spaceBetween}>
      <div className={styles.flex}>
        <RewardMetric number={coins}>
          <Coin />
        </RewardMetric>
        <RewardMetric number={streak}>
          <Streaks />
        </RewardMetric>
      </div>
      <div className={styles.menuContainer}>
        <button className="btn-reset" onClick={onClickDots} disabled={showMenu}>
          <DotsThreeVertical size={32} weight="bold" className={styles.dots} />
        </button>
        <Menu
          showMenu={showMenu}
          setShowMenu={setShowMenu}
          setShowModal={setShowModal}
          ref={menu}
        />
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

const Menu = forwardRef(({ showMenu, setShowMenu, setShowModal }, ref) => {
  const navigate = useNavigate();

  useEffect(() => {
    if (!showMenu) {
      document.body.classList.remove("modal-open");
    } else {
      document.body.classList.add("modal-open");
    }
  }, [showMenu]);

  if (!showMenu) {
    return null;
  }

  const handleInfo = () => {
    setShowModal(false);
    document.body.classList.remove("modal-open");
    navigate(PatientRoutes.ExtraInfo);
  };

  return (
    <div className={styles.menuWrapper}>
      <div className={styles.menu} ref={ref}>
        <button
          className={`btn-reset ${styles.menuItem}`}
          onClick={() => {
            setShowModal(true);
            setShowMenu(false);
          }}
        >
          Uitloggen
          <SignOut size={24} />
        </button>
        <button className={`btn-reset ${styles.menuItem}`} onClick={handleInfo}>
          Info
          <Info size={24} />
        </button>
      </div>
    </div>
  );
});
Menu.displayName = "Menu";

export default TopBar;
