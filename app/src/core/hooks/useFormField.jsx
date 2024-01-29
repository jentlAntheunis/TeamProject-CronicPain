import { useContext } from "react";
import { useFormContext } from "react-hook-form";

const useFormField = ({ formItemContext}) => {
  const itemContext = useContext(formItemContext);
  const { getFieldState, formState, register } = useFormContext();

  const fieldState = getFieldState(itemContext.name, formState);

  if (!itemContext) {
    throw new Error("useFormField must be used within <FormItem>");
  }

  const { id } = itemContext;

  return {
    id,
    name: itemContext.name,
    register,
    formItemId: `${id}-form-item`,
    ...fieldState,
  };
};

export default useFormField;
