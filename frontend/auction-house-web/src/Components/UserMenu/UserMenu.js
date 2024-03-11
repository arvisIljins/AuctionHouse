"use client";
import React from "react";
import "./user-menu.scss";
import Link from "next/link";
import { signOut } from "next-auth/react";

const UserMenu = ({ user }) => {
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
        <li>
          <Link href="/">My items</Link>
        </li>
        <li>
          <Link href="/auctions/create">Create Auction</Link>
        </li>
        <li>
          <Link href="/won">Auctions won</Link>
        </li>
        <li onClick={() => signOut({ callbackUrl: "/" })}>Sign out</li>
      </ul>
    </label>
  );
};

export default UserMenu;
