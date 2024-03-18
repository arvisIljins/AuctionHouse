"use client";
import Image from "next/image";
import React, { useState } from "react";
import "./image.scss";

const CustomImage = ({ imageUrl, alt, className, width, height }) => {
  const [isLoading, setIsLanding] = useState(true);
  return (
    <div className="image-container">
      <Image
        alt={alt}
        className={isLoading ? "image" : className}
        src={imageUrl}
        width={width}
        height={height}
        onLoadingComplete={() => setIsLanding(false)}
      />
    </div>
  );
};

export default CustomImage;
