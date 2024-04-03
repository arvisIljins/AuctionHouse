import Navbar from "@/components/navbar/Navbar";
import "./Styles/globals.scss";
import ToasterProvider from "./provaiders/ToasterProvider";

export const metadata = {
  title: "Auction House",
  description: "Find you dream house here",
};

export default function RootLayout({ children }) {
  return (
    <html lang="en">
      <body>
        <ToasterProvider />
        <Navbar />
        {children}
      </body>
    </html>
  );
}
