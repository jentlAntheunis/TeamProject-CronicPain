import axios from "axios";
import request from "./request";

// Example api call with bearer token
const getTodos = async () => request({
  url: '/todos',
})

const postTodo = async (todo) => request({
  url: '/todos',
  method: 'POST',
  data: todo,
})

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

const getUserData = async (userId) => await request({
  url: '/users/' + userId,
  method: 'GET',
})

export {
  getUser,
  checkIfUserExists,
  storePatient,
  sendMailToPatient,
  getUserData,
}
