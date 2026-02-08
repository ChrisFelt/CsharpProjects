-- Data Definition Language for Habit Logger

-- #####################################################
-- TABLE PARAMETERS
-- #####################################################

-- -----------------------------------------------------
-- Users Table
-- -----------------------------------------------------
-- AUTOINCREMENT is not necessary in SQLite;
-- PRIMARY KEY acts as an alias for built in ROWID column, which increments automatically
CREATE TABLE Users (
	userID			INTEGER PRIMARY KEY NOT NULL UNIQUE, 
	userName		VARCHAR(20) NOT NULL UNIQUE  -- VARCHAR is converted to TEXT in SQLite
);


-- -----------------------------------------------------
-- Habits Table
-- -----------------------------------------------------
CREATE TABLE Habits (
	habitID			INTEGER PRIMARY KEY NOT NULL UNIQUE,
	name			VARCHAR(30) NOT NULL UNIQUE,
	description		VARCHAR(200),
	userID			INTEGER NOT NULL,
	CONSTRAINT fk_Habits_User 
		FOREIGN KEY (userID) 
		REFERENCES Users (userID) 
		ON DELETE CASCADE 
		ON UPDATE NO ACTION
);


-- -----------------------------------------------------
-- Dates Table
-- -----------------------------------------------------
CREATE TABLE Dates (
	dateID			INTEGER PRIMARY KEY NOT NULL UNIQUE,
	date			DATE UNIQUE
);


-- -----------------------------------------------------
-- Habits_has_Dates Table
-- -----------------------------------------------------
CREATE TABLE Habits_has_Dates (
	habitHasDateID	INTEGER PRIMARY KEY NOT NULL UNIQUE,
	note			VARCHAR(200),
	quantity		INTEGER NOT NULL,
	habitID			INTEGER NOT NULL,
	dateID			INTEGER NOT NULL,
	CONSTRAINT fk_Habits_has_Dates_Date
		FOREIGN KEY (dateID) 
		REFERENCES Dates (dateID) 
		ON DELETE CASCADE 
		ON UPDATE NO ACTION,
	CONSTRAINT fk_Habits_has_Dates_Habit 
		FOREIGN KEY (habitID) 
		REFERENCES Habits (habitID) 
		ON DELETE CASCADE 
		ON UPDATE NO ACTION
);


-- #####################################################
-- INSERT QUERIES
-- #####################################################

-- -----------------------------------------------------
-- Users Table
-- -----------------------------------------------------
INSERT INTO Users (
	userName
)
VALUES (
	'anonymous123'
), (
	'powerUser'
), (
	'ilikehabits'
);


-- -----------------------------------------------------
-- Habits Table
-- -----------------------------------------------------
INSERT INTO Habits (
	name,
	description,
	userID
)
VALUES (
	'pages read',
	'mostly books, some comics',
	(SELECT userID FROM Users WHERE userName='anonymous123')
), (
	'miles biked',
	'strictly road bikes',
	(SELECT userID FROM Users WHERE userName='anonymous123')
), (
	'lines coded',
	'this is a habit ok',
	(SELECT userID FROM Users WHERE userName='powerUser')
), (
	'popped knuckles',
	NULL,
	(SELECT userID FROM Users WHERE userName='powerUser')
), (
	'cookies eaten',
	'i like cookies',
	(SELECT userID FROM Users WHERE userName='ilikehabits')
), (
	'cups of coffee',
	'i like coffee too',
	(SELECT userID FROM Users WHERE userName='ilikehabits')
), (	
	'brush teeth',
	'i like clean teeth',
	(SELECT userID FROM Users WHERE userName='ilikehabits')
);


-- -----------------------------------------------------
-- Dates Table
-- -----------------------------------------------------
-- using YYYY-MM-DD format
INSERT INTO Dates (
	date
)
VALUES (
	'2025-08-21'	
), (
	'2025-08-22'	
), (
	'2025-08-23'	
);


-- -----------------------------------------------------
-- Habits_has_Dates Table
-- -----------------------------------------------------
INSERT INTO Habits_has_Dates (
	note,
	quantity,
	habitID,
	dateID
)
VALUES (
	'finished reading äÒê∂èb',
	7,
	(SELECT habitID FROM Habits WHERE name='pages read' 
	AND userID=(SELECT userID FROM Users WHERE userName='anonymous123')),  -- match habit name to the correct user
	(SELECT dateID FROM Dates WHERE date='2025-08-21')
), (
	NULL,
	21,
	(SELECT habitID FROM Habits WHERE name='miles biked' 
	AND userID=(SELECT userID FROM Users WHERE userName='anonymous123')),
	(SELECT dateID FROM Dates WHERE date='2025-08-21')
), (
	NULL,
	1337,
	(SELECT habitID FROM Habits WHERE name='lines coded' 
	AND userID=(SELECT userID FROM Users WHERE userName='powerUser')),
	(SELECT dateID FROM Dates WHERE date='2025-08-21')
), (
	NULL,
	1,
	(SELECT habitID FROM Habits WHERE name='popped knuckles' 
	AND userID=(SELECT userID FROM Users WHERE userName='powerUser')),
	(SELECT dateID FROM Dates WHERE date='2025-08-21')
), (
	'ate a box of gingersnaps',
	12,
	(SELECT habitID FROM Habits WHERE name='cookies eaten' 
	AND userID=(SELECT userID FROM Users WHERE userName='ilikehabits')),
	(SELECT dateID FROM Dates WHERE date='2025-08-22')
), (
	'i can''t sleep :(',
	8,
	(SELECT habitID FROM Habits WHERE name='cups of coffee' 
	AND userID=(SELECT userID FROM Users WHERE userName='ilikehabits')),
	(SELECT dateID FROM Dates WHERE date='2025-08-22')
), (
	'need to brush more',
	0,
	(SELECT habitID FROM Habits WHERE name='brush teeth' 
	AND userID=(SELECT userID FROM Users WHERE userName='ilikehabits')),
	(SELECT dateID FROM Dates WHERE date='2025-08-23')
);
