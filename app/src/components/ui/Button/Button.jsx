import { forwardRef } from "react";
import { cva } from "class-variance-authority";
import clsx from "clsx";
import styling from "./Button.module.css";

const buttonVariants = cva(styling.button, {
  variants: {
    variant: {
      primary: styling.primary,
      secondary: styling.secondary,
      tertiary: styling.tertiary,
    },
    size: {
      default: styling.defaultSize,
      full: styling.fullWidth,
      small: styling.small,
      superSmall: styling.superSmall,
    },
  },
  defaultVariants: {
    variant: "primary",
    size: "default",
  },
});

const Button = forwardRef(({ className, variant, size, ...props }, ref) => (
  <button
    ref={ref}
    {...props}
    className={clsx(buttonVariants({ variant, size, className }), "btn-reset")}
  />
));
Button.displayName = "Button";

export default Button;
