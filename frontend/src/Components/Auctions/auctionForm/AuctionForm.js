"use client";

import React, { useEffect, useState } from "react";
import "./auction-form.scss";
import { useForm } from "react-hook-form";
import TextInput from "@/components/textInput/TextInput";
import { map } from "lodash";
import Button from "@/components/button/Button";
import { usePathname, useRouter } from "next/navigation";
import { createAuction, updateAuction } from "@/app/services/auctionsService";
import toast from "react-hot-toast";
import Loader from "@/components/loader/Loader";

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
    label: "End date",
    placeholder: "Enter date",
    type: "date",
    name: "endDate",
    errorMessage: "Date is required",
  },
];

const AuctionForm = ({ auction }) => {
  const {
    control,
    handleSubmit,
    reset,
    formState: { isSubmitting, isValid, isDirty, errors },
  } = useForm({ mode: "onTouched" });
  const [loading, setLoading] = useState(false);
  const pathname = usePathname();
  const router = useRouter();
  const isNew = pathname === "/auctions/create";

  useEffect(() => {
    if (auction) {
      const { title, description, tags, imageUrl, reservePrice, endDate } =
        auction;
      reset({
        title,
        description,
        tags,
        imageUrl,
        reservePrice,
        endDate: new Date(endDate),
      });
    }
  }, [auction, reset]);

  async function onSubmit(data) {
    try {
      setLoading(true);
      let response;
      if (isNew) {
        response = await createAuction(data);
      } else {
        data.id = auction.id;
        response = await updateAuction(data);
      }
      if (!response?.success) {
        throw response;
      }
      router.push(`/auctions/details/${response.data.id}`);
      setLoading(false);
    } catch (error) {
      setLoading(false);
      toast.error(error?.message || "Unexpected runtime error!");
    }
  }

  return (
    <form className="form" onSubmit={handleSubmit(onSubmit)}>
      {loading && <Loader />}
      {!loading &&
        map(formValues, (item, index) => {
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
