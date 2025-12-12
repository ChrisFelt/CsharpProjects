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
-- get UserID with userName
SELECT userID				AS 'userID',
	   userName				AS 'User Name'
FROM Users
WHERE userName = userNameInput;

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
-- Option 1. get all Habits info for the current User
SELECT	habitID				AS 'habitID',
		name				AS 'Name',
		description			AS 'Description'
FROM Habits
WHERE userID = userIDInput;
-- Option 2. get Habits info for the current User on a given Date
SELECT	h.habitID			AS 'habitID',  -- included for easy access with btnEdit
		h.name				AS 'Name',
		h.description		AS 'Description',
		hd.note				AS 'Note',
		hd.quantity			AS 'Quantity',
		hd.habitHasDateID	AS 'habitHasDateID'
FROM Dates AS d
INNER JOIN Habits_has_Dates AS hd
	ON d.dateID = hd.dateID
INNER JOIN Habits AS h
	ON hd.habitID = h.habitID
WHERE d.date = dateInput
	AND h.UserID = userIDInput;

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
-- first, delete any Dates for which this Habit is the only existing relationship
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


-- -----------------------------------------------------
-- Dates Data Manipulation Queries
-- and Habits_has_Dates Data Manipulation Queries
-- -----------------------------------------------------
-- CREATE
-- 1.a. insert new row into Dates
INSERT INTO Dates (date)
VALUES (dateInput);
-- 1.b. insert new row into Dates if it does not already exist 
-- (conditionals in SQLite do NOT use IF!!!) see https://stackoverflow.com/questions/46349203/how-to-conditionally-insert-or-replace-a-row-in-sqlite
-- IF ( SELECT COUNT(*)
-- 	 FROM Dates
-- 	 WHERE Dates.date = dateInput) = 0
-- BEGIN
-- 	INSERT INTO Dates (date)
-- 	VALUES (dateInput);
-- END
INSERT INTO Dates (date)
SELECT dateInput -- must use SELECT rather than VALUES when followed by the conditional
WHERE NOT EXISTS (SELECT * FROM Dates WHERE date=dateInput);

-- 2. establish relationship with Habits via Habits_has_Dates intermediate table
INSERT INTO Habits_has_Dates (quantity, habitID, dateID)
VALUES (quantityInput,
		(SELECT habitID FROM Habits WHERE name=nameInput),  -- pull nameInput from Habit selection when added to Date
		(SELECT dateID FROM Dates WHERE date=dateInput));

-- READ
-- Read from Dates and Habits_has_Dates simultaneously given an input Date
SELECT	d.date				AS 'Date',
		hd.note				AS 'Note',
		hd.quantity			AS 'Quantity',
		hd.habitHasDateID	AS 'habitHasDateID'  -- make available for easier quantity update
FROM Dates AS d
INNER JOIN Habits_has_Dates AS hd
	ON d.dateID = hd.dateID
WHERE d.date = dateInput;

-- UPDATE
-- update frequency given a HabitHasDateID
UPDATE Habits_has_Dates
SET	Habits_has_Dates.quantity		= quantityInput
WHERE Habits_has_Dates.habitHasDateID = HabitHasDateIDInput;

-- DELETE
-- Option 1. delete Habits_has_Dates record given a habitHasDateID
DELETE
FROM Habits_has_Dates
WHERE habitHasDateID = habitHasDateIDInput;
-- Option 2. delete Date (less common, may not need)
-- Habits_has_Dates cascade deletes when Dates is deleted, and Habits do not require a Dates relationship
DELETE
FROM Dates
WHERE dateID = dateIDInput;


