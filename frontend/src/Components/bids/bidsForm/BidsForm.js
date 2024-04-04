"use client";
import { useBidsStore } from "@/app/hooks/UseBidsStore";
import { placeBid } from "@/app/services/auctionsService";
import Button from "@/components/button/Button";
import Loader from "@/components/loader/Loader";
import TextInput from "@/components/textInput/TextInput";
import { map } from "lodash";
import React, { useState } from "react";
import { useForm } from "react-hook-form";
import toast from "react-hot-toast";

const formValues = [
  {
    label: "Bid amount",
    placeholder: "Enter amount",
    type: "number",
    name: "amount",
    errorMessage: "Amount is required",
  },
];

const BidsForm = ({ auctionId }) => {
  const [loading, setLoading] = useState(false);
  const {
    register,
    handleSubmit,
    reset,
    control,
    formState: { errors },
  } = useForm({ mode: "onTouched" });

  const addBid = useBidsStore((state) => state.addBid);

  async function onSubmit(data) {
    try {
      setLoading(true);
      await placeBid(auctionId, +data.amount).then((bid) => {
        if (bid.success) {
          addBid(bid);
          reset();
        } else {
          toast.error(bid?.message || "Unexpected runtime error!");
        }
      });
    } catch (error) {
      toast.error(error?.message || "Unexpected runtime error!");
    } finally {
      setLoading(false);
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
      <Button text="Submit" type="submit" onClick={handleSubmit(onSubmit)} />
    </form>
  );
};

export default BidsForm;
