import clsx from "clsx";
import { forwardRef } from "react";
import styling from "./Label.module.css";

const Label = forwardRef((props, forwardedRef) => (
  <label
    {...props}
    className={clsx(styling.label, props.className)}
    ref={forwardedRef}
    onMouseDown={(e) => {
      e.preventDefault();
    }}
  />
));

Label.displayName = "Label";

export default Label;
