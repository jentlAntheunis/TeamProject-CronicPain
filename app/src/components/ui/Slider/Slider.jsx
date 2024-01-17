import styles from "./Slider.module.css";
import { useState } from "react";

const Slider = ({ max, setSliderValue  }) => {
  const [value, setValue] = useState(1);
  const markers = Array.from({ length: max }, (_, index) => index + 1);

  const handleSliderChange = (event) => {
    setValue(parseInt(event.target.value));
    setSliderValue(parseInt(event.target.value));
  };

  return (
    <div className={styles.sliderContainer}>
      <div className={styles.markers}>
        {markers.map((marker) => {
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
        value={value}
        onChange={handleSliderChange}
      />
    </div>
  );
};

export default Slider;
