import NextAuth from "next-auth";
import DuendeIDS6Provider from "next-auth/providers/duende-identity-server6";

export const authOptions = {
  session: {
    strategy: "jwt",
  },
  providers: [
    DuendeIDS6Provider({
      id: "id-server",
      clientId: "clientApp",
      clientSecret: "secret",
      issuer: "http://localhost:5001",
      authorization: { params: { scope: "openid profile auctionApp" } },
      idToken: true,
    }),
  ],
};

const handler = NextAuth(authOptions);

export { handler as GET, handler as POST };