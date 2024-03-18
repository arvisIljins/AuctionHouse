"use client";
import React from "react";
import "./user-menu.scss";
import Link from "next/link";
import { signOut } from "next-auth/react";
import { usePathname, useRouter } from "next/navigation";
import { useParamsStore } from "@/app/Hooks/UseParamStore";

const UserMenu = ({ user }) => {
  const router = useRouter();
  const pathname = usePathname();
  const setParams = useParamsStore((state) => state.setParams);

  function setWinner() {
    setParams({ winner: user.username, seller: undefined });
    if (pathname !== "/") router.push("/");
  }

  function setSeller() {
    setParams({ seller: user.username, winner: undefined });
    if (pathname !== "/") router.push("/");
  }

  return (
    <label className="menu-wrapper">
      <div className="user-button">{user.name}</div>
      <input type="checkbox" className="user-input" />
      <ul className="user-menu">
        <li>
          <Link href="/session">Users session</Link>
        </li>
        <li>
          <Link href="/">Auctions list</Link>
        </li>
        <li onClick={() => setSeller()}>My items</li>
        <li>
          <Link href="/auctions/create">Create Auction</Link>
        </li>
        <li onClick={() => setWinner()}>Auctions won</li>
        <li onClick={() => signOut({ callbackUrl: "/" })}>Sign out</li>
      </ul>
    </label>
  );
};

export default UserMenu;
