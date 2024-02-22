import Navbar from "@/Components/Navbar/Navbar";

export const metadata = {
  title: "Auction House",
  description: "Find you dream house here",
};

export default function RootLayout({ children }) {
  return (
    <html lang="en">
      <body>
        <Navbar />
        <main className="container mx-auto p-5 pt-10">{children}</main>
      </body>
    </html>
  );
}
