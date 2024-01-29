const defaultAnswer = (question) => {
  if (question.scale.options.length % 2 === 0) {
    return 0;
  }
  return Math.floor(question.scale.options.length / 2);
}

const orderOptions = (options) => {
  options.sort((a, b) => parseInt(a.position) - parseInt(b.position));
  return options;
}

export { defaultAnswer, orderOptions }
