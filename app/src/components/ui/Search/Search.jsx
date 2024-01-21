import { MagnifyingGlass } from "@phosphor-icons/react";
import styles from "./Search.module.css";
import Input from "../Input/Input";

const Search = ({ name, value, onChange, ...props }) => {
  return (
    <div className={styles.inputContainer}>
      <Input
        name={name}
        className={styles.input}
        value={value}
        onChange={onChange}
        {...props}
      />
      <MagnifyingGlass size={20} className={styles.icon} />
    </div>
  );
};

export default Search;
