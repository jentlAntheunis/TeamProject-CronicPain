import { useNavigate } from "react-router-dom";
import PageHeading from "../../../ui/PageHeading/PageHeading";
import ScrollableScreen from "../../../ui/ScrollableScreen/ScrollableScreen";

const ExtraInfoScreen = () => {
  const navigate = useNavigate();

  return (
    <ScrollableScreen>
      <PageHeading onBack={() => navigate(-1)}>Extra informatie</PageHeading>
    </ScrollableScreen>
  );
};

export default ExtraInfoScreen;
