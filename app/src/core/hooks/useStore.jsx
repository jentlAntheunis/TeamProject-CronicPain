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
      questionaireCategory: null,
      movementTime: 0,
      setQuestions: (questions) => set({ questions }),
      setAnswers: (answers) => set({ answers }),
      addAnswer: (answer, index) =>
        set((state) => {
          const newAnswers = [...state.answers];
          newAnswers[index] = answer;
          return { answers: newAnswers };
        }),
      removeAnswers: () => set({ answers: [] }),
      removeQuestions: () => set({ questions: [] }),
      resetCurrentQuestion: () => set({ currentQuestion: 0 }),
      incrementCurrentQuestion: () =>
        set((state) => ({ currentQuestion: state.currentQuestion + 1 })),
      decrementCurrentQuestion: () =>
        set((state) => ({ currentQuestion: state.currentQuestion - 1 })),
      incrementQuestionaireIndex: () => set({ questionaireIndex: 1 }),
      decrementQuestionaireIndex: () =>
        set((state) => ({ questionaireIndex: state.questionaireIndex - 1 })),
      resetQuestionaireIndex: () => set({ questionaireIndex: 0 }),
      setQuestionaireId: (id) => set({ questionaireId: id }),
      resetQuestionaireId: () => set({ questionaireId: null }),
      setQuestionaireCategory: (type) => set({ questionaireCategory: type }),
      resetQuestionaireCategory: () => set({ questionaireCategory: null }),
      setMovementTime: (time) => set({ movementTime: time }),
      resetMovementTime: () => set({ movementTime: 0 }),
      resetEverything: () => {
        set({ answers: [] });
        set({ questions: [] });
        set({ currentQuestion: 0 });
        set({ questionaireIndex: 0 });
        set({ questionaireId: null });
        set({ questionaireCategory: null });
        set({ movementTime: 0 });
      }
    }),
    {
      name: "questionaire-store",
      storage: createJSONStorage(() => localStorage),
    }
  )
);

export default useStore;
