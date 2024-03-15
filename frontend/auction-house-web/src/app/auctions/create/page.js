import React from "react";
import "./create.scss";
import AuctionForm from "../AuctionForm/AuctionForm";

const Create = () => {
  return (
    <section className="create">
      <h1 className="create__title">Create</h1>
      <AuctionForm />
    </section>
  );
};

export default Create;
