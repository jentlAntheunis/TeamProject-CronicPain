import styles from "./ScrollableScreen.module.css";

const ScrollableScreen = ({ children, className }) => (
  <div className={`margins-desktop screen-padding ${styles.scrollableScreen} ${className}`}>
    {children}
  </div>
);

export default ScrollableScreen;
