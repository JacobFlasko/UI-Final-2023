Group: Jacob Flasko, Matthew Kurtz, Thomas Halligan and Randy Sinz

Setup Guide:

Before running the Selenium tests, please complete the following steps:

Update Empty Variables ☹️:

There are several variables in the test code that are currently empty or contain placeholders❗. Please replace these with the appropriate values:

Username and Password:
Replace [Enter Username Here] with your actual username. (Line 15)
Replace [Enter Password Here] with your actual password. (Line 16)
Web Driver Path:
Replace [Your Driver Path Here] with the actual path to your ChromeDriver executable. (Line 21)
Project URL:
Replace [Path to Project Here] with the URL of the ASP.NET application you are testing. (Line 23)
Additional Page URLs:
If your tests involve other pages within the application, add their URLs to the projectURL variable, separated by commas.
User Data:
Replace the placeholder values for startingWeight, curentWeight, desiredWeight, heightInInches, gender, ActivityLevel, birthday, and firstName, lastName, and email.
Element Xpaths:
Replace all occurrences of [Add The Xpath To The Element Here] with the actual Xpaths of the web elements you want to interact with.
Alternatively change the locator and use the ID or Class Name.
Home Page URL:
Replace Add Home Page URL Here with the URL of the application's home page. 2.
Copy Dependencies from my Pom.xml file:

Run the Tests ✅
