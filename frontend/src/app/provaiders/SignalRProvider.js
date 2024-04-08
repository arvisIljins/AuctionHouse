"use client";
import { useEffect, useState } from "react";
import { useAuctionStore } from "../hooks/UseAuctionStore";
import { useBidsStore } from "../hooks/UseBidsStore";
import { HubConnectionBuilder } from "@microsoft/signalr";
import toast from "react-hot-toast";
import { getDetailsViewData } from "../services/auctionsService";
import { urls } from "../constants";

const SignalRProvider = ({ children, user }) => {
  const [connection, setConnection] = useState(null);
  const setCurrentPrice = useAuctionStore((state) => state.setCurrentPrice);
  const addBid = useBidsStore((state) => state.addBid);

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
      .withUrl(urls.notificationsUrl)
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);
  }, []);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then(() => {
          connection.on("BidPlaced", (bid) => {
            if (bid.status.includes("Accepted")) {
              setCurrentPrice(bid.id, bid.amount);
            }
            addBid(bid);
          });

          connection.on("AuctionCreated", (auction) => {
            if (user?.username !== auction.seller) {
              toast.success(`New auction created - ${auction.title}`);
            }
          });

          connection.on("AuctionFinished", (req) => {
            const auction = getDetailsViewData(req.auctionId);
            if (user?.username !== auction.seller) {
              toast.success(`Auction finished - ${auction.title}`);
            }
          });
        })
        .catch((error) => console.log(error));
    }

    return () => {
      connection?.stop();
    };
  }, [connection, setCurrentPrice]);

  return children;
};

export default SignalRProvider;
