"use client";

import React, { useEffect } from "react";
import "./auction-form.scss";
import { useForm } from "react-hook-form";
import TextInput from "@/Components/TextInput/TextInput";
import { map } from "lodash";
import Button from "@/Components/Button/Button";
import { useRouter } from "next/navigation";
import { createAuction } from "@/app/services/auctionsService";
import toast from "react-hot-toast";

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
  {
    label: "Reserve price",
    placeholder: "Enter price",
    type: "number",
    name: "reservePrice",
    errorMessage: "Reserve price is required",
  },
  {
    label: "Image url",
    placeholder: "Enter image url",
    type: "text",
    name: "imageUrl",
    errorMessage: "Image url is required",
  },
  {
    label: "Date",
    placeholder: "Enter date",
    type: "date",
    name: "date",
    errorMessage: "Date is required",
  },
];

const AuctionForm = () => {
  const {
    control,
    handleSubmit,
    setFocus,
    formState: { isSubmitting, isValid, isDirty, errors },
  } = useForm({ mode: "onTouched" });

  const router = useRouter();

  async function onSubmit(data) {
    try {
      const response = await createAuction(data);
      debugger;
      if (!response.success) {
        throw response;
      }
      router.push(`/auctions/details/${response.data.id}`);
    } catch (error) {
      toast.error(error.message || "Unexpected runtime error!");
    }
  }

  return (
    <form className="form" onSubmit={handleSubmit(onSubmit)}>
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
      <Button text="Submit" type="submit" />
    </form>
  );
};

export default AuctionForm;
