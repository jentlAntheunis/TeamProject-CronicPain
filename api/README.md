# Backend Pebbles

Welkom in het wonderlijk warrige doolhof dat we onze backend noemen.

Door het gebruik van EntityFramework zijn de json bodies niet case sensitive.

Hier zijn de mogelijke endpoints:

## Default

GET

- /
  - Default endpoint dat aangeeft of de backend werkt


## Users (specialists and patients combined)

GET

- /users
  - Gets all users
- /users/exists/{email}
  - Returns true if it finds the email in the user database
  - Returns false if the email is not in the database

POST

- /users/loginbyemail
  - Returns user object
  - Body:
    ```json
    "email"
    ```

PUT

- /users/{userId}
  - Used to edit any user
  - Body:
    ```json
    {
      "firstName": "string",
      "lastName": "string",
      "email": "string"
    }
    ```

DELETE

- /users/{userId}
  - Used to soft-delete any user


## Patients

GET

- /patients/{patientId}
  - Gets patient by id
- /patients/{patientId}/details
  - Gets patient by id with more detail such as logins, movement sessions and suggestions
- /patients/{patientId}/movementsessions
  - Gets all movement sessions of the patient
- /patients/{patientId}/movementsuggestions
  - Gets all movement suggestions of the patient
- /patients/{patientId}/pebblesmood
  - Gets the mood pebbles should be in right now
  - Output:
    ```
      HAPPY | NEUTRAL | SAD
    ```
- /patients/{patientId}/movementtimeweek
  - Gets the movement times of the last 7 days
  - Output:
    ```json
      {
        "days": [
          {
            "date": "DateTime",
            "total": int
          },
          ... *7
        ]
      }
    ```
- /patients/{patientId}/streakhistory

  - Gets the amount of questionnaires filled in in the last 7 days
  - Output:

    ```json
      {
        "days": [
          {
            "date": "DateTime",
            "total": int
          },
          ... *7
        ]
      }

    ```
- /patients/{patientId}/painhistory
  - Returns a list of pain values of each day in the last month
  - Output:
    ```json
      {
        "days": [
          {
            "date": "DateTime",
            "total": int
          },
          ... *7
        ]
      }
    ```
  - /patients/{patientId}/questionnaires
    - Returns a json with all the questionnaires (incl their id, date, category name, patient id), their questions (id and content) and their answers (before and after the movement in the case of a movement questionnaire: see the questionnaire index, the answer id, the question id, the option id, the position of the option and the option content)

POST

- /patients/{patientId}/movementsessions
  - Post a movement session
  - Body:
    ```json
      {
        "seconds": int
      }
    ```

PUT

- /patients/{patientId}/addcoins/{amount}
  - Add coins to the patient
- /patients/{patientId}/checkstreak
  - Checks if patient has filled in a questionnaire yesterday and today. If both are false, resets streak to 0
- /patients/{patientId}/addstreak
  - Increases the patient streak with one




## Specialists

GET

- /specialists
  - gets all specialists
- /specialists/{specialistId}
  - gets specialist by id
- /specialists/{specialistId}/patients
  - Gets a list of Patients for the specialist
- /specialists/{specialistId}/haspatient/{patientId}
  - Checks if a patient and specialist are connected


POST

- /specialists
  - Adds a specialist
  - Body:
    ```json
    {
      "firstName": "string",
      "lastName": "string",
      "email": "string"
    }
    ```
- /specialists/send-email/{specialistId}
  - Sends an invitation email from the specialist to a user
  - Body:
    ```json
    {
      "firstName": "string",
      "lastName": "string",
      "email": "string"
    }
    ```
- /specialists/{specialistId}/patients
  - Adds a new patient to the specialist. If the patient is already present in the database (checked by email, then lastname, then firstname), it adds a new relation between the specialist and patient, but no new patient.
  - Body:
    ```json
    {
      "firstName": "string",
      "lastName": "string",
      "email": "string"
    }
    ```
- /specialists/{specialistId}/patients/addlist
  - Adds a list of patients
  - Body:
    ```json
    [
      {
        "firstName": "string",
        "lastName": "string",
        "email": "string"
      },
      {
        "firstName": "string",
        "lastName": "string",
        "email": "string"
      },
      ...
    ]
    ```
- /specialists/{specialistId}/patients/{patientId}/movementSuggestions
  - Add movement suggestion to patient
  - Body:
    ```json
    {
      "name": "string",
      "description": "string",
      "videoUrl": "url or not_provided"
    }
    ```

PUT

- /specialists/{specialistId}
  - Edits the specialist information
  - Body:
    ```json
    {
      "firstName": "string",
      "lastName": "string",
      "email": "string"
    }
    ```

## Question

GET

- /question/getallquestions
  - Gives back a list of all the questions in the database (including id, category id, category name, specialist id, scale id and content)

POST

- /question/addquestion
  - Adds a question to the database
  - Body:
    ```json
    {
    "CategoryId": "string",
    "SpecialistId": "string",
    "ScaleId": "string",
    "Content": "string"
    }
    ```
- /question/addquestions
  - Adds a list of questions to the database
  - Body:
    ```json
    {
    "data": [
      {
        "content": "string",
        "categoryId": "string",
        "scaleId": "string",
        "specialistId": "string"
      },
      {
        "content": "string",
        "categoryId": "string",
        "scaleId": "string",
        "specialistId": "string"
      },
      ...
    ]
    }
    ```

PUT

- /question/updatequestion/{userid}
  - Updates an existing question in the database (you give the question id)
  - Body:
    ```json
    {
    "CategoryId": "string",
    "SpecialistId": "string",
    "ScaleId": "string",
    "Content": "string"
    }
    ```






## Questionnaires

GET

- /questionnaires/movementquestionnaire/{userId}
  - Returns a questionnaire with 5 random questions in it of the movement catgory, together with the answer option id's
- /questionnaires/bonusquestionnaire/{userId}
  - Returns a questionnaire with 5 random questions in it of the movement catgory, together with the answer option id's
- /questionnaires/dailypainquestionnaire/{userId}
  - Returns the dauly pain questionnaire, together with the answer option id's
- /questionnaire/checkifFirstquestionnaireoftheday
  - Returns true if it's the first answered questionnaire of the day
  - Returns false if it's not the fist answered questionnaire of the day
- /questionnaire/checkifbonusdone/{patientId}
  - Returns true or false when the patient has already done a bonus questionnaire today

## Answer

GET

- /answers/user/{userId}/impacts
  - Returns a list of all the questionnaires and their impact (the score of the comparison of all the questions in a questionnaire)

POST

- /answers
  - Saves the answers from the questionnaire before and after a movement session
  - Body:
    ```json
    {
      "questionnaireId": "string",
      "answers": [
        {
          "questionId": "string",
          "optionId": "string",
          "questionnaireIndex": "0"
        },
        {
          "questionId": "string",
          "optionId": "string",
          "questionnaireIndex": "0"
        },
        {
          "questionId": "string",
          "optionId": "string",
          "questionnaireIndex": "0"
        },
        {
          "questionId": "string",
          "optionId": "string",
          "questionnaireIndex": "0"
        },
        {
          "questionId": "string",
          "optionId": "string",
          "questionnaireIndex": "0"
        },
        {
          "questionId": "string",
          "optionId": "string",
          "questionnaireIndex": "1"
        },
        {
          "questionId": "string",
          "optionId": "string",
          "questionnaireIndex": "1"
        },
        {
          "questionId": "string",
          "optionId": "string",
          "questionnaireIndex": "1"
        },
        {
          "questionId": "string",
          "optionId": "string",
          "questionnaireIndex": "1"
        },
        {
          "questionId": "string",
          "optionId": "string",
          "questionnaireIndex": "1"
        }
      ]
    }
    ```

## Category

GET

- /all
  - Returns an array with all the categories (id and name)


## Scale

GET

- /all
  - Returns an array with all the scales (id and name)


## Store

GET

- /store/{patientId}
  - Gets a complete list of all store items, with info on owned and active
- /store/{patientId}/byprice
  - Gets a complete list of all store items, with info on owned and active, ordered by price

PUT

- /store/{patientId}/buy/{colorId}
  - Subtracts patient coins if they have enough and adds the color to their account
- /store/{patientId}/use/{colorId}
  - Adds the color if owned to the patients avatar, and sets the color as active in the store endpoint
