Entities:

1. Users
    - Relationships: 
        1. May have 0 or more habits; optional 1:M relationship with Habits.
    - Attributes:
        1. Unique KEY
        2. Username
    - Optional Attributes (may be added as a stretch goal):
        3. Salt 
        4. Password - hash using Argon2 with .NET PasswordHasher
        5. First name
        6. Last name
        7. Email

2. Habits
    - Relationships: 
        1. Must have one and only one Users; required 1:M relationship with Users.
        2. May occur on 0 or more Dates; optional M:N relationship with Dates.
    - Attributes:
        1. Unique KEY
        2. Name
        3. Description

3. Dates
    - Relationships: 
        1. Dates may have 0 or more Habits; optional M:N relationship with Habits.
    - Attributes:
        1. Unique KEY
        2. Date - MM/DD/YYYY format

4. Habits_has_Dates
    - Relationships:
        1. Intermediate table used to facilitate the M:N relationship between Habits and Dates.
        2. Must have a Habits and a Dates; required 1:M relationship with Dates AND Habits. 
    - Attributes:
        1. Habits KEY
        2. Dates KEY
        3. Quantity - number of times the habit occurred on this date
