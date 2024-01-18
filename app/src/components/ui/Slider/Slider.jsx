import { capitalize, spaceToBreak } from "../../../core/utils/formatText";
import styles from "./Slider.module.css";
import { useState } from "react";

const Slider = ({ max, value, setValue, minLabel, maxLabel }) => {
  const markers = Array.from(Array(max + 1).keys());

  const handleSliderChange = (event) => {
    setValue(parseInt(event.target.value));
  };

  return (
    <div className={styles.sliderWrapper}>
      <div className={styles.sliderContainer}>
        <div className={styles.markers}>
          {markers.map((marker) => {
            return (
              <span
                key={marker}
                className={styles.marker}
                style={{
                  opacity: value === marker ? 0 : 1,
                }}
              ></span>
            );
          })}
        </div>
        <input
          type="range"
          min="0"
          max={max}
          className={styles.slider}
          list="tickmarks"
          value={value}
          onChange={handleSliderChange}
        />
      </div>
      <div className={styles.labels}>
        <span className={styles.label}>
          {spaceToBreak(capitalize(minLabel))}
        </span>
        <span className={styles.label}>
          {spaceToBreak(capitalize(maxLabel))}
        </span>
      </div>
    </div>
  );
};

export default Slider;
