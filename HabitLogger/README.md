# Habit Logger

Welcome to Habit Logger -- a very simple desktop application for keeping track of your daily habits! Just login with your profile, choose a date, and start adding habits you'd like to keep tabs on. This application runs and stores all data on your local computer.

#### Built With

Habit Logger was written in C# using the Windows Forms GUI library for .NET Windows desktop applications. It also uses SQLite to store your data locally.

## Setup

This application is compatible with .NET version 4.8+. Please ensure that you a compatible .NET version installed before continuing.


To run the application, simply download the project files and compile the source code using Visual Studio or your favorite IDE for C#. From there, run the program executable to begin!

## Usage

#### Login Window

Once the program starts, you will see the following login screen:


<img width="2868" height="1925" alt="Image" src="https://github.com/user-attachments/assets/48e4b1cf-b082-4d19-94b6-09ee8c37d056" />

<br>

```
Annotations:
1. Login text box - enter your username here
2. Login button - click to login after entering a username
3. Exit button - exit the program
4. New user link - click to create a new user; this will open the Add New User window
```

To login, enter your username and click the 'Login' button. You may also login using the Enter key once you input your username.


If you are a new user, you must first create a user profile to begin using Habit Logger. Click on the New User hyperlink at the bottom of the window to begin.

#### Add New User Window

If you are a new user, the following window will pop up after clicking the New User hyperlink:


<img width="1252" height="1025" alt="Image" src="https://github.com/user-attachments/assets/d6f6e1b3-374c-4471-93ac-bbc241517dbd" />

```
Annotations:
1. New User text box - enter the name you would like to use for your profile here
2. Add User button - click to add your username to the database
3. Cancel button - exit the Add New User window without adding your profile
```

To create a new user profile, enter your desired profile name into the New User text box and click 'Add User'. This window will automatically close and you may then login using your new username.

#### Main Window

Once you have logged into the application, you will see the following window, from which you may create habits and add them to dates:


<img width="2860" height="1929" alt="Image" src="https://github.com/user-attachments/assets/5457c5a1-d805-4d4e-bd49-9d710f1fa436" />

```
Annotations:
1. Calendar - use the calendar to select dates
2. Welcome message - displays the current user
3. Logout button - click to exit the Main window and return to the Login window; no data will be lost
4. Daily Habits Data Grid View - displays the habits you practiced on the selected date with number of times practiced and an optional note
5. Undo/Redo buttons - undo the previous action or redo an action that has been undone
6. User Habits Data Grid View - displays ALL habits for the current User with an optional description for each habit
```

To create a new habit, you can begin typing in the 'Habit' column in either 4. (after selecting your desired date) or 6. Once you press Enter, the Add New Habit window will open and you may enter the name of your habit and a (optional) description there. Once a habit is created, it will always be listed in 6. However, it will only be listed in 4. if the appropriate date is selected.


If a new habit is created using 4. it will be added to 6. and 4. simultaneously. A habit may be deleted form the selected date by selecting the habit row (click the empty square to the left of the row in the Data Grid View) and pressing the Delete button. If a habit is deleted from 4., it will only be deleted from the selected date. Warning! If a habit is deleted from 6., it will be deleted from the user profile, which includes ALL dates.


You may modify the Frequency or Note columns in 4. by clicking on the cell in question and entering the desired value. You may also modify Habit name or Description in 6. by doing the same.


As pictured in the figure of the Main window above, if the habit already exists, you will be prompted with an autocomplete option including all possiblities given your current entry. You may autocomplete at any time by pressing the Tab button or clicking on any of the options in the popup.


Finally, the Undo/Redo buttons allow you to undo your last action only in 4. Ff you made a mistake or otherwise wish to undo the previous change, simply click 'Undo'. You may redo a previously undone action by clicking 'Redo'. If an action can be undone or redone, the text in the appropriate button will be shown in black. If no action can be taken, the text will be greyed out. Note: all Undo/Redo history will be lost if you select a new date!

#### Add New Habit Window

If you attempt to create a new habit at any point from the Main window, the following Add New Habit window will open:


<img width="1516" height="1015" alt="Image" src="https://github.com/user-attachments/assets/a091d23a-701b-4e6b-85e2-b919d60e167f" />

```
Annotations:
1. Habit name selection - automatically populates as the value you entered
2. Description text box - enter a description for your new habit here; optional!
3. Ok button - add the new habit with your current selections
4. Cancel button - exit the Add New Habit window without adding the habit
```

To add your new habit, select the habit name, add a description (optional), and click 'Ok'. 


The new habit name automatically populates to the name you entered into the Main window, but you may instead choose to select an existing habit if you prefer. Any edit you make to the description will be updated in the existing habit.

<br>

### Thank you for using Habit Logger!

<br>

---

<br>

Project Outline Source: https://www.thecsharpacademy.com/project/12/habit-logger