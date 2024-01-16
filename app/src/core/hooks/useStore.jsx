import { create } from "zustand";
import { persist, createJSONStorage } from "zustand/middleware";

const useStore = create(
  persist(
    (set) => ({
      questions: [],
      answers: [],
      currentQuestion: 0,
      setQuestions: (questions) => set({ questions }),
      setAnswers: (answers) => set({ answers }),
      addAnswer: (answer) =>
        set((state) => ({ answers: [...state.answers, answer] })),
      removeAnswers: () => set({ answers: [] }),
      removeQuestions: () => set({ questions: [] }),
      resetCurrentQuestion: () => set({ currentQuestion: 0 }),
      incrementCurrentQuestion: () =>
        set((state) => ({ currentQuestion: state.currentQuestion + 1 })),
      decrementCurrentQuestion: () =>
        set((state) => ({ currentQuestion: state.currentQuestion - 1 })),
    }),
    {
      name: "questionaire-store",
      storage: createJSONStorage(() => localStorage),
    }
  )
);

export default useStore;
