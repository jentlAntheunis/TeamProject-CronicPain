import axios from "axios";
import request from "./request";
import QuestionCategories from "../config/questionCategories";

/**
 * Authentication API calls
 */
const getUser = async (email) => await request({
  url: '/users/loginbyemail',
  method: 'POST',
  data: email,
  headers: {
    'Content-Type': 'application/json',
  },
})

const checkIfUserExists = async (email) => {
  const { data } = await axios.get(import.meta.env.VITE_API_URL + '/users/exists/' + email)
  return data
}

const storePatient = async ({ firstName, lastName, email, specialistId }) => await request({
  url: `/specialists/${specialistId}/patients`,
  method: 'POST',
  data: { firstName, lastName, email }
})

const sendMailToPatient = async ({ firstName, lastName, email, specialistId }) => await request({
  url: '/specialists/send-email/' + specialistId,
  method: 'POST',
  data: { firstName, lastName, email }
})

/**
 * User API calls
 */
const getUserData = async (userId) => await request({
  url: '/users/' + userId,
  method: 'GET',
})

/**
 * Questionnaire API calls
 */
const getQuestionnaire = async (userId, questionnaireType, category) => {
  const result = await request({
    url: `/questionnaires/${questionnaireType}/${userId}`,
    method: 'GET',
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
  return getQuestionnaire(userId, 'movementquestionnaire', QuestionCategories.Movement);
};

const getBonusQuestionnaire = async (userId) => {
  return getQuestionnaire(userId, 'bonusquestionnaire', QuestionCategories.Bonus);
};

const getDailyQuestionnaire = async (userId) => {
  return getQuestionnaire(userId, 'dailyquestionnaire', QuestionCategories.Daily);
};

export {
  getUser,
  checkIfUserExists,
  storePatient,
  sendMailToPatient,
  getUserData,
  getMovementQuestionnaire,
  getBonusQuestionnaire,
  getDailyQuestionnaire,
}
