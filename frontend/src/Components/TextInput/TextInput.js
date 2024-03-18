"use client";
import "react-datepicker/dist/react-datepicker.css";
import React, { forwardRef } from "react";
import "./text-input.scss";
import { useController } from "react-hook-form";
import DatePicker from "react-datepicker";

const TextInput = (props) => {
  const { fieldState, field } = useController({
    ...props,
    defaultValue: "",
  });
  const { placeholder, type, label } = props;

  const CustomInput = forwardRef(({ value, onClick, placeholder }, ref) => (
    <input
      className="form__field form__date"
      onClick={onClick}
      value={value}
      placeholder={placeholder}
      readOnly
      ref={ref}
    />
  ));

  return (
    <div className="form__group field">
      {type === "date" ? (
        <DatePicker
          {...field}
          dateFormat="dd MMMM yyyy h:mm a"
          showTimeSelect
          className="form__field"
          selected={field.value}
          onChange={(date) => field.onChange(date)}
          customInput={<CustomInput />}
        />
      ) : (
        <input
          type={type}
          className="form__field"
          placeholder={placeholder}
          {...props}
          {...field}
        />
      )}
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
