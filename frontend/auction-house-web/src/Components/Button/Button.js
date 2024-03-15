"use client";
import React from "react";
import "./button.scss";

const Button = ({ text, onClick = null, disabled }) => {
  return (
    <button
      disabled={disabled}
      onClick={() => onClick}
      className="button"
      role="button">
      {text}
    </button>
  );
};

export default Button;
