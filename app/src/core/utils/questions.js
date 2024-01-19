const defaultAnswer = (question) => {
  if (question.options.length % 2 === 0) {
    return 0;
  }
  return Math.floor(question.options.length / 2);
}

export { defaultAnswer }
