"use client";
import React from "react";
import "./text-input.scss";
import { useController } from "react-hook-form";

const TextInput = (props) => {
  const { fieldState, field } = useController({
    ...props,
    defaultValue: "",
  });
  const { placeholder, type, label } = props;

  return (
    <div className="form__group field">
      <input
        type={type}
        className="form__field"
        placeholder={placeholder}
        {...props}
        {...field}
      />
      {label && (
        <label for="name" className="form__label">
          {label}
        </label>
      )}
      {fieldState.error && (
        <div className="form__error">{fieldState.error.message}</div>
      )}
    </div>
  );
};

export default TextInput;
