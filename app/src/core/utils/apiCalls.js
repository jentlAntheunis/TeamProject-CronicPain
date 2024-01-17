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

export {
  getUser
}
