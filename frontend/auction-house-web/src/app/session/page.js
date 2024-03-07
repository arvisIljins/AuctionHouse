import React from "react";
import { getSession } from "../services/authService";
import "./session.scss";

export default async function Session() {
  const session = await getSession();
  return (
    <div className="session">
      <h1>Session data</h1>
      <h3>Name: {session.user.name}</h3>
      <h3>Login: {session.user.username}</h3>
    </div>
  );
}
