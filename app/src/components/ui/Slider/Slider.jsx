import styles from "./Slider.module.css";
import { useState } from "react";

const Slider = ({ max }) => {
  const [value, setValue] = useState(1); // Add state for the slider value
  const markers = Array.from({ length: max }, (_, index) => index + 1);

  console.log("value", value)
  console.log("markers", markers)

  return (
    <div className={styles.sliderContainer}>
      <div className={styles.markers}>
        {markers.map((marker) => {
          console.log(marker === value)
          return (
          <span
            key={marker}
            className={styles.marker}
            style={{
              opacity: value === marker ? 0 : 1
            }}
          ></span>
          )
          })}
      </div>
      <input
        type="range"
        min="1"
        max={max}
        className={styles.slider}
        list="tickmarks"
        value={value} // Bind the value to the state
        onChange={(e) => setValue(parseInt(e.target.value))} // Update the state on slider change
      />
    </div>
  );
};

export default Slider;
