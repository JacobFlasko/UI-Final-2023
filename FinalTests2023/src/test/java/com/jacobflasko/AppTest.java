// Update This Before You Begin
package com.jacobflasko;

import org.openqa.selenium.*;
import org.junit.jupiter.api.*;
import org.junit.jupiter.params.*;
import org.junit.jupiter.params.provider.*;
import org.openqa.selenium.chrome.ChromeDriver;
import static org.junit.jupiter.api.Assertions.*;

public class AppTest {

    // Username and Password

    String userName = "Enter Username Here";
    String pasword = "Enter Password Here";

    // Web Driver Variables
    public static WebDriver driver;
    // Update This Before You Begin
    public static final String driverPath = "Your Driver Path Here";
    // Update This Before You Begin
    public static final String projectURL = "Path to Project Here";
    // Add the URL For Any Additional Pages Here

    // Basic User Data
    public static String startingWeight = "200";
    WebElement startingWeightInput = driver.findElement(By.xpath("Add The Xpath To The Element Here"));
    public static String curentWeight = "225";
    WebElement currentWeightInput = driver.findElement(By.xpath("Add The Xpath To The Element Here"));
    public static double desiredWeight = 175;
    WebElement desiredWeightInput = driver.findElement(By.xpath("Add The Xpath To The Element Here"));
    public static double heightInInches = 72;
    WebElement heightInput = driver.findElement(By.xpath("Add The Xpath To The Element Here"));
    public static String gender = "Male";
    WebElement genderInput = driver.findElement(By.xpath("Add The Xpath To The Element Here"));
    // This should be 1-10
    public static int ActivityLevel = 5;
    WebElement activityLevelInput = driver.findElement(By.xpath("Add The Xpath To The Element Here"));
    // I assume this would be a a string that would be parsed to extract the date
    public static String birthday = "06/20/2004";
    WebElement birthdayInput = driver.findElement(By.xpath("Add The Xpath To The Element Here"));
    public static int age;
    WebElement ageLabel = driver.findElement(By.xpath("Add The Xpath To The Element Here"));
    public static int dailyCalories = 2_000;
    WebElement dailyCaloriesInput = driver.findElement(By.xpath("Add The Xpath To The Element Here"));

    // Extra Information, Not Ivolved in tests
    public static String firstName;
    public static String lastName;
    public static String email;

    @BeforeAll
    public static void setupTest() {
        System.setProperty("webdriver.chrome.driver", driverPath);
        driver = new ChromeDriver();
    }

    // Test age is calculated and displayed correctly with different birthday inputs

    public static WebElement AgeInput = driver.findElement(By.xpath("Enter Xpath Here"));

    @ParameterizedTest
    @CsvSource({
            "06/20/2002",
            "06/20/2003",
            "06/20/2004",
            "06/20/2005",
            "06/20/2006"
    })
    public void ageIsCalculatedCorrectly(String birthDate) {
        driver.get(projectURL);
        AgeInput.sendKeys(birthDate);
        String[] expected = { "21", "20", "19", "18", "17" };
        String actual = ageLabel.getDomProperty("textContent");
        assertEquals(expected, actual);
    }

    // Test that profile page properly displays users information

    public static String viewProfilePageURL = "Input the View Profile Page URL Here";

    // Fill In The Xpath For These

    public static WebElement ProfileStartingWeightDisplay = driver
            .findElement(By.xpath("Add The Xpath To The Element Here"));

    public static WebElement ProfileCurrentWeightDisplay = driver
            .findElement(By.xpath("Add The Xpath To The Element Here"));

    public static WebElement ProfileDesiredWeightDisplay = driver
            .findElement(By.xpath("Add The Xpath To The Element Here"));

    public static WebElement UserHeightDisplay = driver
            .findElement(By.xpath("Add The Xpath To The Element Here"));

    public static WebElement UserGenderDisplay = driver
            .findElement(By.xpath("Add The Xpath To The Element Here"));

    public static WebElement UserActivityLevelDisplay = driver
            .findElement(By.xpath("Add The Xpath To The Element Here"));

    public static WebElement UserBirthdayDisplay = driver
            .findElement(By.xpath("Add The Xpath To The Element Here"));

    public static WebElement UserAgeDisplay = driver
            .findElement(By.xpath("Add The Xpath To The Element Here"));

    public static WebElement UserCalorieDisplay = driver
            .findElement(By.xpath("Add The Xpath To The Element Here"));

    // User Starting Weight

    @Test
    public void StartingWeightIsNotNullOnProfileViewPage() {
        driver.get(viewProfilePageURL);
        ProfileStartingWeightDisplay.sendKeys(startingWeight);
        String actual = ProfileStartingWeightDisplay.getDomProperty("textContent");
        assertNotNull(actual);
    }

    @Test
    public void StartingWeightIsDisplayedOnProfileViewPage() {
        driver.get(viewProfilePageURL);
        ProfileStartingWeightDisplay.sendKeys(startingWeight);
        String expected = startingWeight;
        String actual = ProfileStartingWeightDisplay.getDomProperty("textContent");
        assertEquals(expected, actual);
    }

    // User Current Weight

    @Test
    public void CurrentWeightIsNotNullOnProfileViewPage() {
        driver.get(viewProfilePageURL);
        ProfileCurrentWeightDisplay.sendKeys(curentWeight);
        String actual = ProfileCurrentWeightDisplay.getDomProperty("textContent");
        assertNotNull(actual);
    }

    @Test
    public void CurrentWeightIsDisplayedOnProfileViewPage() {
        driver.get(viewProfilePageURL);
        String expected = curentWeight;
        String actual = ProfileCurrentWeightDisplay.getDomProperty("textContent");
        assertEquals(expected, actual);
    }

    // User Desired Weight

    @Test
    public void DesiredWeightIsNotNullOnProfileViewPage() {
        driver.get(viewProfilePageURL);
        String actual = ProfileDesiredWeightDisplay.getDomProperty("textContent");
        assertNotNull(actual);
    }

    @Test
    public void DesiredWeightIsDisplayedOnProfileViewPage() {
        driver.get(viewProfilePageURL);
        double expected = desiredWeight;
        String actual = ProfileDesiredWeightDisplay.getDomProperty("textContent");
        assertEquals(expected, actual);
    }

    // User Height

    @Test
    public void UserHeightIsNotNullOnProfileViewPage() {
        driver.get(viewProfilePageURL);
        String actual = UserHeightDisplay.getDomProperty("textContent");
        assertNotNull(actual);
    }

    @Test
    public void UserHeightIsDisplayedOnProfileViewPage() {
        driver.get(viewProfilePageURL);
        double expected = heightInInches;
        String actual = UserHeightDisplay.getDomProperty("textContent");
        assertEquals(expected, actual);
    }

    // User Gender

    @Test
    public void UserGenderIsNotNullOnProfileViewPage() {
        driver.get(viewProfilePageURL);
        String actual = UserGenderDisplay.getDomProperty("textContent");
        assertNotNull(actual);
    }

    @Test
    public void UserGenderIsDisplayedOnProfileViewPage() {
        driver.get(viewProfilePageURL);
        String expected = gender;
        String actual = UserGenderDisplay.getDomProperty("textContent");
        assertEquals(expected, actual);
    }

    // User Activity Level

    @Test
    public void UserActivityLevelIsNotNullOnProfileViewPage() {
        driver.get(viewProfilePageURL);
        String actual = UserActivityLevelDisplay.getDomProperty("textContent");
        assertNotNull(actual);
    }

    @Test
    public void UserActivityLevelIsDisplayedOnProfileViewPage() {
        driver.get(viewProfilePageURL);
        int expected = ActivityLevel;
        String actual = UserActivityLevelDisplay.getDomProperty("textContent");
        assertEquals(expected, actual);
    }

    // User Birthday

    @Test
    public void UserBirthdayIsNotNullOnProfileViewPage() {
        driver.get(viewProfilePageURL);
        String actual = UserBirthdayDisplay.getDomProperty("textContent");
        assertNotNull(actual);
    }

    @Test
    public void UserBirthdayIsDisplayedOnProfileViewPage() {
        driver.get(viewProfilePageURL);
        String expected = birthday;
        String actual = UserBirthdayDisplay.getDomProperty("textContent");
        assertEquals(expected, actual);
    }

    // User Age

    @Test
    public void UserAgeIsNotNullOnProfileViewPage() {
        driver.get(viewProfilePageURL);
        String actual = UserAgeDisplay.getDomProperty("textContent");
        assertNotNull(actual);
    }

    @Test
    public void UserAgeIsDisplayedOnProfileViewPage() {
        driver.get(viewProfilePageURL);
        int expected = age;
        String actual = UserAgeDisplay.getDomProperty("textContent");
        assertEquals(expected, actual);
    }

    // User Number of Calories

    @Test
    public void UserCaloriesIsNotNullOnProfileViewPage() {
        driver.get(viewProfilePageURL);
        String actual = UserCalorieDisplay.getDomProperty("textContent");
        assertNotNull(actual);
    }

    @Test
    public void UserCaloriesIsDisplayedOnProfileViewPage() {
        driver.get(viewProfilePageURL);
        int expected = dailyCalories;
        String actual = UserCalorieDisplay.getDomProperty("textContent");
        assertEquals(expected, actual);
    }

    // End of View Page Tests

    // Home Page Tests

    public static String homePageUrl = "Add Home Page URL Here";

    public static WebElement RemainingCalorieDisplay = driver
            .findElement(By.xpath("Add The Xpath To The Element Here"));

    public static WebElement WeightLostDisplay = driver
            .findElement(By.xpath("Add The Xpath To The Element Here"));

    // Test remaining calories are displayed on home page

    @Test
    public void RemainingCaloriesIsNotNullOnHomePage() {
        driver.get(homePageUrl);
        String actual = RemainingCalorieDisplay.getDomProperty("textContent");
        assertNotNull(actual);
    }

    @Test
    public void RemainingCaloriesIsDisplayedOnHomePage() {
        driver.get(homePageUrl);
        int expected = dailyCalories;
        String actual = RemainingCalorieDisplay.getDomProperty("textContent");
        assertEquals(expected, actual);
    }

    // Test weight lost to this point is displayed on home page

    @Test
    public void WeightLostIsNotNullOnHomePage() {
        driver.get(homePageUrl);
        String actual = WeightLostDisplay.getDomProperty("textContent");
        assertNotNull(actual);
    }

    // End of home page tests

    // Beginning of weight loss tests

    public static WebElement WeightLossEntry = driver.findElement(By.xpath("Add the xpath to the Element Here"));

    @Test
    public void weightLossEntryCalculatesProperly() {
        driver.get(homePageUrl);
        WeightLossEntry.sendKeys("20");
        String expected = "180";
        String actual = WeightLostDisplay.getDomProperty("textContent");
        assertEquals(expected, actual);
    }

    @Test
    public void weightLossDisplayIsNotNullAfterCalculations() {
        driver.get(homePageUrl);
        WeightLossEntry.sendKeys("20");
        String actual = WeightLostDisplay.getDomProperty("textContent");
        assertNotNull(actual);
    }

    @ParameterizedTest
    @ValueSource(ints = { 10, 20, 15, 15 })
    public void weightLossDisplayWorksWithMultipleValues(int losses) {
        driver.get(homePageUrl);
        int[] expected = { 190, 170, 155, 140 };
        String actual = WeightLostDisplay.getDomProperty("textContext");
        assertEquals(expected, actual);
    }

    @Test
    public void WeightLostIsDisplayedOnHomePage() {
        driver.get(homePageUrl);
        int expected = dailyCalories;
        String actual = WeightLostDisplay.getDomProperty("textContent");
        assertEquals(expected, actual);
    }

    // End Weight Loss Tests

    // Testing Edge Cases

    public static WebElement submitFormButton = driver.findElement(By.xpath("Enter element xpath here"));

    @Test
    public void TestStartingWeightIsValidated() throws InterruptedException {
        driver.get(homePageUrl);
        startingWeightInput.sendKeys("-1");
        currentWeightInput.sendKeys("200");
        desiredWeightInput.sendKeys("185");
        heightInput.sendKeys("72");
        genderInput.sendKeys("Male");
        activityLevelInput.sendKeys("5");
        birthdayInput.sendKeys("06/20/2004");
        dailyCaloriesInput.sendKeys("2000");
        ageLabel.sendKeys("19");
        submitFormButton.click();
        Thread.sleep(1_000);
        WebElement startingWeightInputError = driver.findElement(By.xpath("Enter element xpath here"));
        String expected = "Enter the expected error message here.";
        String actual = startingWeightInputError.getDomAttribute("textContent");
        assertEquals(expected, actual);
    }

    @Test
    public void TestCurrentWeightIsValidated() throws InterruptedException {
        driver.get(homePageUrl);
        startingWeightInput.sendKeys("225");
        currentWeightInput.sendKeys("-1");
        desiredWeightInput.sendKeys("185");
        heightInput.sendKeys("72");
        genderInput.sendKeys("Male");
        activityLevelInput.sendKeys("5");
        birthdayInput.sendKeys("06/20/2004");
        dailyCaloriesInput.sendKeys("2000");
        ageLabel.sendKeys("19");
        submitFormButton.click();
        Thread.sleep(1_000);
        WebElement currentWeightInputError = driver.findElement(By.xpath("Enter element xpath here"));
        String expected = "Enter the expected error message here.";
        String actual = currentWeightInputError.getDomAttribute("textContent");
        assertEquals(expected, actual);
    }

    @Test
    public void TestDesiredWeightIsValidated() throws InterruptedException {
        driver.get(homePageUrl);
        startingWeightInput.sendKeys("225");
        currentWeightInput.sendKeys("200");
        desiredWeightInput.sendKeys("-1");
        heightInput.sendKeys("72");
        genderInput.sendKeys("Male");
        activityLevelInput.sendKeys("5");
        birthdayInput.sendKeys("06/20/2004");
        dailyCaloriesInput.sendKeys("2000");
        ageLabel.sendKeys("19");
        submitFormButton.click();
        Thread.sleep(1_000);
        WebElement desiredWeightInputError = driver.findElement(By.xpath("Enter element xpath here"));
        String expected = "Enter the expected error message here.";
        String actual = desiredWeightInputError.getDomAttribute("textContent");
        assertEquals(expected, actual);
    }

    @Test
    public void TestHeightInputIsValidated() throws InterruptedException {
        driver.get(homePageUrl);
        startingWeightInput.sendKeys("225");
        currentWeightInput.sendKeys("200");
        desiredWeightInput.sendKeys("185");
        heightInput.sendKeys("-1");
        genderInput.sendKeys("Male");
        activityLevelInput.sendKeys("5");
        birthdayInput.sendKeys("06/20/2004");
        dailyCaloriesInput.sendKeys("2000");
        ageLabel.sendKeys("19");
        submitFormButton.click();
        Thread.sleep(1_000);
        WebElement heightInputError = driver.findElement(By.xpath("Enter element xpath here"));
        String expected = "Enter the expected error message here.";
        String actual = heightInputError.getDomAttribute("textContent");
        assertEquals(expected, actual);
    }

    @Test
    public void TestGenderInputIsValidated() throws InterruptedException {
        driver.get(homePageUrl);
        startingWeightInput.sendKeys("225");
        currentWeightInput.sendKeys("200");
        desiredWeightInput.sendKeys("185");
        heightInput.sendKeys("72");
        genderInput.sendKeys("♨️🙏❌👎🤷🌎$%#*&%^");
        activityLevelInput.sendKeys("5");
        birthdayInput.sendKeys("06/20/2004");
        dailyCaloriesInput.sendKeys("2000");
        ageLabel.sendKeys("19");
        submitFormButton.click();
        Thread.sleep(1_000);
        WebElement GenderInputError = driver.findElement(By.xpath("Enter element xpath here"));
        String expected = "Enter the expected error message here.";
        String actual = GenderInputError.getDomAttribute("textContent");
        assertEquals(expected, actual);
    }

    @Test
    public void TestActivityLevelInputIsValidated() throws InterruptedException {
        driver.get(homePageUrl);
        startingWeightInput.sendKeys("225");
        currentWeightInput.sendKeys("200");
        desiredWeightInput.sendKeys("185");
        heightInput.sendKeys("72");
        genderInput.sendKeys("Female");
        activityLevelInput.sendKeys("-1");
        birthdayInput.sendKeys("06/20/2004");
        dailyCaloriesInput.sendKeys("2000");
        ageLabel.sendKeys("19");
        submitFormButton.click();
        Thread.sleep(1_000);
        WebElement ActivityLevelInputError = driver.findElement(By.xpath("Enter element xpath here"));
        String expected = "Enter the expected error message here.";
        String actual = ActivityLevelInputError.getDomAttribute("textContent");
        assertEquals(expected, actual);
    }

    @Test
    public void TestBirthDayInputIsValidated() throws InterruptedException {
        driver.get(homePageUrl);
        startingWeightInput.sendKeys("225");
        currentWeightInput.sendKeys("200");
        desiredWeightInput.sendKeys("185");
        heightInput.sendKeys("72");
        genderInput.sendKeys("Female");
        activityLevelInput.sendKeys("5");
        birthdayInput.sendKeys("06/20/1500");
        dailyCaloriesInput.sendKeys("2000");
        ageLabel.sendKeys("19");
        submitFormButton.click();
        Thread.sleep(1_000);
        WebElement BirthDayInputError = driver.findElement(By.xpath("Enter element xpath here"));
        String expected = "Enter the expected error message here.";
        String actual = BirthDayInputError.getDomAttribute("textContent");
        assertEquals(expected, actual);
    }

    @Test
    public void TestCalorieInputIsValidated() throws InterruptedException {
        driver.get(homePageUrl);
        startingWeightInput.sendKeys("225");
        currentWeightInput.sendKeys("200");
        desiredWeightInput.sendKeys("185");
        heightInput.sendKeys("72");
        genderInput.sendKeys("Female");
        activityLevelInput.sendKeys("5");
        birthdayInput.sendKeys("06/20/2004");
        dailyCaloriesInput.sendKeys("-2000");
        ageLabel.sendKeys("19");
        submitFormButton.click();
        Thread.sleep(1_000);
        WebElement CalorieInputError = driver.findElement(By.xpath("Enter element xpath here"));
        String expected = "Enter the expected error message here.";
        String actual = CalorieInputError.getDomAttribute("textContent");
        assertEquals(expected, actual);
    }

    @Test
    public void TestAgeInputIsValidated() throws InterruptedException {
        driver.get(homePageUrl);
        startingWeightInput.sendKeys("225");
        currentWeightInput.sendKeys("200");
        desiredWeightInput.sendKeys("185");
        heightInput.sendKeys("72");
        genderInput.sendKeys("Female");
        activityLevelInput.sendKeys("5");
        birthdayInput.sendKeys("06/20/2004");
        dailyCaloriesInput.sendKeys("2000");
        ageLabel.sendKeys("-1");
        submitFormButton.click();
        Thread.sleep(1_000);
        WebElement AgeInputWeightInputError = driver.findElement(By.xpath("Enter element xpath here"));
        String expected = "Enter the expected error message here.";
        String actual = AgeInputWeightInputError.getDomAttribute("textContent");
        assertEquals(expected, actual);
    }

    // Closing out the Driver
    @AfterAll
    public static void closeTest() {
        driver.close();
    }
}
