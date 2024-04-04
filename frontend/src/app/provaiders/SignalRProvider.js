"use client";
import { useEffect, useState } from "react";
import { useAuctionStore } from "../hooks/UseAuctionStore";
import { useBidsStore } from "../hooks/UseBidsStore";
import { HubConnectionBuilder } from "@microsoft/signalr";
import { notificationsUrl } from "../constants";

const SignalRProvider = ({ children }) => {
  const [connection, setConnection] = useState(null);
  const setCurrentPrice = useAuctionStore((state) => state.setCurrentPrice);
  const addBid = useBidsStore((state) => state.addBid);

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
      .withUrl(notificationsUrl)
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
            console.log("Bid received");
            if (bid.status.includes("Accepted")) {
              setCurrentPrice(bid.id, bid.amount);
            }
            debugger;
            addBid(bid);
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
