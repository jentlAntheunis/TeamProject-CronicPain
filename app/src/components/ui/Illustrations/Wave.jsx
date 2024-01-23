const Wave = ({
  className,
  opacity = 0.3,
  height = "auto",
  ...props
}) => (
  <svg
    width="1512"
    height="85"
    viewBox="0 0 1512 85"
    fill="none"
    xmlns="http://www.w3.org/2000/svg"
    style={{ width: "100%", height: height }}
    className={className}
    {...props}
  >
    <path
      d="M685.461 48.5879C1000.46 -30.4121 1306.5 -17.5 1610.5 126.5L1613.96 556.588L-88.5386 540.088C-126.539 341.921 0 1 0 1C95 55.5 370.461 127.588 685.461 48.5879Z"
      fill="#2987FB"
      fillOpacity={opacity}
    />
  </svg>
);

export default Wave;
