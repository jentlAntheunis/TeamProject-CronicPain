import clsx from "clsx";
import { forwardRef } from "react";
import styling from "./FileInput.module.css";

const FileInput = forwardRef(({ className, ...props }, forwardedRef) => (
  <input
    type="file"
    className={clsx(styling.fileInput, className)}
    ref={forwardedRef}
    {...props}
  />
));
FileInput.displayName = "FileInput";

export default FileInput;
