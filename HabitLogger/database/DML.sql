-- Data Manipulation Language for Habit Logger

-- -----------------------------------------------------
-- Users Data Manipulation Queries
-- -----------------------------------------------------

-- CREATE
-- add a new User
-- currently only contains 2 attributes which may be expanded at a later date, see 
-- associated DbOutline.md
INSERT INTO Users (userName)
VALUES (userNameInput);  -- userNameInput variable value determined by user input

-- READ
-- get all Users
SELECT userID		AS 'User ID',
	   userName		AS 'User Name'
FROM Users;

-- UPDATE
-- allow user to modify their user name
-- first statement used to populate current user profile info
SELECT *
FROM Users
WHERE userID = userIDInput;
-- update current user
UPDATE Users
SET Users.userName		= userNameInput
WHERE Users.userID = userIDInput;  -- this value automatically supplied by the app

-- DELETE
-- first, delete all records from Dates that would no longer have any relationships 
-- with Habits after the Habit is cascade deleted when User is deleted
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
-- remove the user and all associated habits (including intermediary Habits_has_Dates records) - automatic due to ON DELETE CASCADE foreign key constraint in Habits table
DELETE
FROM Users
WHERE userID = userIDInput;