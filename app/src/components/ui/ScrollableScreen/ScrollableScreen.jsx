import styles from "./ScrollableScreen.module.css";

const ScrollableScreen = ({ children }) => (
  <div className={`margins-desktop screen-padding ${styles.scrollableScreen}`}>
    {children}
  </div>
);

export default ScrollableScreen;
