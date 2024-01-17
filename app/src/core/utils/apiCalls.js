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

const login = async (email) => request({
  url: '/users/login',
  method: 'POST',
  data: email,
})

export {
  login
}
