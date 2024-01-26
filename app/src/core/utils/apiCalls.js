import axios from "axios";
import request from "./request";
import QuestionCategories from "../config/questionCategories";

/**
 * Authentication API calls
 */
const getUser = async (email) =>
  await request({
    url: "/users/loginbyemail",
    method: "POST",
    data: email,
    headers: {
      "Content-Type": "application/json",
    },
  });

const checkIfUserExists = async (email) => {
  const { data } = await axios.get(
    import.meta.env.VITE_API_URL + "/users/exists/" + email
  );
  return data;
};

const storePatient = async ({ firstName, lastName, email, specialistId }) =>
  await request({
    url: `/specialists/${specialistId}/patients`,
    method: "POST",
    data: { firstName, lastName, email },
  });

const storePatientList = async ({ patients, specialistId }) => await request({
  url: `/specialists/${specialistId}/patients/addlist`,
  method: "POST",
  data: patients,
})

const sendMailToPatient = async ({
  firstName,
  lastName,
  email,
  specialistId,
}) =>
  await request({
    url: "/specialists/send-email/" + specialistId,
    method: "POST",
    data: { firstName, lastName, email },
  });

/**
 * User API calls
 */
const getUserData = async (userId) =>
  await request({
    url: "/patients/" + userId,
    method: "GET",
  });

const getPatients = async (userId) =>
  await request({
    url: "/specialists/" + userId + "/patients",
    method: "GET",
  });

/**
 * Questionnaire API calls
 */
const getQuestionnaire = async (userId, questionnaireType, category) => {
  const result = await request({
    url: `/questionnaires/${questionnaireType}/${userId}`,
    method: "GET",
  });
  return {
    ...result,
    data: {
      ...result.data,
      category: category,
    },
  };
};

const getMovementQuestionnaire = async (userId) => {
  return getQuestionnaire(
    userId,
    "movementquestionnaire",
    QuestionCategories.Movement
  );
};

const getBonusQuestionnaire = async (userId) => {
  return getQuestionnaire(
    userId,
    "bonusquestionnaire",
    QuestionCategories.Bonus
  );
};

const getDailyQuestionnaire = async (userId) => {
  return getQuestionnaire(
    userId,
    "dailypainquestionnaire",
    QuestionCategories.Daily
  );
};

const sendAnswers = async (data) =>
  await request({
    url: "/answers",
    method: "POST",
    data: data,
  });

/**
 * Coins and Streaks API calls
 */
const addCoins = async (userId, amount) =>
  await request({
    url: `/patients/${userId}/addcoins/${amount}`,
    method: "PUT",
  });

const getStreakHistory = async (userId) =>
  await request({
    url: `/patients/${userId}/streakhistory`,
    method: "GET",
  });

const checkStreak = async (userId) => await request({
  url: `/patients/${userId}/checkstreak`,
  method: "PUT",
});

/**
 * Shop API calls
 */
const getShopItems = async (userId) =>
  await request({
    url: `/store/${userId}/byprice`,
    method: "GET",
  });

const buyColor = async (userId, itemId) =>
  await request({
    url: `/store/${userId}/buy/${itemId}`,
    method: "PUT",
  });

const activateColor = async (userId, itemId) =>
  await request({
    url: `/store/${userId}/use/${itemId}`,
    method: "PUT",
  });

const getPebblesMood = async (userId) =>
  await request({
    url: `/patients/${userId}/pebblesmood`,
    method: "GET",
  });

/**
 * Movement API calls
 */
const storeMovement = async (userId, totalTime) =>
  await request({
    url: `/patients/${userId}/movementsessions`,
    method: "POST",
    data: { seconds: totalTime },
  });

/**
 * Questionnaire Details from Patient API calls
 */
const getImpact = async (userId) => await request({
  url: `/answers/user/${userId}/impacts`,
  method: 'GET',
})

const getMovementWeek = async (userId) => await request({
  url: `/patients/${userId}/movementtimeweek`,
  method: 'GET',
})

const getPainMonth = async (userId) => await request({
  url: `/patients/${userId}/painhistory`,
  method: 'GET',
})

const getQuestionnaires = async (userId) => await request({
  url: `/patients/${userId}/questionnaires`,
  method: 'GET',
})

/**
 * Question API calls
 */
const getScales = async () => await request({
  url: `/scale/all`,
  method: 'GET',
})

const getCategories = async () => await request({
  url: `/category/all`,
  method: 'GET',
})

const addQuestion = async (data) => await request({
  url: `/question/addquestion`,
  method: 'POST',
  data: data,
})


export {
  getUser,
  checkIfUserExists,
  storePatient,
  storePatientList,
  sendMailToPatient,
  getUserData,
  getPatients,
  getMovementQuestionnaire,
  getBonusQuestionnaire,
  getDailyQuestionnaire,
  sendAnswers,
  addCoins,
  getShopItems,
  buyColor,
  activateColor,
  storeMovement,
  getStreakHistory,
  checkStreak,
  getPebblesMood,
  getImpact,
  getMovementWeek,
  getPainMonth,
  getQuestionnaires,
  getScales,
  getCategories,
  addQuestion,
};
