export const idProd = process.env.NODE_ENV === "production";

export const urls = {
  issuer: process.env.API_ISSUER,
  baseUrl: process.env.API_URL,
  notificationsUrl: idProd
    ? "https://api.auctions-house.com/notifications"
    : process.env.NEXT_PUBLIC_API_NOTIFICATIONS,
};
