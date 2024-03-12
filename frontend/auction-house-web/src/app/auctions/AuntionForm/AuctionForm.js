"use client";

import React from "react";
import "./auction-form.scss";
import { useForm } from "react-hook-form";
import TextInput from "@/Components/TextInput/TextInput";
import { map } from "lodash";

const formValues = [
  {
    label: "Auction title",
    placeholder: "Enter title",
    type: "text",
    name: "title",
    errorMessage: "Title is required",
  },
  {
    label: "Description",
    placeholder: "Enter description",
    type: "text",
    name: "description",
    errorMessage: "Description is required",
  },
  {
    label: "Tags",
    placeholder: "Enter tags",
    type: "text",
    name: "tags",
    errorMessage: "Tags is required",
  },
];

const AuctionForm = () => {
  const {
    control,
    handleSubmit,
    setFocus,
    formState: { isSubmitting, isValid, isDirty, errors },
  } = useForm();

  function onSubmit(data) {
    console.log(data);
  }

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      {map(formValues, (item, index) => {
        return (
          <TextInput
            key={index}
            control={control}
            label={item.label}
            placeholder={item.placeholder}
            type={item.type}
            name={item.name}
            rules={{ required: item.errorMessage }}
          />
        );
      })}
      <button type="submit">submit</button>
    </form>
  );
};

export default AuctionForm;
