import React from "react";
import "./button.scss";

const Button = ({ text, onClick }) => {
  return (
    <button onClick={() => onClick} className="button" role="button">
      {text}
    </button>
  );
};

export default Button;
