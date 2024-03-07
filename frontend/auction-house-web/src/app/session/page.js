import React from "react";
import { getSession } from "../services/authService";
import "./session.scss";

export default async function Session() {
  const session = await getSession();
  return (
    <div className="session">
      <h3>Session data: {session.user.name}</h3>
    </div>
  );
}
