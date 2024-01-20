import { create } from "zustand";
import { persist, createJSONStorage } from "zustand/middleware";

const useStore = create(
  persist(
    (set) => ({
      questions: [],
      answers: [],
      currentQuestion: 0,
      questionaireIndex: 0,
      questionaireId: null,
      movementTime: 0,
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
      incrementQuestionaireIndex: () =>
        set((state) => ({ questionaireIndex: state.questionaireIndex + 1 })),
      decrementQuestionaireIndex: () =>
        set((state) => ({ questionaireIndex: state.questionaireIndex - 1 })),
      resetQuestionaireIndex: () => set({ questionaireIndex: 0 }),
      setQuestionaireId: (id) => set({ questionaireId: id }),
      resetQuestionaireId: () => set({ questionaireId: null }),
      setMovementTime: (time) => set({ movementTime: time }),
      resetMovementTime: () => set({ movementTime: 0 }),
    }),
    {
      name: "questionaire-store",
      storage: createJSONStorage(() => localStorage),
    }
  )
);

export default useStore;
