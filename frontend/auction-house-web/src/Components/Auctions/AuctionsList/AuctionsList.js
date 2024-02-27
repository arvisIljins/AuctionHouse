"use client";
import React, { useEffect, useState } from "react";
import { AuctionCard } from "../AuctionCard/AuctionCard";
import { map } from "lodash";
import "./auctions-list.scss";
import Pagination from "@/Components/Pagintaion/Pagination";
import { getData } from "@/app/services/auctionsService";
import Filters from "@/Components/Filters/Filters";

const AuctionsList = () => {
  const [auctions, setAuctions] = useState([]);
  const [selectedPage, setSelectedPage] = useState(1);
  const [pageCount, setPageCount] = useState(0);

  useEffect(() => {
    setAuctions([]);
    getData(selectedPage).then((result) => {
      setAuctions(result.data.items);
      setPageCount(result.data.pageCount);
    });
  }, [selectedPage]);
  return (
    <>
      {/* <Filters /> */}
      <div className="list">
        {map(auctions, (auction) => {
          return <AuctionCard key={auction.title} auction={auction} />;
        })}
      </div>
      <Pagination
        pageCount={pageCount}
        selectedPage={selectedPage}
        setSelectedPage={setSelectedPage}
      />
    </>
  );
};

export default AuctionsList;
