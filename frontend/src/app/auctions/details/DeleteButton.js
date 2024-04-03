"use client";
import Button from "@/components/button/Button";
import { deleteAuction } from "@/app/services/auctionsService";
import { useRouter } from "next/navigation";
import React, { useState } from "react";

export default function DeleteButton({ id }) {
  const [loading, setLoading] = useState(false);
  const router = useRouter();

  function handleDeleteAuction() {
    setLoading(true);
    deleteAuction(id)
      .then((res) => {
        if (!res?.success) {
          throw response;
        }
        router.push("/");
      })
      .catch((error) => {
        toast.error(error?.message || "Unexpected runtime error!");
      })
      .finally(() => setLoading(false));
  }

  return (
    <Button
      onClick={handleDeleteAuction}
      text="Delete auction"
      loading={loading}
    />
  );
}
