import clsx from "clsx";
import { forwardRef } from "react";
import styling from "./Input.module.css";

const Input = forwardRef(
  ({ className, type = "text", ...props }, forwardedRef) => (
    <input
      type={type}
      className={clsx(styling.input, className)}
      ref={forwardedRef}
      {...props}
    />
  )
);
Input.displayName = "Input";

export default Input;
