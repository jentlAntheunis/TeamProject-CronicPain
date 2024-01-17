// Dont forget to use whitespace: pre-wrap; on the element
const spaceToBreak = (text) => {
  return text.split(' ').join('\n');
}

const capitalize = (text) => {
  return text.charAt(0).toUpperCase() + text.slice(1);
}

export { spaceToBreak, capitalize };
