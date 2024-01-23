# Backend Pebbles

Welkom in het wonderlijk warrige doolhof dat we onze backend noemen.

Door het gebruik van EntityFramework zijn de json bodies niet case sensitive.

Hier zijn de mogelijke endpoints:

## Default

GET

- /
  - Default endpoint dat aangeeft of de backend werkt

## Specialists

GET

- /specialists
  - gets all specialists
- /specialists/{<span style="color: cornflowerblue">specialistId</span>}
  - gets specialist by id
- /specialists/{<span style="color: cornflowerblue">specialistId</span>}/patients
  - Gets a list of Patients for the specialist

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
- /specialists/send-email/{<span style="color: cornflowerblue">specialistId</span>}
  - Sends an invitation email from the specialist to a user
  - Body:
    ```json
    {
      "firstName": "string",
      "lastName": "string",
      "email": "string"
    }
    ```
- /specialists/{<span style="color: cornflowerblue">specialistId</span>}/patients
  - Adds a new patient to the specialist. If the patient is already present in the database (checked by email, then lastname, then firstname), it adds a new relation between the specialist and patient, but no new patient.
  - Body:
    ```json
    {
      "firstName": "string",
      "lastName": "string",
      "email": "string"
    }
    ```
- /specialists/{<span style="color: cornflowerblue">specialistId</span>}/patients/addlist
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
- /specialists/{<span style="color: cornflowerblue">specialistId</span>}/patients/{<span style="color: cornflowerblue">patientId</span>}/movementSuggestions
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

- /specialists/{<span style="color: cornflowerblue">specialistId</span>}
  - Edits the specialist information
  - Body:
    ```json
    {
      "firstName": "string",
      "lastName": "string",
      "email": "string"
    }
    ```

## Patients

GET

- /patients/{<span style="color: cornflowerblue">patientId</span>}
  - Gets patient by id
- /patients/{<span style="color: cornflowerblue">patientId</span>}/details
  - Gets patient by id with more detail
    - such as logins, movement sessions and suggestions
- /patients/{<span style="color: cornflowerblue">patientId</span>}/movementsessions
  - Gets all movement sessions of the patient
- /patients/{<span style="color: cornflowerblue">patientId</span>}/movementsuggestions
  - Gets all movement suggestions of the patient
- /patients/{<span style="color: cornflowerblue">patientId</span>}/pebblesmood
  - Gets the mood pebbles should be in right now
  - Output:
    ```
      HAPPY | NEUTRAL | SAD
    ```
- /patients/{<span style="color: cornflowerblue">patientId</span>}/movementtimeweek
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
- /patients/{<span style="color: cornflowerblue">patientId</span>}/streakhistory
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
- /patients/{patientid}/questionnaires
  - Returns a json with all the questionnaires (incl their date), their questions and their answers (before and after the movement in the case of a movement questionnaire)

POST

- /patients/{<span style="color: cornflowerblue">patientId</span>}/movementsessions
  - Post a movement session
  - Body:
    ```json
      {
        "seconds": int
      }
    ```

PUT

- /patients/{<span style="color: cornflowerblue">patientId</span>}/addcoins/{<span style="color: cornflowerblue">amount</span>}
- Add coins to the patient

## Users (specialists and patients combined)

GET

- /users
  - Gets all users
- /users/exists/{<span style="color: cornflowerblue">email</span>}
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

- /users/{<span style="color: cornflowerblue">userId</span>}
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

- /users/{<span style="color: cornflowerblue">userId</span>}
  - Used to soft-delete any user

## Store

GET

- /store/{<span style="color: cornflowerblue">patientId</span>}
  - Gets a complete list of all store items, with info on owned and active
- /store/{<span style="color: cornflowerblue">patientId</span>}/buy/{<span style="color: cornflowerblue">colorId</span>}
  - Subtracts patient coins if they have enough and adds the color to their account
- /store/{<span style="color: cornflowerblue">patientId</span>}/use/{<span style="color: cornflowerblue">colorId</span>}
  - Adds the color if owned to the patients avatar, and sets the color as active in the store endpoint


## Question

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

## Scale

GET

- /all
  - Returns an array with all the scales (id and name)


## Category

GET

- /all
  - Returns an array with all the categories (id and name)



