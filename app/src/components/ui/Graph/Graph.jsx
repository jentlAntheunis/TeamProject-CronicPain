import { cva } from "class-variance-authority";
import styles from "./Graph.module.css";
import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  Tooltip,
  ResponsiveContainer,
} from "recharts";
import { LineChart, Line } from "recharts";
import InfoTooltip from "../InfoTooltip/InfoTooltip";
import { useEffect } from "react";
import { fillMissingDates } from "../../../core/utils/patientDetails";
const lineData = [];

for (let i = 0; i < 30; i++) {
  const startDate = new Date();
  const currentDate = new Date(startDate);
  currentDate.setDate(startDate.getDate() + i);
  lineData.push({
    name: currentDate.toLocaleDateString("en-GB", {
      day: "2-digit",
      month: "2-digit",
    }),
    Pijn: Math.floor(Math.random() * 11),
  });
}

const barData = [
  {
    name: "Ma",
    Tijdsduur: Math.floor(Math.random() * 60),
  },
  {
    name: "Di",
    Tijdsduur: Math.floor(Math.random() * 60),
  },
  {
    name: "Wo",
    Tijdsduur: Math.floor(Math.random() * 60),
  },
  {
    name: "Do",
    Tijdsduur: Math.floor(Math.random() * 60),
  },
  {
    name: "Vr",
    Tijdsduur: 0,
  },
  {
    name: "Za",
    Tijdsduur: Math.floor(Math.random() * 60),
  },
  {
    name: "Zo",
    Tijdsduur: Math.floor(Math.random() * 60),
  },
];

let axisWidth;
const biggestTijdsduur = Math.max(...barData.map((data) => data.Tijdsduur));
if (biggestTijdsduur < 100) {
  axisWidth = 30;
} else {
  axisWidth = 40;
}

const graphVariants = cva(styles.graph, {
  variants: {
    variant: {
      bar: styles.bar,
      line: styles.line,
    },
  },
  defaultVariants: {
    variant: "bar",
  },
});

const Graph = ({ title, variant, data }) => {
  let graphComponent;
  let graphInfo;

  useEffect(() => {
    if (variant === "line") {
      console.log(data);
    }
  }, [data, variant]);

  if (variant === "bar") {
    graphInfo =
      "Deze grafiek geeft de duur van de bewegingssessies weer per dag van de week uitgedrukt in minuten.";
    graphComponent = (
      <ResponsiveContainer width={"100%"} height={"100%"}>
        <BarChart
          data={barData}
          margin={{
            top: 0,
            right: 0,
            left: 0,
            bottom: 0,
          }}
        >
          <XAxis
            dataKey="name"
            axisLine={false}
            tickLine={false}
            stroke="#94a3b8"
          />
          <YAxis
            axisLine={false}
            tickLine={false}
            stroke="#94a3b8"
            width={axisWidth}
          />
          <Tooltip cursor={{ fill: "#f1f5f9" }} />
          <Bar dataKey="Tijdsduur" fill="#3b82f6" radius={[8, 8, 0, 0]} />
        </BarChart>
      </ResponsiveContainer>
    );
  } else if (variant === "line") {
    graphInfo =
      "Deze grafiek geeft de pijnervaring weer op een schaal van 0 tot 10.";
    graphComponent = (
      <ResponsiveContainer width={"100%"} height={"100%"}>
        <LineChart width={500} height={300} data={data}>
          <XAxis
            dataKey="date"
            axisLine={false}
            tickLine={false}
            stroke="#94a3b8"
          />
          <YAxis
            axisLine={false}
            tickLine={false}
            stroke="#94a3b8"
            width={axisWidth}
            domain={[0, 10]}
            tickCount={(0, 2, 4, 6, 8, 10)}
          />
          <Tooltip />
          <Line
            type="monotone"
            dataKey="Pijn"
            stroke="#3b82f6"
            strokeWidth={4}
            dot={false}
            connectNulls
          />
        </LineChart>
      </ResponsiveContainer>
    );
  }

  return (
    <div className={styles.graphContainer}>
      <div className={styles.titleContainer}>
        <div className={styles.graphTitle}>{title}</div>
        <InfoTooltip text={graphInfo} />
      </div>
      <div className={graphVariants({ variant })}>{graphComponent}</div>
    </div>
  );
};

export default Graph;
