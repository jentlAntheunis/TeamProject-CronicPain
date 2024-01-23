import { X } from "@phosphor-icons/react";
import Button from "../../ui/Button/Button";
import Coin from "../../ui/Icons/Coin";
import Pebbles from "../../ui/Illustrations/Pebbles";
import Modal from "../../ui/Modal/Modal";
import ScrollableScreen from "../../ui/ScrollableScreen/ScrollableScreen";
import TabBarNav from "../../ui/TabBarNav/TabBarNav";
import TopBar from "../../ui/TopBar/TopBar";
import styles from "./Shop.module.css";
import { useEffect, useState } from "react";
import { useUser } from "../../app/auth/AuthProvider.jsx";
import {
  buyColor,
  getShopItems,
  getUserData,
} from "../../../core/utils/apiCalls.js";
import { useQuery } from "@tanstack/react-query";
import { toast } from "react-toastify";

const Shop = () => {
  const [showModal, setShowModal] = useState(false);
  const [modalContent, setModalContent] = useState("");

  const user = useUser();

  const {
    data: userData,
    isLoading: userLoading,
    isError: userError,
  } = useQuery({
    queryKey: ["user"],
    queryFn: () => getUserData(user.id),
  });

  const {
    data: shopData,
    isLoading: shopLoading,
    isError: shopError,
  } = useQuery({
    queryKey: ["shop"],
    queryFn: () => getShopItems(user.id),
  });
  useEffect(() => {}, [userData, shopData]);

  if (!userData || !shopData) return;
  if (userLoading || shopLoading) return null;

  if (userError || shopError) {
    toast.error("Er is iets misgegaan bij het ophalen van je gegevens.");
    return null;
  }

  const handleBuyColor = async (colorId) => {
    console.log("buy " + colorId);
  };

  const handleUseColor = async (colorId) => {
    console.log("use " + colorId);
  };

  const handleClickShopItem = ({
    id = "",
    colorName = "",
    price = 0,
    hex = "",
    owned = false,
  }) => {
    colorName = colorName.replace(" (Default)", "");
    setShowModal(true);
    setModalContent({ id, colorName, price, hex, owned });
  };

  return (
    <ScrollableScreen>
      <Modal showModal={showModal} setShowModal={setShowModal}>
        <div className={styles.center}>
          <div className={styles.modalHeader}>
            <div className={styles.modalText}>
              <div className={styles.modalTitle}>Kleur</div>
              <div className={styles.modalColorTitle}>
                {modalContent.colorName}
              </div>
            </div>
            <button
              className={`btn-reset ${styles.closeBtn}`}
              onClick={() => setShowModal(false)}
            >
              <X size={32} />
            </button>
          </div>
          <Pebbles
            size={"12rem"}
            shieldColor={modalContent.hex}
            className={styles.pebbles}
          />
        </div>
        {modalContent.owned ? (
          <div className={styles.buttonContainer}>
            <Button size="full" onClick={() => handleUseColor(modalContent.id)}>
              Gebruik
            </Button>
          </div>
        ) : (
          <div className={styles.buttonContainer}>
            <Button
              variant="tertiary"
              size="full"
              onClick={() => setShowModal(false)}
            >
              Annuleer
            </Button>
            <Button size="full" onClick={() => handleBuyColor(modalContent.id)}>
              <Coin size={20} />
              {modalContent.price}
            </Button>
          </div>
        )}
      </Modal>
      <TopBar coins={userData.data.coins} streak={userData.data.streak} />
      <div className={styles.center}>
        <Pebbles
          size="13.75rem"
          shieldColor={shopData.data.find((item) => item.active)?.hex}
        />
        <div className={styles.shopItems}>
          {shopData.data.map((item) => (
            <div className={styles.shopItem} key={item.id}>
              <div
                className={styles.colorCard}
                style={{ backgroundColor: item.hex }}
              ></div>
              <div className={styles.colorName}>
                {item.name.replace(" (Default)", "")}
              </div>
              {item.owned ? (
                <Button
                  size="shop"
                  variants="shop"
                  disabled={item.active}
                  onClick={() =>
                    handleClickShopItem({
                      id: item.id,
                      colorName: item.name,
                      hex: item.hex,
                      owned: item.owned,
                    })
                  }
                >
                  {item.active ? "Actief" : "Gebruik"}
                </Button>
              ) : (
                <Button
                  size="shop"
                  variants="shop"
                  onClick={() =>
                    handleClickShopItem({
                      id: item.id,
                      colorName: item.name,
                      price: item.price,
                      hex: item.hex,
                      owned: item.owned,
                    })
                  }
                >
                  <Coin size={18} />
                  {item.price}
                </Button>
              )}
            </div>
          ))}
        </div>
      </div>
      <TabBarNav />
    </ScrollableScreen>
  );
};

export default Shop;
