import axios from 'axios';
import { auth } from '../services/firebase';

const client = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  timeout: 3000,
});

const request = async ({ ...options }) => {
  // get token from firebase user
  const user = auth.currentUser;
  // if user is logged in, add token to request header, else throw error
  if (user) {
    console.log("User logged in, adding token to request header")
    await user.getIdToken().then((token) => {
      client.defaults.headers.common.Authorization = `Bearer ${token}`;
    });
  } else {
    console.error("User not logged in, no token");
    throw new Error('User not logged in');
  }

  const onSuccess = (response) => {
    console.debug('Request Successful!', response);
    return response;
  };
  const onError = (error) => {
    // if session expired, log user out and redirect to login page
    console.error('Request Failed:', error.message);
    console.log(error)
    if (error.response.status === 401) {
      auth.signOut();
    }
    return Promise.reject(error);
  }

  return client(options)
    .then(onSuccess)
    .catch(onError);
}

export default request;
