import Button from "../Button/Button";
import Coin from "../Icons/Coin";
import Modal from "./Modal";
import { Link } from "react-router-dom";
import styles from "./RecieveCoinsModal.module.css";

const RecieveCoinsModal = ({ showModal, setShowModal, amount, linkTo }) => (
  <Modal showModal={showModal} setShowModal={setShowModal}>
    <div className={styles.container}>
      <h1 className={styles.title}>Bedankt, hier is je beloning!</h1>
      <div className={styles.amount}>
        <Coin size={32} />
        <span>{amount}</span>
      </div>
      <Link to={linkTo}>
        <Button size="full">Bedankt</Button>
      </Link>
    </div>
  </Modal>
);

export default RecieveCoinsModal;
