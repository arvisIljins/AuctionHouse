import Navbar from "@/components/navbar/Navbar";
import "./Styles/globals.scss";
import ToasterProvider from "./provaiders/ToasterProvider";
import SignalRProvider from "./provaiders/SignalRProvider";

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
        <SignalRProvider>{children}</SignalRProvider>
      </body>
    </html>
  );
}
