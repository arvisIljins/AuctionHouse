import { getDetailsViewData } from "@/app/services/auctionsService";
import React from "react";
import "./details.scss";
import CustomImage from "@/Components/Image/CustomImage";
import CountdownTimer from "@/Components/CountdownTimer/CountdownTimer";
import Link from "next/link";
import Button from "@/Components/Button/Button";
import { getCurrentUser } from "@/app/services/authService";

export default async function AuctionDetails({ params }) {
  const response = await getDetailsViewData(params.id);
  const user = await getCurrentUser();
  const data = response.data;
  const showEditButton = user.username === data.seller;
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
      <p className="details__description details__description__margin">
        Price: {data.reservePrice}
      </p>
      <div className="details__description__margin">
        <CountdownTimer endDate={data.endDate} />
      </div>
      {showEditButton && (
        <Link href={`/auctions/update/${params.id}`}>
          <Button text="Edit auction" type="submit" />
        </Link>
      )}
    </div>
  );
}
