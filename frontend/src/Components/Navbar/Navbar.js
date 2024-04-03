import React from "react";
import { BsHouses } from "react-icons/bs";
import "./navbar.scss";
import Search from "./SearchBar/Search";
import LoginButton from "./LoginButton/LoginButton";
import UserMenu from "../userMenu/UserMenu";
import { getCurrentUser } from "@/app/services/authService";
import Link from "next/link";

export default async function Navbar() {
  const user = await getCurrentUser();
  return (
    <header className="header">
      <div className="header__body header__body__blue-box">
        <Link href="/">
          <div className="header__body__logo-section">
            <BsHouses size={34} />
            <div className="">Auction House</div>
          </div>
        </Link>
        <div>
          <Search />
        </div>
        <div>{user?.name ? <UserMenu user={user} /> : <LoginButton />}</div>
      </div>
    </header>
  );
}
