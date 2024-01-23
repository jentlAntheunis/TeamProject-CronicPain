import FullHeightScreen from "../../ui/FullHeightScreen/FullHeightScreen";
import Pebbles from "../../ui/Illustrations/Pebbles";
import TabBarNav from "../../ui/TabBarNav/TabBarNav";
import TopBar from "../../ui/TopBar/TopBar";
import styles from "./Shop.module.css";

const Shop = () => {
  return (
    <FullHeightScreen>
      <div className={styles.screen}>
        <TopBar coins={69} streak={420} />
        <Pebbles size="13.75rem" />
        <div>shopitems grid</div>
      </div>
      <TabBarNav />
    </FullHeightScreen>
  );
};

export default Shop;
