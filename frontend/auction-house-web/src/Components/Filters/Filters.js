import React from "react";
import "./filters.scss";
import { map } from "lodash";

const Filters = (props) => {
  const { setSageSize, pageSize } = props;
  const pageSizeOptions = [4, 10, 20];
  return (
    <label class="dropdown">
      <div class="dd-button">Filters</div>
      <input type="checkbox" class="dd-input" id="test" />
      <ul class="dd-menu">
        <li>
          {map(pageSizeOptions, (item) => {
            return (
              <label
                className="form-control"
                onChange={() => setSageSize(item)}>
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
        <li>Something else here</li>
      </ul>
    </label>
  );
};

export default Filters;
