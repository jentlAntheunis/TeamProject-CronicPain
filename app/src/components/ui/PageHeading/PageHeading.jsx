import styles from './PageHeading.module.css';
import { ArrowLeft } from '@phosphor-icons/react';

const PageHeading = ({ children }) => (
  <div className={styles.pageHeading}>
    <ArrowLeft size={32} className={styles.arrow}/>
    <h1 className={styles.heading}>{children}</h1>
  </div>
);

export default PageHeading;
