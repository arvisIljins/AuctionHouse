"use server";
export async function getData(selectedPage, pageSize) {
  const res = await fetch(
    `http://localhost:6001/search?pageSize=${pageSize}&pageNumber=${selectedPage}`
  );

  if (!res.ok) throw new Error("Failed to fetch data");
  return res.json();
}
