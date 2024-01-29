import { z } from "zod";

const validatePatientCsv = (data) => {
  // strip empty rows
  data = data.filter((row) => row.length > 1);

  const hasThreeColumns = data.every((row) => row.length === 3);

  if (!hasThreeColumns) {
    throw new Error("Het bestand bevat niet exact 3 kolommen");
  }

  const possibleColumnNames = [
    "familienaam",
    "achternaam",
    "last_name",
    "first_name",
    "lastname",
    "voornaam",
    "firstname",
    "email",
    "e-mail",
    "e-mailadres",
    "emailadres",
  ];

  const hasNoColumnNames = data.every((row) => {
    return row.every(
      (field) => !possibleColumnNames.includes(field.toLowerCase())
    );
  });

  if (!hasNoColumnNames) {
    throw new Error("Het bestand bevat kolomnamen");
  }

  const hasNoEmptyFields = data.every((row) => {
    return row.every((field) => field !== "");
  });

  if (!hasNoEmptyFields) {
    throw new Error("Het bestand bevat lege velden");
  }

  const hasValidEmails = data.every((row) => {
    const email = row[2];
    return z.string().email().safeParse(email).success;
  });

  if (!hasValidEmails) {
    throw new Error("Het bestand bevat ongeldige e-mailadressen");
  }


  const hasNoDuplicates = data.every((row, index) => {
    const email = row[2];
    const emails = data.map((row) => row[2]);
    return emails.indexOf(email) === index;
  });

  if (!hasNoDuplicates) {
    throw new Error("Het bestand bevat dubbele e-mailadressen");
  }

  return data;
}

const validateQuestionCsv = (data) => {
  data = data.filter((row) => row.length > 0);

  const hasOneColumn = data.every((row) => row.length === 1);

  if (!hasOneColumn) {
    throw new Error("Het bestand bevat niet exact 1 kolom");
  }

  const possibleColumnNames = [
    "vraag",
    "question",
    "content",
    "vragen",
    "questions",
    "contents",
  ];

  const hasNoColumnNames = data.every((row) => {
    return row.every(
      (field) => !possibleColumnNames.includes(field.toLowerCase())
    );
  });

  if (!hasNoColumnNames) {
    throw new Error("Het bestand bevat een kolomnaam");
  }

  const hasNoEmptyFields = data.every((row) => {
    return row.every((field) => field !== "");
  });

  if (!hasNoEmptyFields) {
    throw new Error("Het bestand bevat lege velden");
  }

  const hasValidQuestions = data.every((row) => {
    const question = row[0];
    return z.string().min(5).safeParse(question).success;
  });

  if (!hasValidQuestions) {
    throw new Error("Het bestand bevat ongeldige vragen");
  }


  const hasNoDuplicates = data.every((row, index) => {
    const question = row[0];
    const questions = data.map((row) => row[0]);
    return questions.indexOf(question) === index;
  });

  if (!hasNoDuplicates) {
    throw new Error("Het bestand bevat dubbele vragen");
  }

  return data;
}

export { validatePatientCsv, validateQuestionCsv }
