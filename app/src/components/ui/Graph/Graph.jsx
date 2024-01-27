import { cva } from "class-variance-authority";
import styles from "./Graph.module.css";
import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  Tooltip,
  ResponsiveContainer,
  CartesianGrid,
} from "recharts";
import { LineChart, Line } from "recharts";
import InfoTooltip from "../InfoTooltip/InfoTooltip";
import clsx from "clsx";
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

const Graph = ({
  title,
  variant,
  data,
  tooltip,
  className,
  setShowModal,
  setModalContent,
}) => {
  return (
    <div className={clsx(styles.graphContainer, className)}>
      <div className={styles.titleContainer}>
        <div className={styles.graphTitle}>{title}</div>
        <InfoTooltip
          text={tooltip}
          onClick={() => {
            if (setShowModal) setShowModal(true);
            if (setModalContent)
              setModalContent({ title: title, text: tooltip });
          }}
        />
      </div>
      <div className={graphVariants({ variant })}>
        {data?.length > 0 ? (
          <GraphComponent variant={variant} data={data} />
        ) : (
          <div className={styles.noData}>Geen data beschikbaar</div>
        )}
      </div>
    </div>
  );
};

const GraphComponent = ({ variant, data }) => {
  if (variant === "bar") {
    return (
      <ResponsiveContainer width={"100%"} height={"100%"}>
        <BarChart
          data={data}
          margin={{
            top: 0,
            right: 12,
            left: 0,
            bottom: 0,
          }}
        >
          <XAxis
            dataKey="date"
            axisLine={false}
            tickLine={false}
            stroke="#94a3b8"
            tickMargin={10}
          />
          <YAxis
            axisLine={false}
            tickLine={false}
            stroke="#94a3b8"
            width={axisWidth}
          />
          <CartesianGrid stroke="#f1f5f9" strokeDasharray="5 3" />
          <Tooltip
            cursor={{ fill: "#f3f8fc" }}
            contentStyle={{
              borderRadius: "8px",
              boxShadow: "0 0 1rem 0 rgba(0, 0, 0, 0.05)",
              borderColor: "#f1f5f9",
            }}
            formatter={(value) => `${value} minuten`}
          />
          <Bar dataKey="Tijdsduur" fill="#3b82f6" radius={[8, 8, 0, 0]} />
        </BarChart>
      </ResponsiveContainer>
    );
  } else if (variant === "line") {
    return (
      <ResponsiveContainer width={"100%"} height={"100%"}>
        <LineChart
          width={500}
          height={300}
          data={data}
          margin={{
            top: 0,
            right: 12,
            left: 0,
            bottom: 0,
          }}
        >
          <XAxis
            dataKey="date"
            axisLine={false}
            tickLine={false}
            stroke="#94a3b8"
            tickMargin={10}
            interval="equidistantPreserveStart"
          />
          <YAxis
            axisLine={false}
            tickLine={false}
            stroke="#94a3b8"
            width={axisWidth}
            domain={[0, 10]}
            tickCount={(0, 2, 4, 6, 8, 10)}
          />
          <CartesianGrid stroke="#f1f5f9" strokeDasharray="5 3" />
          <Tooltip
            contentStyle={{
              borderRadius: "8px",
              boxShadow: "0 0 1rem 0 rgba(0, 0, 0, 0.05)",
              borderColor: "#f1f5f9",
            }}
          />
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
};

export default Graph;
