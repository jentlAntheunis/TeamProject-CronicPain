import { zodResolver } from "@hookform/resolvers/zod";
import { useForm, FormProvider, Controller } from "react-hook-form";
import styling from "./Form.module.css";
import { createContext, forwardRef, useId } from "react";
import clsx from "clsx";
import Label from "../../ui/Label/Label";
import useFormField from "../../../core/hooks/useFormField";
import { Slot } from "@radix-ui/react-slot";

/**
 * @param {z.ZodObject} schema - Zod schema
 * @param {Object} defaultValues - Default values for the form
 * @param {Function} onSubmit - Function to execute when the form is submitted
 * @param {JSX.Element} children - Form children
 * @param {string} className - Class name for the form
 */
const Form = ({ schema, defaultValues, onSubmit, children, className }) => {
  const form = useForm({
    resolver: zodResolver(schema),
    defaultValues,
  });

  return (
    <FormProvider {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className={clsx(styling.form, className)}
      >
        {children}
      </form>
    </FormProvider>
  );
};

const FormItemContext = createContext({});

const FormItem = forwardRef(({ name, className, ...props }, ref) => {
  const id = useId();
  return (
    <FormItemContext.Provider value={{ name: name, id }}>
      <div ref={ref} className={clsx(styling.formItem, className)} {...props} />
    </FormItemContext.Provider>
  );
});
FormItem.displayName = "FormItem";

const FormLabel = forwardRef(({ className, ...props }, ref) => {
  const { error, formItemId } = useFormField({
    formItemContext: FormItemContext,
  });

  return (
    <Label
      ref={ref}
      className={clsx(error && styling.labelError, className)}
      htmlFor={formItemId}
      {...props}
    />
  );
});
FormLabel.displayName = "FormLabel";

const FormControl = forwardRef(({children, ...props}, ref) => {
  const { error, formItemId, name, register } = useFormField({
    formItemContext: FormItemContext,
  });

  return (
    <Slot ref={ref} id={formItemId} {...register(name)} aria-invalid={!!error} {...props}>
      {children}
    </Slot>
  );
});
FormControl.displayName = "FormControl";

const FormMessage = forwardRef(({ className, children, ...props }, ref) => {
  const { error } = useFormField({ formItemContext: FormItemContext });
  const body = error ? String(error?.message) : children;

  if (!body) return null;

  return (
    <p ref={ref} className={clsx(styling.message, className)} {...props}>
      {body}
    </p>
  );
});
FormMessage.displayName = "FormMessage";

export { Form, FormItem, FormLabel, FormControl, FormMessage };
