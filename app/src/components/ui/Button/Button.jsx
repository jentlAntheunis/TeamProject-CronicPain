import { forwardRef } from "react";
import { cva } from "class-variance-authority";
import clsx from "clsx";
import styling from "./Button.module.css";

const buttonVariants = cva(styling.button, {
  variants: {
    variant: {
      primary: styling.primary,
      secondary: styling.secondary,
    },
    size: {
      small: {},
      medium: {},
      large: {},
    },
  },
  defaultVariants: {
    variant: "primary",
    size: "medium",
  },
});

const Button = forwardRef(({ className, variant, size, ...props }, ref) => (
  <button
    ref={ref}
    {...props}
    className={clsx(buttonVariants({ variant, size, className }))}
  />
));
Button.displayName = "Button";

export default Button;
