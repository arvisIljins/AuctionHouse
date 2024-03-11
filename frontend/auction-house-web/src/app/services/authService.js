import { getServerSession } from "next-auth/next";
import { authOptions } from "../api/auth/[...nextauth]/route";
import { getToken } from "next-auth/jwt";
import { headers, cookies } from "next/headers";

export async function getSession() {
  return getServerSession(authOptions);
}

export async function getCurrentUser() {
  try {
    const session = await getSession();

    if (!session) return null;

    return session.user;
  } catch (error) {
    console.error(error);
    return null;
  }
}

export async function getToken() {
  const req = {
    headers: Object.fromEntries(headers()),
    cookies: Object.fromEntries(
      cookies()
        .getAll()
        .map((c) => [c.name, c.value])
    ),
  };

  return await getToken({ req });
}
