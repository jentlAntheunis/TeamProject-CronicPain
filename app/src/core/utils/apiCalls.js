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
  url: '/users/getbyemail',
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

export {
  getUser,
  checkIfUserExists,
}
