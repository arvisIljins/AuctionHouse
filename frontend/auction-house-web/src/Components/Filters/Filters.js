import React from "react";
import "./filters.scss";
import { map } from "lodash";
import { useParamsStore } from "@/app/Hooks/UseParamStore";

const Filters = () => {
  const pageSize = useParamsStore((state) => state.pageSize);
  const setParams = useParamsStore((state) => state.setParams);
  const resetFilters = useParamsStore((state) => state.reset);
  const pageSizeOptions = [3, 6, 12];
  return (
    <label class="dropdown">
      <div class="dd-button">Filters</div>
      <input type="checkbox" class="dd-input" />
      <ul class="dd-menu">
        <li>
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
        <li>Another action</li>
        <li onClick={() => resetFilters()}>Delete filters</li>
      </ul>
    </label>
  );
};

export default Filters;
