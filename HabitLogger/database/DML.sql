-- Data Manipulation Language for Habit Logger

-- -----------------------------------------------------
-- Users Data Manipulation Queries
-- -----------------------------------------------------

-- CREATE
-- add a new User
-- currently only contains 2 attributes which may be expanded at a later date, see associated DbOutline.md
INSERT INTO Users (userName)
VALUES (userNameInput);  -- userNameInput variable value determined by user input

-- READ
-- get all Users
SELECT userID			AS 'User ID',
	   userName			AS 'User Name'
FROM Users;

-- UPDATE
-- allow user to modify their user name
-- first statement used to populate current user profile info
SELECT *
FROM Users
WHERE userID = userIDInput;
-- update current user
UPDATE Users
SET	Users.userName		= userNameInput
WHERE Users.userID = userIDInput;  -- this value automatically supplied by the app

-- DELETE
-- first, delete all records from Dates that are only related to this User's Habits 
-- (Habits and Habits_has_Dates are cascade deleted when User is deleted)
DELETE
FROM Dates
WHERE dateID = (SELECT d.dateID
				FROM Dates AS d
				INNER JOIN Habits_has_Dates AS hd
					ON d.dateID = hd.dateID
				INNER JOIN Habits AS h
					ON hd.habitID = h.habitID
				WHERE h.userID = userIDInput  -- all habits and dates for the user
					AND (SELECT COUNT(dateID)
						 FROM Habits_has_Dates
						 WHERE dateID = d.dateID) <= 1);  -- limit to dates with 1 or less relationships for that date
-- then delete the User
DELETE
FROM Users
WHERE userID = userIDInput;

-- -----------------------------------------------------
-- Habits Data Manipulation Queries
-- -----------------------------------------------------

-- CREATE
-- add a new Habit with no NULL values
INSERT INTO Habits (name, description, userID)
VALUES (nameInput,
		descriptionInput,
		userIDInput);
-- add a new Habit with NULL description
INSERT INTO Habits (name, description, userID)
VALUES (nameInput,
		NULL,
		userIDInput);

-- READ
SELECT	habitID			AS 'Habit ID',
		name			AS 'Name',
		description		AS 'Description',
		userID			AS 'User ID'
FROM Habits;

-- UPDATE
-- display the Habit that will be updated
SELECT *
FROM Habits
WHERE habitID = habitIDInput;
-- update with no NULL values
UPDATE Habits
SET	Habits.name			= nameInput,
	Habits.description	= descriptionInput
WHERE Habits.habitID = habitIDInput;
-- update with NULL description
UPDATE Habits
SET	Habits.name			= nameInput,
	Habits.description	= NULL
WHERE Habits.habitID = habitIDInput;

-- DELETE
-- first delete any Dates for which this Habit is the only existing relationship
DELETE
FROM Dates
WHERE dateID = (SELECT d.dateID
				FROM Dates AS d
				INNER JOIN Habits_has_Dates AS hd
					ON d.dateID = hd.dateID
				INNER JOIN Habits AS h
					ON hd.habitID = h.habitID
				WHERE h.habitID = habitIDInput
					AND (SELECT COUNT(dateID)
						 FROM Habits_has_Dates
						 WHERE dateID = d.dateID) <= 1);
-- then delete the Habit
DELETE
FROM Habits
WHERE habitID = habitIDInput;