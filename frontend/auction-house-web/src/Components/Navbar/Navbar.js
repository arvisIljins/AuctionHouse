import React from "react";
import { BsHouses } from "react-icons/bs";
import "./navbar.styles.scss";

const Navbar = () => {
  return (
    <header>
      <div className="header">
        <BsHouses size={34} />
        <div className="">Auction House</div>
      </div>
      <div>midle</div>
      <div>right</div>
    </header>
  );
};

export default Navbar;
