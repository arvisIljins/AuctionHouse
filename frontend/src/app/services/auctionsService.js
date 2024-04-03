"use server";

import { fetchWrapper } from "@/lib/fetchWrapper";
import { revalidatePath } from "next/cache";

export async function getData(url) {
  return await fetchWrapper.get(`search${url}`);
}

export async function updateAuction(data) {
  const res = await fetchWrapper.put("auctions", data);
  revalidatePath(`/auctions`);
  return res;
}

export async function createAuction(data) {
  return await fetchWrapper.post("auctions", data);
}

export async function getDetailsViewData(id) {
  return await fetchWrapper.get(`auctions/${id}`);
}

export async function deleteAuction(id) {
  return await fetchWrapper.del(`auctions/${id}`);
}

export async function getBidsById(id) {
  return await fetchWrapper.get(`bids/${id}`);
}
