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
import { getUserData } from "../../../core/utils/apiCalls.js";
import { useQuery } from "@tanstack/react-query";
import { toast } from "react-toastify";

let apiResponse = [
  {
    id: "f81762a4-a8cc-4a99-a04d-283c2b4e2bf8",
    name: "Red",
    hex: "#F63B3B",
    price: 10,
    isDeleted: false,
    deletedAt: null,
    owned: true,
    active: true,
  },

  {
    id: "b0315c2a-09d5-41cb-a8ad-2c5d01c93557",
    name: "Blue (Default)",
    hex: "#3B82F6",
    price: 0,
    isDeleted: false,
    deletedAt: null,
    owned: true,
    active: false,
  },

  {
    id: "1948463f-31e0-4bfd-8211-64d87085067e",
    name: "Orange",
    hex: "#F7990C",
    price: 10,
    isDeleted: false,
    deletedAt: null,
    owned: false,
    active: false,
  },

  {
    id: "d061d986-63d7-4315-90a8-9b7165afea00",
    name: "Purple",
    hex: "#AF3BF6",
    price: 10,
    isDeleted: false,
    deletedAt: null,
    owned: false,
    active: false,
  },

  {
    id: "b8f47aac-e7c5-48fc-88d2-ad10be213499",
    name: "Pink",
    hex: "#F14DD7",
    price: 10,
    isDeleted: false,
    deletedAt: null,
    owned: false,
    active: false,
  },

  {
    id: "e9c12c00-cad3-4234-a391-d818ee353faa",
    name: "Gold",
    hex: "#F8D101",
    price: 50,
    isDeleted: false,
    deletedAt: null,
    owned: false,
    active: false,
  },
];

const Shop = () => {
  const [showModal, setShowModal] = useState(false);
  const [modalContent, setModalContent] = useState("");

  apiResponse.sort((a, b) => a.price - b.price);

  // Move the default item to the beginning of the array
  const defaultItem = apiResponse.find((item) =>
    item.name.toLowerCase().includes("(Default)")
  );
  if (defaultItem) {
    apiResponse.splice(apiResponse.indexOf(defaultItem), 1);
    apiResponse.unshift(defaultItem);
  }

  const user = useUser();
  const { data, isLoading, isError } = useQuery({
    queryKey: ["user"],
    queryFn: () => getUserData(user.id),
  });

  useEffect(() => {
    if (!data) return;
    console.log(data);
  }, [data]);

  const handleClickShopItem = ({
    colorName = "",
    price = 0,
    hex = "",
    owned = false,
    active = false,
  }) => {
    colorName = colorName.replace(" (Default)", "");
    setShowModal(true);
    setModalContent({ colorName, price, hex, owned });
  };

  if (isLoading) return null;

  if (isError) {
    toast.error("Er is iets misgegaan bij het ophalen van je gegevens.");
    return null;
  }

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
            <Button size="full">Gebruik</Button>
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
            <Button size="full">
              <Coin size={20} />
              {modalContent.price}
            </Button>
          </div>
        )}
      </Modal>
      <TopBar coins={data.data.coins} streak={data.data.streak} />
      <div className={styles.center}>
        <Pebbles
          size="13.75rem"
          shieldColor={apiResponse.find((item) => item.active)?.hex}
        />
        <div className={styles.shopItems}>
          {apiResponse.map((item) => (
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
