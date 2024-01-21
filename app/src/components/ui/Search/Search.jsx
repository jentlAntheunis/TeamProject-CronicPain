import { MagnifyingGlass } from "@phosphor-icons/react";
import styles from "./Search.module.css";
import Input from "../Input/Input";

const Search = ({ name, value, onChange }) => {
  return (
    <div className={styles.inputContainer}>
      <Input
        name={name}
        placeholder="Search ..."
        className={styles.input}
        value={value}
        onChange={onChange}
      />
      <MagnifyingGlass size={20} className={styles.icon} />
    </div>
  );
};

export default Search;
