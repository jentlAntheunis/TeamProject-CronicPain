import styles from "./Modal.module.css";

const Modal = ({ showModal, setShowModal, children }) => {
  const handleCloseModal = () => {
    setShowModal(false);
  };

  if (!showModal) {
    return null;
  }

  return (
    <div className={styles.modal}>
      <div className={styles.modalBackground} onClick={handleCloseModal}></div>
      <div className={styles.modalContent}>{children}</div>
    </div>
  );
};

export default Modal;
