"use server";
export async function getData(url) {
  const res = await fetch(`http://localhost:6001/search${url}`);

  if (!res.ok) throw new Error("Failed to fetch data");
  return res.json();
}
