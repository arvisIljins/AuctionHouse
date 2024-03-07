"use client";
import React from "react";
import "./user-menu.scss";
import Link from "next/link";

const UserMenu = ({ user }) => {
  return (
    <label className="menu-wrapper">
      <div className="user-button">{user.name}</div>
      <input type="checkbox" className="user-input" />
      <ul className="user-menu">
        <li>
          <Link href="/session">Users session</Link>
        </li>
      </ul>
    </label>
  );
};

export default UserMenu;
