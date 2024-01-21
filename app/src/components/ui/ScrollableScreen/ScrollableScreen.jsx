import styles from "./ScrollableScreen.module.css";

const ScrollableScreen = ({ children }) => (
  <div className={`margins-desktop screen-margin ${styles.scrollableScreen}`}>
    {children}
  </div>
);

export default ScrollableScreen;
