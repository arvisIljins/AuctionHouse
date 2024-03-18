import Link from "next/link";
import React from "react";
import "./restricted.scss";

const Restricted = () => {
  return (
    <div className="restricted">
      <h1>Login to access this page!</h1>

      <Link href="/">Go back to home page</Link>
    </div>
  );
};

export default Restricted;
