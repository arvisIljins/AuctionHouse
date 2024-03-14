import { getDetailsViewData } from "@/app/services/auctionsService";
import React from "react";
import "./details.scss";
import CustomImage from "@/Components/Image/CustomImage";
import CountdownTimer from "@/Components/CountdownTimer/CountdownTimer";

export default async function AuctionDetails({ params }) {
  const response = await getDetailsViewData(params.id);
  const data = response.data;
  return (
    <div className="details">
      <h1 className="details__title">{data.title}</h1>
      <CustomImage
        alt={data.title}
        imageUrl={data.imageUrl}
        width={150}
        height={150}
      />
      <p className="details__description">{data.description}</p>
      <p className="details__description">{data.tags}</p>
      <p className="details__description">Price: {data.reservePrice}</p>
      <CountdownTimer endDate={data.endDate} />
    </div>
  );
}
