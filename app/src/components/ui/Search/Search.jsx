import { MagnifyingGlass } from "@phosphor-icons/react";
import styles from "./Search.module.css";
import Input from "../Input/Input";

const Search = () => {
  return (
    <div className={styles.inputContainer}>
      <Input
        name="fullNameSearch"
        placeholder="Search ..."
        className={styles.input}
      />
      <MagnifyingGlass size={20} className={styles.icon} />
    </div>
  );
};

export default Search;
