"use client";
import React, { useState } from "react";
import "./search.scss";
import { IoIosSearch } from "react-icons/io";
import { useParamsStore } from "@/app/Hooks/UseParamStore";
import { usePathname, useRouter } from "next/navigation";

const Search = () => {
  const router = useRouter();
  const pathname = usePathname();
  const setParams = useParamsStore((state) => state.setParams);
  const setSearch = useParamsStore((state) => state.setSearch);
  const searchValue = useParamsStore((state) => state.searchValue);
  const handleSearch = () => {
    setParams({ searchTerm: searchValue });
    if (pathname !== "/") {
      router.push("/");
    }
  };
  return (
    <div className="search-container">
      <input type="text" name="password" hidden />
      <input
        value={searchValue}
        onKeyDown={(e) => {
          if (e.key === "Enter") handleSearch();
        }}
        type="text"
        name="search"
        placeholder="Search..."
        className="search-input"
        onChange={(e) => setSearch(e.target.value)}
      />
      <a onClick={() => handleSearch()} className="search-button">
        <IoIosSearch />
      </a>
    </div>
  );
};

export default Search;
