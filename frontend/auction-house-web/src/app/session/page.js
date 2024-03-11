import React from "react";
import { getSession, handleGetToken } from "../services/authService";
import "./session.scss";

export default async function Session() {
  const session = await getSession();
  const token = await handleGetToken();
  return (
    <div className="session">
      <h1>Session data</h1>
      <h3>Name: {session.user.name}</h3>
      <h3>Login: {session.user.username}</h3>
      <h3 className="token">Token: {token.access_token}</h3>
    </div>
  );
}
