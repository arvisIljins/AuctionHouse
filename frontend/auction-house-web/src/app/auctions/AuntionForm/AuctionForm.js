"use client";

import React from "react";
import "./auction-form.scss";
import { useForm } from "react-hook-form";

const AuctionForm = () => {
  const {
    register,
    handleSubmit,
    setFocus,
    formState: { isSubmitting, isValid, isDirty, errors },
  } = useForm();

  function onSubmit(data) {
    console.log(data);
  }

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <input
        type="text"
        placeholder="Enter title"
        {...register("title", { required: "Title isRequired" })}
      />
    </form>
  );
};

export default AuctionForm;
