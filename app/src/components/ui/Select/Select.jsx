import clsx from "clsx";
import { forwardRef } from "react";
import styles from "./Select.module.css";
import { CaretDown } from "@phosphor-icons/react";

const Select = forwardRef(
  ({ className, options, placeholder, ...props }, forwardedRef) => (
    <div className={styles.select}>
      <select
        className={clsx(className, styles.selectItem)}
        ref={forwardedRef}
        {...props}
      >
        <option value="" disabled hidden>
          {placeholder}
        </option>
        {options.map((option, index) => (
          <option key={index} value={option}>
            {option}
          </option>
        ))}
      </select>
      <CaretDown size={24} className={styles.chevron} />
    </div>
  )
);
Select.displayName = "Select";

export default Select;
