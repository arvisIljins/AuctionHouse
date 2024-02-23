import React from "react";
import "./auction-card.scss";
import CountdownTimer from "@/Components/CountdownTimer/CountdownTimer";
import CustomImage from "@/Components/Image/CustomImage";

export const AuctionCard = ({ auction }) => {
  const { reservePrice, imageUrl, description, tags, title, endDate } = auction;
  return (
    <div className="cards">
      <div className="cards__container cards__blue-box">
        <div className="cards__row">
          <CustomImage
            alt={title}
            className="cards__image"
            imageUrl={imageUrl}
            width={150}
            height={150}
          />
          <div className="cards__column">
            <p className="cards__title">{title}</p>
            <p className="cards__description">{description}</p>
          </div>
        </div>
        <p className="cards__description">{tags}</p>
        <p className="cards__description">Price: {reservePrice}</p>
        <CountdownTimer endDate={endDate} />
      </div>
    </div>
  );
};
