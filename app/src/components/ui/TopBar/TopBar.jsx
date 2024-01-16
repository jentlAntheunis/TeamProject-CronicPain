import { forwardRef, useEffect, useRef, useState } from "react";
import Coin from "../Icons/Coin";
import Streaks from "../Icons/Streaks";
import RewardMetric from "../RewardMetric/RewardMetric.jsx";

import styles from "./TopBar.module.css";
import { DotsThreeVertical, SignOut, Scroll } from "@phosphor-icons/react";
import Modal from "../Modal/Modal.jsx";
import Button from "../Button/Button.jsx";
import { auth } from "../../../core/services/firebase.js";

const TopBar = () => {
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
    auth.signOut();
  };



  return (
    <div className={styles.spaceBetween}>
      <div className={styles.flex}>
        <RewardMetric number={300}>
          <Coin />
        </RewardMetric>
        <RewardMetric number={2}>
          <Streaks />
        </RewardMetric>
      </div>
      <div className={styles.menuContainer}>
        <DotsThreeVertical
          size={32}
          weight="bold"
          className={styles.dots}
          onClick={onClickDots}
        />
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
  if (!showMenu) {
    return null;
  }

  return (
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
      <button className={`btn-reset ${styles.menuItem}`}>
        Credits
        <Scroll size={24} />
      </button>
    </div>
  );
});
Menu.displayName = "Menu";

export default TopBar;
