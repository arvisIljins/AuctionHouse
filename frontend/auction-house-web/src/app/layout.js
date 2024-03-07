import Navbar from "@/Components/Navbar/Navbar";
import "./Styles/globals.scss";

export const metadata = {
  title: "Auction House",
  description: "Find you dream house here",
};

export default function RootLayout({ children }) {
  return (
    <html lang="en">
      <body>
        <Navbar />
        {children}
      </body>
    </html>
  );
}
