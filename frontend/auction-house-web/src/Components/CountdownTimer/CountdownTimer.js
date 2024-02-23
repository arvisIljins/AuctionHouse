"use client";
import React from "react";
import Countdown, { zeroPad } from "react-countdown";
import "./count-down-timer.scss";

const renderer = ({ days, hours, minutes, seconds, completed }) => {
  let className = "timer__success";
  if (completed) {
    className = "timer__competed";
  }
  if (!completed && days === 0 && hours < 10) {
    className = "timer__warning";
  }
  return (
    <div className={`timer ${className}`}>
      {completed ? (
        <span>Auction finished</span>
      ) : (
        <span suppressHydrationWarning={true}>
          {zeroPad(days)}:{zeroPad(hours)}:{zeroPad(minutes)}:{zeroPad(seconds)}
        </span>
      )}
    </div>
  );
};

export default function CountdownTimer({ endDate }) {
  return (
    <div>
      <Countdown date={endDate} renderer={renderer} />
    </div>
  );
}
