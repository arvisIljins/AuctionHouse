"use client";
import React from "react";
import "./pagination.scss";
import { map } from "lodash";
import { useParamsStore } from "@/app/hooks/UseParamStore";
import { shallow } from "zustand/shallow";
const Pagination = ({ pageCount }) => {
  const params = useParamsStore(
    (state) => ({
      pageNumber: state.pageNumber,
    }),
    shallow
  );
  const setParams = useParamsStore((state) => state.setParams);
  const setPageNumber = (pageNumber) => {
    setParams({ pageNumber });
  };
  const pages = Array.from({ length: pageCount }, (_, i) => i + 1);
  return (
    <div className="container">
      <ul className="container__pagination">
        <li className="container__pagination__li">
          <a onClick={() => setPageNumber(params.pageNumber - 1)}>Prev</a>
        </li>
        {map(pages, (item) => {
          return (
            <li
              key={item}
              className={`container__pagination__li ${
                item === params.pageNumber && "container__pagination__active"
              }`}>
              <a onClick={() => setPageNumber(item)}>{item}</a>
            </li>
          );
        })}
        <li className="container__pagination__li">
          <a onClick={() => setPageNumber(params.pageNumber + 1)}>Next</a>
        </li>
      </ul>
    </div>
  );
};

export default Pagination;
