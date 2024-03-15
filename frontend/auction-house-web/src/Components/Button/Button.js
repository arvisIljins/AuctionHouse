"use client";
import React from "react";
import "./button.scss";

const Button = ({ text, onClick = null, disabled, loading }) => {
  return (
    <button
      disabled={disabled || loading}
      onClick={() => onClick()}
      className="button"
      role="button">
      {loading ? "Loading.." : text}
    </button>
  );
};

export default Button;
