import React from "react";
import { BsHouses } from "react-icons/bs";
import "./navbar.scss";
import Search from "./SearchBar/Search";

const Navbar = () => {
  return (
    <header className="header">
      <div className="header__body header__body__blue-box">
        <div className="header__body__logo-section">
          <BsHouses size={34} />
          <div className="">Auction House</div>
        </div>
        <div>
          <Search />
        </div>
        <div>right</div>
      </div>
    </header>
  );
};

export default Navbar;
