import React from "react";
import "./filters.scss";
import { map } from "lodash";
import { useParamsStore } from "@/app/hooks/UseParamStore";

const Filters = () => {
  const pageSize = useParamsStore((state) => state.pageSize);
  const setParams = useParamsStore((state) => state.setParams);
  const resetFilters = useParamsStore((state) => state.reset);
  const orderBy = useParamsStore((state) => state.orderBy);
  const filteredBy = useParamsStore((state) => state.filterBy);
  const pageSizeOptions = [3, 6, 12];
  const orderButton = [
    { title: "Created at", value: "createdAt" },
    { title: "Updated at", value: "updatedAt" },
    { title: "Reserve price", value: "reservePrice" },
  ];
  const filterButton = [
    { title: "Finished", value: "finished" },
    { title: "Ending soon", value: "endingSoon" },
  ];
  return (
    <label className="dropdown">
      <div className="dd-button">Filters</div>
      <input type="checkbox" className="dd-input" />
      <ul className="dd-menu">
        <li>
          Items in page:
          {map(pageSizeOptions, (item) => {
            return (
              <label key={item.title} className="form-control">
                <input
                  type="checkbox"
                  name="checkbox"
                  onChange={() => setParams({ pageSize: item })}
                  checked={pageSize === item}
                />
                {item}
              </label>
            );
          })}
        </li>
        <li>
          Sort by:
          {map(orderButton, (item) => {
            return (
              <label key={item.title} className="form-control">
                <input
                  type="checkbox"
                  name="checkbox"
                  onChange={() => setParams({ orderBy: item.value })}
                  checked={orderBy === item.value}
                />
                {item.title}
              </label>
            );
          })}
        </li>
        <li>
          Filter by:
          {map(filterButton, (item) => {
            return (
              <label key={item.title} className="form-control">
                <input
                  type="checkbox"
                  name="checkbox"
                  onChange={() => setParams({ orderBy: item.value })}
                  checked={filteredBy === item.value}
                />
                {item.title}
              </label>
            );
          })}
        </li>
        <li onClick={() => resetFilters()}>Delete filters</li>
      </ul>
    </label>
  );
};

export default Filters;
