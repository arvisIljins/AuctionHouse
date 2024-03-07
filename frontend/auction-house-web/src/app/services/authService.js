import { getServerSession } from "next-auth/next";
import { authOptions } from "../api/auth/[...nextauth]/route";

export async function getSession() {
  return getServerSession(authOptions);
}

export async function getCurrentUser() {
  try {
    const session = await getSession();
    console.log({ session });

    if (!session) return null;

    return session.user;
  } catch (error) {
    console.error(error);
    return null;
  }
}
