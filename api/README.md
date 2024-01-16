# Backend Pebbles

Welkom in het wonderlijk warrige doolhof dat we onze backend noemen.

Door het gebruik van EntityFramework zijn de json bodies niet case sensitive.

Hier zijn de mogelijke endpoints:

## Specialists
GET
* /specialists
    * gets all specialists
* /specialists/{id}
    * gets specialist by id

POST
* /specialists
    * Adds a specialist
    * Body:
        ```json
        {
            "firstName": "string",
            "lastName": "string",
            "email": "string"
        }
        ```
* /specialists/send-email/{id}
    * Sends an invitation email from the specialist to a user
    * Body:
        ```json
        {
            "firstName": "string",
            "lastName": "string",
            "email": "string"
        }
        ```
* /specialists/{id}/patients
    * Adds a new patient to the specialist
    * Body:
        ```json
        {
            "firstName": "string",
            "lastName": "string",
            "email": "string"
        }
        ```

PUT
* /specialists/{id}
    * Edits the specialist information
    * Body:
        ```json
        {
            "firstName": "string",
            "lastName": "string",
            "email": "string",
        }
        ```

## Users (specialists and patients combined)
GET
* /users
    * Gets all users
* /users/{id}
    * Gets user by id
    * Can be used to get a user that has been soft-deleted
* /users/exists/{email}
    * Returns true if it finds the email in the user database
    * Returns false if the email is not in the database

PUT
* /users/{id}
    * Used to edit any user
    * Body:
        ```json
        {
            "firstName": "string",
            "lastName": "string",
            "email": "string"
        }
        ```

DELETE
* /users/{id}
    * Used to soft-delete any user
    