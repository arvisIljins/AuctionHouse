import React from "react";
import "./filters.scss";
import { map } from "lodash";
import { useParamsStore } from "@/app/Hooks/UseParamStore";

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
    <label class="dropdown">
      <div class="dd-button">Filters</div>
      <input type="checkbox" class="dd-input" />
      <ul class="dd-menu">
        <li>
          Items in page:
          {map(pageSizeOptions, (item) => {
            return (
              <label
                className="form-control"
                onChange={() => setParams({ pageSize: item })}>
                <input
                  type="checkbox"
                  name="checkbox"
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
              <label
                className="form-control"
                onChange={() => setParams({ orderBy: item.value })}>
                <input
                  type="checkbox"
                  name="checkbox"
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
              <label
                className="form-control"
                onChange={() => setParams({ filterBy: item.value })}>
                <input
                  type="checkbox"
                  name="checkbox"
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
