# Backend Pebbles

Welkom in het wonderlijk warrige doolhof dat we onze backend noemen.

Door het gebruik van EntityFramework zijn de json bodies niet case sensitive.

Hier zijn de mogelijke endpoints:

## Specialists

GET
- /specialists
  - gets all specialists
- /specialists/{<span style="color: cornflowerblue">specialistId</span>}
  - gets specialist by id

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
  - Adds a new patient to the specialist
  - Body:
    ```json
    {
      "firstName": "string",
      "lastName": "string",
      "email": "string"
    }
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
- /patients/{<span style="color: cornflowerblue">patientId</span>}/movementsession/start
  - Starts movement session for the patient
  - Movement session id will be returned
- /patients/{<span style="color: cornflowerblue">movementSessionId</span>}/end
  - Stops the movement session
  - <span style="color: red">Use Movement Id for end, not Patient Id</span>
- /patients/{<span style="color: cornflowerblue">patientId</span>}/pebblesmood
  - Gets the mood pebbles should be in right now
  - NOT IMPLEMENTED YET

## Users (specialists and patients combined)

GET
- /users
  - Gets all users
- /users/{<span style="color: cornflowerblue">userId</span>}
  - Gets user by id
  - Can be used to get a user that has been soft-deleted
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
