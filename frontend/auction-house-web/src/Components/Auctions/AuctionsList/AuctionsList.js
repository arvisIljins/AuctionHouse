import React from "react";
import { AuctionCard } from "../AuctionCard/AuctionCard";
import { map } from "lodash";
import "./auctions-list.scss";

async function getData() {
  const res = await fetch("http://localhost:6001/search");

  if (!res.ok) throw new Error("Failed to fetch data");
  return res.json();
}
async function AuctionsList() {
  const result = await getData();
  return (
    <div className="list">
      {map(result.data.items, (auction) => {
        return <AuctionCard key={auction.title} auction={auction} />;
      })}
    </div>
  );
}

export default AuctionsList;
