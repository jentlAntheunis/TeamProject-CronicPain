import { useNavigate } from "react-router-dom";
import PageHeading from "../../../ui/PageHeading/PageHeading";
import ScrollableScreen from "../../../ui/ScrollableScreen/ScrollableScreen";
import HowestLogo from "../../../ui/Logos/HowestLogo";
import styles from "./ExtraInfoScreen.module.css";

const ExtraInfoScreen = () => {
  const navigate = useNavigate();

  return (
    <ScrollableScreen>
      <PageHeading onBack={() => navigate(-1)}>Extra informatie</PageHeading>
      <h2 className={styles.title}>Over ons</h2>
      <p>
        Welkom op de &quot;Extra Info&quot; pagina van ons project! Wij zijn een
        gepassioneerd groepje MCT-studenten, gevestigd in België. Voor het vak
        Team Project hebben we de opdracht gekregen om een innovatieve
        applicatie te ontwikkelen die specifiek is ontworpen voor chronische
        pijnpatiënten.
      </p>
      <h2 className={styles.title}>Het team</h2>
      <ul>
        <li>Thibault Feraux - Student AI Engineer</li>
        <li>Lien De Jong - Student AI Engineer</li>
        <li>Juul Van de Velde - Student Next Web Developer</li>
        <li>Jentl Antheunis - Student Smart XR Developer</li>
      </ul>
      <h2 className={styles.title}>Ons project: Pebbles</h2>
      <p>
        Ons project, genaamd Pebbles, heeft als doel een gebruiksvriendelijk
        platform te bieden dat niet alleen bewegingsstatistieken, maar ook de
        mentale toestand van patiënten bijhoudt, zoals hun emotionele welzijn en
        motivatie om te bewegen.
      </p>
      <p>
        We geloven sterk in het integreren van gamification-elementen om
        patiënten aan te moedigen consistent hun gegevens vast te leggen en
        actief deel te nemen aan de app. Door dit te doen, streven we ernaar het
        proces niet alleen functioneel, maar ook boeiend en plezierig te maken.
      </p>
      <p>
        Onze applicatie is ontworpen met het oog op multidisciplinaire
        ondersteuning. Hulpverleners hebben de mogelijkheid om gemakkelijk
        vragen toe te voegen aan de applicatie. Bovendien kunnen ze de
        verzamelde data overzichtelijk bekijken, waardoor ze de voortgang van
        elke patiënt effectief kunnen monitoren.
      </p>
      <p>
        Voor de patiënten hebben we een persoonlijk traject gecreëerd. Dit
        traject biedt niet alleen inzicht in hun eigen data, maar stimuleert hen
        ook om hun begrip van pijn te verbeteren en hen aan te moedigen met
        vertrouwen te blijven bewegen.
      </p>
      <div className={styles.credits}>
        Mogelijk gemaakt door <HowestLogo className={styles.howestLogo} />
      </div>
    </ScrollableScreen>
  );
};

export default ExtraInfoScreen;
