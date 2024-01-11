import Coin from "../ui/Icons/Coin.jsx";
import Streaks from "../ui/Icons/Streaks.jsx";
import RewardMetrics from "../ui/RewardMetrics/RewardMetrics.jsx";


const DashboardScreen = () => {
  return (
    <div>
      <RewardMetrics number={300}>
        <Coin />
      </RewardMetrics>
      <RewardMetrics number={2}>
        <Streaks />
      </RewardMetrics>
    </div>
  );
};

export default DashboardScreen;
