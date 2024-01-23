import { useEffect } from "react";
import styles from "./Modal.module.css";

const Modal = ({ showModal, setShowModal, children, easyClose = true }) => {

  const handleCloseModal = () => {
    document.body.classList.remove("modal-open");
    setShowModal(false);
  };

  useEffect(() => {
    if (!showModal) {
      document.body.classList.remove("modal-open");
    } else {
      document.body.classList.add("modal-open");
    }
  }, [showModal]);

  if (!showModal) {
    return null;
  }

  return (
    <div className={styles.modal}>
      <div className={styles.modalBackground} onClick={easyClose && handleCloseModal}></div>
      <div className={styles.modalContent}>{children}</div>
    </div>
  );
};

export default Modal;
