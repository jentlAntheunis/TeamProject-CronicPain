import Button from "../../ui/Button/Button";
import Coin from "../../ui/Icons/Coin";
import Pebbles from "../../ui/Illustrations/Pebbles";
import ScrollableScreen from "../../ui/ScrollableScreen/ScrollableScreen";
import TabBarNav from "../../ui/TabBarNav/TabBarNav";
import TopBar from "../../ui/TopBar/TopBar";
import styles from "./Shop.module.css";

const Shop = () => {
  let apiResponse = [
    {
      id: "f81762a4-a8cc-4a99-a04d-283c2b4e2bf8",
      name: "Red",
      hex: "#F63B3B",
      price: 10,
      isDeleted: false,
      deletedAt: null,
      owned: false,
      active: false,
    },

    {
      id: "b0315c2a-09d5-41cb-a8ad-2c5d01c93557",
      name: "Blue (Default)",
      hex: "#3B82F6",
      price: 0,
      isDeleted: false,
      deletedAt: null,
      owned: true,
      active: true,
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

  apiResponse.sort((a, b) => a.price - b.price);

  // Move the default item to the beginning of the array
  const defaultItem = apiResponse.find((item) =>
    item.name.toLowerCase().includes("(Default)")
  );
  if (defaultItem) {
    apiResponse.splice(apiResponse.indexOf(defaultItem), 1);
    apiResponse.unshift(defaultItem);
  }

  return (
    <ScrollableScreen>
      <TopBar coins={69} streak={420} />
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
                <Button size="shop" variants="shop" disabled={item.active}>
                  {item.active ? "Actief" : "Gebruik"}
                </Button>
              ) : (
                <Button size="shop" variants="shop">
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
