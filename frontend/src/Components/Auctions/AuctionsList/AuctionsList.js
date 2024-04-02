"use client";
import React, { useEffect, useState } from "react";
import { AuctionCard } from "../AuctionCard/AuctionCard";
import { map } from "lodash";
import "./auctions-list.scss";
import Pagination from "@/Components/Pagintaion/Pagination";
import { getData } from "@/app/services/auctionsService";
import Filters from "@/Components/Filters/Filters";
import { useParamsStore } from "@/app/Hooks/UseParamStore";
import { shallow } from "zustand/shallow";
import queryString from "query-string";
import Loader from "@/Components/Loader/Loader";
import { useAuctionStore } from "@/app/Hooks/UseAuctionStore";

const AuctionsList = () => {
  const [loading, setLoading] = useState(true);
  const params = useParamsStore(
    (state) => ({
      pageNumber: state.pageNumber,
      pageSize: state.pageSize,
      searchTerm: state.searchTerm,
      orderBy: state.orderBy,
      filterBy: state.filterBy,
      seller: state.seller,
      winner: state.winner,
    }),
    shallow
  );

  const data = useAuctionStore(
    (state) => ({
      auctions: state.auctions,
      totalCount: state.totalCount,
      pageCount: state.pageCount,
    }),
    shallow
  );

  const url = queryString.stringifyUrl({ url: "", query: params });

  const setData = useAuctionStore((state) => state.setData);

  useEffect(() => {
    setData([]);
    getData(url).then((result) => {
      setData(result.data);
      setLoading(false);
    });
  }, [url]);

  const showList = !loading && data.auctions.length > 0;
  console.log(data);
  return (
    <>
      <Filters />
      {loading && <Loader />}
      {showList && (
        <div className="list">
          {map(data.auctions, (auction) => {
            return <AuctionCard key={auction.title} auction={auction} />;
          })}
        </div>
      )}
      {!showList && <h1 className="no-results">There is no result found</h1>}
      {showList && <Pagination pageCount={data.pageCount} />}
    </>
  );
};

export default AuctionsList;
