# Habit Logger

<br>

---

<br>

Project Outline Source: https://www.thecsharpacademy.com/project/12/habit-logger

Requirements:

1. This is an application where youÅfll log occurrences of a habit.

2. This habit can't be tracked by time (ex. hours of sleep), only by quantity (ex. number of water glasses a day)

3. Users need to be able to input the date of the occurrence of the habit

4. The application should store and retrieve data from a real database

5. When the application starts, it should create a sqlite database, if one isnÅft present.

6. It should also create a table in the database, where the habit will be logged.

7. The users should be able to insert, delete, update and view their logged habit.

8. You should handle all possible errors so that the application never crashes.

9. You can only interact with the database using ADO.NET. You canÅft use mappers such as Entity Framework or Dapper.


Challenge Goals:

1. If you haven't, try using parameterized queries to make your application more secure.
// https://stackoverflow.com/questions/5468425/how-do-parameterized-queries-help-against-sql-injection

2. Let the users create their own habits to track. That will require that you let them choose the unit of measurement of each habit.

3. Seed Data into the database automatically when the database gets created for the first time, generating a few habits and inserting a hundred records with randomly generated values. This is specially helpful during development so you don't have to reinsert data every time you create the database.

4. Create a report functionality where the users can view specific information (i.e. how many times the user ran in a year? how many kms?) SQL allows you to ask very interesting things from your database.