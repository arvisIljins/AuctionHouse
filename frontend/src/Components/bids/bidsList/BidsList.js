"use client";
import { useBidsStore } from "@/app/hooks/UseBidsStore";
import { getBidsById } from "@/app/services/auctionsService";
import { map } from "lodash";
import React, { useEffect } from "react";
import toast from "react-hot-toast";

const BidsList = ({ id }) => {
  const bids = useBidsStore((state) => state.bids);
  const setBids = useBidsStore((state) => state.setBids);

  useEffect(() => {
    getBidsById(id)
      .then((res) => {
        console.log(res);
        if (res.error) {
          throw res.error;
        } else {
          setBids(res.data);
        }
      })
      .catch((err) => {
        toast.error(res.message || err.message);
      });
  }, [id, setBids]);

  return <p>{map(bids, (bid) => bid.amount)}</p>;
};

export default BidsList;
