# Habit Logger

Welcome to Habit Logger -- a very simple desktop application for keeping track of your daily habits! Just login with your profile, choose a date, and start adding habits you'd like to keep tabs on. This application runs and stores all data on your local computer.

#### Built With

Habit Logger was written in C# using the Windows Forms GUI library for .NET Windows desktop applications. It also uses SQLite to store your data locally.

## Setup

Using Windows 10/11, simply download the project files and compile the source code using Visual Studio or your favorite IDE for C#. From there, run the program executable to begin!

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

<br>

If you are a new user, you must first create a user profile to begin using Habit Logger. Click on the New User hyperlink at the bottom of the window to begin.

<br>

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

<br>

#### Main Window

Once you have logged into the application, you will see the following window, from which you may create habits and add them to dates:
<img width="2860" height="1929" alt="Image" src="https://github.com/user-attachments/assets/5457c5a1-d805-4d4e-bd49-9d710f1fa436" />

```
Annotations:
1. 
```

<br>

<img width="1516" height="1015" alt="Image" src="https://github.com/user-attachments/assets/a091d23a-701b-4e6b-85e2-b919d60e167f" />

---

<br>

Project Outline Source: https://www.thecsharpacademy.com/project/12/habit-logger