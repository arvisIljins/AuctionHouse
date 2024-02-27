"use server";
export async function getData(selectedPage) {
  const res = await fetch(
    `http://localhost:6001/search?pageSize=10&pageNumber=${selectedPage}`
  );

  if (!res.ok) throw new Error("Failed to fetch data");
  return res.json();
}
