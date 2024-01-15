import { AuthRoutes } from "./routes";

// Instructions for firebase how to setup email link
const actionCodeSettings = {
  // URL you want to redirect back to. The domain (www.example.com) for this
  // URL must be in the authorized domains list in the Firebase Console.
  url: import.meta.env.VITE_APP_URL + AuthRoutes.Login,
  // This must be true.
  handleCodeInApp: true,
}

export { actionCodeSettings };
