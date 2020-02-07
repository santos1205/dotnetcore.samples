import React from "react";

const priceLabel = props => {
  const { status, valor } = props;
  if (status === "VENDIDO") return <strike>{`R$ ${valor}`}</strike>;
  else return `R$ ${valor}`;
};

export default priceLabel;
