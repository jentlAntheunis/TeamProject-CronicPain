import { create } from "zustand";

const useStore = create((set) => ({
  questions: [],
  answers: [],
  setQuestions: (questions) => set({ questions }),
  setAnswers: (answers) => set({ answers }),
  removeAnswers: () => set({ answers: [] }),
  removeQuestions: () => set({ questions: [] }),
}));

export default useStore;
