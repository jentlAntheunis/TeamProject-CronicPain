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

const fillMissingDates = (data) => {
  // Set empty array to store the result
  const resultArray = [];

  // Return an empty array if the input is empty
  if (data.length === 0) {
    return resultArray;
  }

  // Extract the start and end dates from the original array
  const startDate = new Date(data[0].date);
  const endDate = new Date();

  // Iterate through each day between start and end dates
  let currentDate = new Date(startDate);
  while (currentDate <= endDate) {
    // Get the date string in the correct format (dd/mm/yyyy)
    const dateString = currentDate.toLocaleDateString("nl-BE");

    // Find the corresponding entry in the original array
    const matchingEntry = data.find(entry => {
      const entryDate = new Date(entry.date);
      return entryDate.toLocaleDateString("nl-BE") === dateString
    });

    // Generate the day/month string (dd/mm) used as the label in the chart
    const day = dateString.split('/')[0].padStart(2, '0');
    const month = dateString.split('/')[1].padStart(2, '0');
    const dayMonth = `${day}/${month}`;

    // Create a new object with the date and value (or null if not found)
    const newObj = {
      date: dayMonth,
      Pijn: matchingEntry ? matchingEntry.int : null,
    };

    resultArray.push(newObj);

    // Move to the next day
    currentDate.setDate(currentDate.getDate() + 1);
  }

  return resultArray;
}

export { countImpact, fillMissingDates }
