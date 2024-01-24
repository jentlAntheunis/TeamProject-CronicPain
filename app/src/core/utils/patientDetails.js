import { Impacts } from "../config/impacts";

const countImpact = (data) => {
  let positiveCount = 0;
  let negativeCount = 0;
  let neutralCount = 0;

  for (const key in data) {
    if (data[key] === Impacts.Positive) positiveCount++;
    else if (data[key] === Impacts.Negative) negativeCount++;
    else if (data[key] === Impacts.Neutral) neutralCount++;
  }

  const result = {
    [Impacts.Positive]: positiveCount,
    [Impacts.Negative]: negativeCount,
    [Impacts.Neutral]: neutralCount,
  };

  return result;
}

export { countImpact }
