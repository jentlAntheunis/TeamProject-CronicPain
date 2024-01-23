import axios from "axios";
import request from "./request";
import QuestionCategories from "../config/questionCategories";

/**
 * Authentication API calls
 */
const getUser = async (email) => await request({
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

const storePatient = async ({ firstName, lastName, email, specialistId }) => await request({
  url: `/specialists/${specialistId}/patients`,
  method: "POST",
  data: { firstName, lastName, email },
});

const sendMailToPatient = async ({
  firstName,
  lastName,
  email,
  specialistId,
}) => await request({
  url: "/specialists/send-email/" + specialistId,
  method: "POST",
  data: { firstName, lastName, email },
});

/**
 * User API calls
 */
const getUserData = async (userId) => await request({
  url: "/patients/" + userId,
  method: "GET",
});

const getPatients = async (userId) => await request({
  url: '/specialists/' + userId + '/patients',
  method: 'GET',
})

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

const sendAnswers = async (data) => await request({
  url: "/answers",
  method: "POST",
  data: data,
});

/**
 * Coins and Streaks API calls
 */
const addCoins = async (userId, amount) => await request({
  url: `/patients/${userId}/addcoins/${amount}`,
  method: "PUT",
});

/**
 * Shop API calls
 */
const getShopItems = async (userId) => await request({
  url: `/store/${userId}`,
  method: "GET",
});

const buyColor = async (userId, itemId) => await request({
  url: `/store/${userId}/buy/${itemId}`,
  method: "GET",
});

const activateColor = async (userId, itemId) => await request({
  url: `/store/${userId}/use/${itemId}`,
  method: "GET",
});

/**
 * Movement API calls
 */
const storeMovement = async (userId, totalTime) => await request({
  url: `/patients/${userId}/movementsessions`,
  method: 'POST',
  data: { seconds: totalTime }
})

export {
  getUser,
  checkIfUserExists,
  storePatient,
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
};
