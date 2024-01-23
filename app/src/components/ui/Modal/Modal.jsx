import styles from "./Modal.module.css";

const Modal = ({ showModal, setShowModal, children }) => {
  const handleCloseModal = () => {
    document.body.style.overflowY = "";
    document.body.style.height = "";
    document.body.style.position = "";
    setShowModal(false);
  };

  if (!showModal) {
    return null;
  } else {
    document.body.style.overflowY = "hidden";
    document.body.style.height = "100svh";
    document.body.style.position = "fixed";
  }

  return (
    <div className={styles.modal}>
      <div className={styles.modalBackground} onClick={handleCloseModal}></div>
      <div className={styles.modalContent}>{children}</div>
    </div>
  );
};

export default Modal;
