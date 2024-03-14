"use server";

import { fetchWrapper } from "@/Lib/fetchWrapper";

export async function getData(url) {
  return await fetchWrapper.get(`search${url}`);
}

export async function updateAuction(id, data) {
  return await fetchWrapper.put(`auctions/${id}`, data);
}

export async function createAuction(data) {
  return await fetchWrapper.post("auctions", data);
}
