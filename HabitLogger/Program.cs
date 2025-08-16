// Console Menu
// allows user to view habits, create new habit, update habit, or delete habit
//      view habits: displays a list of existing habits, selecting the habit lists dates and quantity
//      create new habit: the user enters the name of the habit, then is prompted to enter a date and quantity until they type quit
//      update habit: displays list of existing habits, and the user may choose a habit to edit the name of, or a date that habit occurred to update
//      delete habit: displays list of existing habits, then the user may delete the entire habit from the db or zero or more dates associated with the habit



// class to control the db interface -- DatabaseControl -- controls each CRUD action to the db -- receives text input and returns text output 
// potentially a class to facilitate user input for CRUD -- Display -- loops through db to display data requests -- seems unnecessary