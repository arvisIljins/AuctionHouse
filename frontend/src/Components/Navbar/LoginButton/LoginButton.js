"use client";
import React from "react";
import "./login-button.scss";
import { signIn } from "next-auth/react";
const LoginButton = () => {
  return (
    <button
      onClick={() =>
        signIn("id-server", { callbackUrl: "/" }, { prompt: "login" })
      }
      className="button"
      role="button">
      Login
    </button>
  );
};

export default LoginButton;
