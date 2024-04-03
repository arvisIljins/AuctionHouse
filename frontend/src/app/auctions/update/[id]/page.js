import React from "react";
import "./update.scss";
import AuctionForm from "../../auctionForm/AuctionForm";
import { getDetailsViewData } from "@/app/services/auctionsService";
import { getCurrentUser } from "@/app/services/authService";

export default async function Update({ params }) {
  const response = await getDetailsViewData(params.id);
  const user = await getCurrentUser();
  const data = response.data;
  return (
    <section className="update">
      <h1 className="update__title">Update auction details</h1>
      <AuctionForm auction={data} />
    </section>
  );
}
