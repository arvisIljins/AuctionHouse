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

const AuctionsList = () => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [pageCount, setPageCount] = useState(0);

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

  const url = queryString.stringifyUrl({ url: "", query: params });

  useEffect(() => {
    setData([]);
    getData(url).then((result) => {
      setData(result.data.items);
      setPageCount(result.data.pageCount);
      setLoading(false);
    });
  }, [url]);

  const showList = !loading && data.length > 0;
  return (
    <>
      <Filters />
      {loading && <Loader />}
      {showList && (
        <div className="list">
          {map(data, (auction) => {
            return <AuctionCard key={auction.title} auction={auction} />;
          })}
        </div>
      )}
      {!showList && <h1 className="no-results">There is no result found</h1>}
      {showList && <Pagination pageCount={pageCount} />}
    </>
  );
};

export default AuctionsList;
