"use client";
import React from "react";
import "./pagination.scss";
import { map } from "lodash";
const Pagination = (props) => {
  const { selectedPage, pageCount, setSelectedPage } = props;
  const pages = Array.from({ length: pageCount }, (_, i) => i + 1);
  return (
    <div className="container">
      <ul className="container__pagination">
        <li className="container__pagination__li">
          <a onClick={() => setSelectedPage(selectedPage - 1)}>Prev</a>
        </li>
        {map(pages, (item) => {
          return (
            <li
              key={item}
              className={`container__pagination__li ${
                item === selectedPage && "container__pagination__active"
              }`}>
              <a onClick={() => setSelectedPage(item)}>{item}</a>
            </li>
          );
        })}
        <li className="container__pagination__li">
          <a onClick={() => setSelectedPage(selectedPage + 1)}>Next</a>
        </li>
      </ul>
    </div>
  );
};

export default Pagination;
