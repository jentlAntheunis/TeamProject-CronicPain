import { z } from "zod";

const validatePatientCsv = (data) => {
  // strip empty rows
  data = data.filter((row) => row.length > 1);

  const hasThreeColumns = data.every((row) => row.length === 3);

  if (!hasThreeColumns) {
    throw new Error("Het bestand bevat niet 3 kolommen");
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

  const hasValidEmails = data.every((row) => {
    const email = row[2];
    return z.string().email().safeParse(email).success;
  });

  if (!hasValidEmails) {
    throw new Error("Het bestand bevat ongeldige e-mailadressen");
  }

  const hasNoEmptyFields = data.every((row) => {
    return row.every((field) => field !== "");
  });

  if (!hasNoEmptyFields) {
    throw new Error("Het bestand bevat lege velden");
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

export { validatePatientCsv }
