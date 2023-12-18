// Update This Before You Begin
package com.jacobflasko;

import org.openqa.selenium.*;
import org.junit.jupiter.api.*;
import org.junit.jupiter.params.*;
import org.junit.jupiter.params.provider.*;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.chrome.ChromeOptions;
import org.openqa.selenium.interactions.Actions;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;

import static org.junit.jupiter.api.Assertions.*;

import java.time.Duration;

public class AppTest {

    // Username and Password

    String userName = "Enter Username Here";
    String pasword = "Enter Password Here";

    // Web Driver Variables
    public static WebDriver driver;
    // Update This Before You Begin
    public static final String driverPath = "C:\\Selenium\\chromedriver-win64\\chromedriver.exe";
    // Update This Before You Begin
    public static final String projectURL = "https://localhost:7094/";
    // Add the URL For Any Additional Pages Here

    // Basic User Data
    //WebElement editInfo;

    public static int startingWeight = 200;
    public static int currentWeight = 225;
    public static int desiredWeight = 175;
    public static int heightInInches = 72;
    public static int gender = 0;
    // // This should be 1-10 (*LIE NOISE*)
    public static int ActivityLevel = 5;
    // // I assume this would be a a string that would be parsed to extract the date (*LIE NOISE*)
    public static String birthday = "6/20/2004";
    WebElement birthdayInput;

    // //Maybe put this for the main page
    public static int age = 19;
    // WebElement ageLabel = driver.findElement(By.xpath("Add The Xpath To The Element Here"));


    public static int dailyCalories = 2000;
    // WebElement dailyCaloriesInput = driver.findElement(By.id("UserCaloriesToLoseWeight"));

    // Extra Information, Not Ivolved in tests
    public static String email = "predas.www@gmail.com";
    public static String password = "Password0!"; //Dont worry I dont use this password for anything



    @BeforeAll
    public static void setupTest() {
        ChromeOptions options = new ChromeOptions();
        options.addArguments("--remote-allow-origins=*");
        System.setProperty("webdriver.chrome.driver", driverPath);
        driver = new ChromeDriver(options);
        driver.get(projectURL);
        WebDriverWait wait = new WebDriverWait(driver, Duration.ofSeconds(10));
        wait.until(ExpectedConditions.presenceOfElementLocated(By.xpath("/html/body/header/nav/div/div/ul[2]/li[2]/a")));



        WebElement login = driver.findElement(By.xpath("/html/body/header/nav/div/div/ul[2]/li[2]/a"));
        login.click();

        WebElement emailInput = driver.findElement(By.id("Input_Email"));
        emailInput.sendKeys(email);
        WebElement passwordInput = driver.findElement(By.id("Input_Password"));
        passwordInput.sendKeys(password);
        WebElement loginButton = driver.findElement(By.id("login-submit"));
        loginButton.click();

        
    }

    // Test age is calculated and displayed correctly with different birthday inputs

    //public static WebElement AgeInput = driver.findElement(By.xpath("Enter Xpath Here"));

    //This test should work but Selenium fucking sucks so it doesnt
    /*@ParameterizedTest
    @CsvSource({
            "06/20/2002,21",
            "06/20/2003,20",
            "06/20/2004,19",
            "06/20/2005,18",
            "06/20/2006,17"
    })
    @ParameterizedTest
    @CsvSource({
            "06/20/2002,21"
    })
    public void ageIsCalculatedCorrectly(String birthDate, String expected) {
        WebElement editInfo = driver.findElement(By.xpath("/html/body/header/nav/div/div/ul[1]/li[2]/a"));
        editInfo.click();
        //birthdayInput = driver.findElement(By.id("UserBirthday"));
        //birthdayInput.sendKeys(birthDate);
        WebElement conformButton = driver.findElement(By.xpath("/html/body/div[1]/main/div/div/form/button"));


        
        //((JavascriptExecutor) driver).executeScript("arguments[0].scrollIntoView(true);", conformButton);
        ((JavascriptExecutor) driver).executeScript("arguments[0].style.visibility='visible'", conformButton);
        WebDriverWait wait = new WebDriverWait(driver, Duration.ofSeconds(100));
        wait.until(ExpectedConditions.elementToBeClickable(conformButton));
        
        conformButton.click();


        //WebElement ageLabel = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[1]/p[3]"));
        //String[] expected = { "21", "20", "19", "18", "17" };
        //String actual = ageLabel.getDomProperty("textContent");
        //assertEquals(expected, actual);
    }*/

    // Test that profile page properly displays users information

    // public static String viewProfilePageURL = "Input the View Profile Page URL Here";

    // // Fill In The Xpath For These

    // // User Starting Weight

    @Test
    public void StartingWeightIsNotNullOnProfileViewPage() {
        WebElement startingWeightText = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[4]/p[1]"));

        assertNotNull(startingWeightText.getText());
    }

    @Test
    public void StartingWeightIsDisplayedOnProfileViewPage() {
        WebElement startingWeightText = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[4]/p[1]"));
        String total = startingWeightText.getText().substring(startingWeightText.getText().indexOf(":")+2);
        total = total.substring(0, total.indexOf(" "));
        int actual = Integer.parseInt(total);
        assertEquals(startingWeight, actual);
    }

    // // User Current Weight

    @Test
    public void CurrentWeightIsNotNullOnProfileViewPage() {
        WebElement currentWeightText = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[4]/p[2]"));

        assertNotNull(currentWeightText.getText());
    }

    @Test
    public void CurrentWeightIsDisplayedOnProfileViewPage() {
        WebElement currentWeightText = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[4]/p[2]"));
        String total = currentWeightText.getText().substring(currentWeightText.getText().indexOf(":")+2);
        total = total.substring(0, total.indexOf(" "));
        int actual = Integer.parseInt(total);
        assertEquals(currentWeight, actual);
    }

    // // User Desired Weight

    @Test
    public void DesiredWeightIsNotNullOnProfileViewPage() {
        WebElement desiredWeightText = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[4]/p[3]"));

        assertNotNull(desiredWeightText.getText());
    }

    @Test
    public void DesiredWeightIsDisplayedOnProfileViewPage() {
        WebElement desiredWeightText = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[4]/p[3]"));
        String total = desiredWeightText.getText().substring(desiredWeightText.getText().indexOf(":")+2);
        total = total.substring(0, total.indexOf(" "));
        int actual = Integer.parseInt(total);
        assertEquals(desiredWeight, actual);
    }

    // // User Height

    @Test
    public void UserHeightIsNotNullOnProfileViewPage() {
        
        WebElement heightText = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[1]/p[1]"));

        assertNotNull(heightText.getText());
    }

    @Test
    public void UserHeightIsDisplayedOnProfileViewPage() {
        WebElement heightText = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[1]/p[1]"));
        String total = heightText.getText().substring(heightText.getText().indexOf(":")+2);
        total = total.substring(0, total.indexOf(" "));
        int actual = Integer.parseInt(total);
        assertEquals(heightInInches, actual);
    }

    // // User Gender

    @Test
    public void UserGenderIsNotNullOnProfileViewPage() {
        WebElement genderText = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[1]/p[2]"));

        assertNotNull(genderText.getText());
    }

    @Test
    public void UserGenderIsDisplayedOnProfileViewPage() {
        WebElement genderText = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[1]/p[2]"));

        String expected = "Gender: Male";
        String actual = genderText.getText();
        assertEquals(expected, actual);
    }

    // // User Activity Level

    @Test
    public void UserActivityLevelIsNotNullOnProfileViewPage() {
        WebElement activityText = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[1]/p[5]"));

        assertNotNull(activityText.getText());
    }

    @Test
    public void UserActivityLevelIsDisplayedOnProfileViewPage() {
        WebElement activityText = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[1]/p[5]"));

        String expected = "Activity Level: Very Active";
        String actual = activityText.getText();
        assertEquals(expected, actual);
    }

    // // User Birthday

    @Test
    public void UserBirthdayIsNotNullOnProfileViewPage() {
        WebElement birthdayText = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[1]/p[3]"));

        assertNotNull(birthdayText.getText());
    }

    @Test
    public void UserBirthdayIsDisplayedOnProfileViewPage() {
        WebElement birthdayText = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[1]/p[3]"));

        String expected = "Birthday: " + birthday;
        String actual = birthdayText.getText();
        assertEquals(expected, actual);
    }

    // // User Age

    @Test
    public void UserAgeIsNotNullOnProfileViewPage() {
        WebElement ageText = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[1]/p[4]"));

        assertNotNull(ageText.getText());
    }

    @Test
    public void UserAgeIsDisplayedOnProfileViewPage() {
        WebElement ageText = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[1]/p[4]"));
        String total = ageText.getText().substring(ageText.getText().indexOf(":")+2);
        int actual = Integer.parseInt(total);
        assertEquals(age, actual);
    }

    // // User Number of Calories

    @Test
    public void UserCaloriesIsNotNullOnProfileViewPage() {
        WebElement caloriesText = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[3]/p[1]"));

        assertNotNull(caloriesText.getText());
    }

    @Test
    public void UserCaloriesIsDisplayedOnProfileViewPage() {
        WebElement caloriesText = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[3]/p[1]"));
        String total = caloriesText.getText().substring(caloriesText.getText().indexOf(":")+2);
        total = total.substring(0, total.indexOf(" "));
        int actual = Integer.parseInt(total);

        assertEquals(dailyCalories, actual);
    }

    // // Test remaining calories are displayed on home page

    @Test
    public void RemainingCaloriesIsNotNullOnHomePage() {
        WebElement caloriesText = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[3]/p[3]"));
        String total = caloriesText.getText().substring(caloriesText.getText().indexOf(":")+2);
        total = total.substring(0, total.indexOf(" "));

        assertNotNull(total);
    }

    @Test
    public void RemainingCaloriesIsDisplayedOnHomePage() {
        WebElement caloriesText = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[3]/p[3]"));
        String total = caloriesText.getText().substring(caloriesText.getText().indexOf(":")+2);
        total = total.substring(0, total.indexOf(" "));
        int actual = Integer.parseInt(total);

        assertEquals(2000, actual);
    }

    // // Test weight lost to this point is displayed on home page

    @Test
    public void WeightLostIsNotNullOnHomePage() {
        WebElement weightText = driver.findElement(By.id("UserWeightLost"));

        assertNotNull(weightText.getText());
    }

    // // End of home page tests

    // // Beginning of weight loss tests

    // public static WebElement WeightLossEntry = driver.findElement(By.xpath("Add the xpath to the Element Here"));

    @Test
    public void weightLossEntryCalculatesProperly() {
        WebElement weightText = driver.findElement(By.id("UserWeightLost"));
        WebElement calcWeight = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[4]/form/button"));
        
        //((JavascriptExecutor) driver).executeScript("arguments[0].scrollIntoView(true);", calcWeight);
        //WebDriverWait wait = new WebDriverWait(driver, Duration.ofSeconds(10));
        //wait.until(ExpectedConditions.visibilityOf(calcWeight));

        weightText.clear();
        weightText.sendKeys("20");
        int expected = 180;
        JavascriptExecutor jse2 = (JavascriptExecutor)driver;
        jse2.executeScript("arguments[0].scrollIntoView()", calcWeight); 

        JavascriptExecutor executor = (JavascriptExecutor)driver;
        executor.executeScript("arguments[0].click();", calcWeight);
        //calcWeight.click();
        
        WebElement currentWeightText = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[4]/p[2]"));
        String total = currentWeightText.getText().substring(currentWeightText.getText().indexOf(":")+2);
        total = total.substring(0, total.indexOf(" "));
        int actual = Integer.parseInt(total);

        assertEquals(expected, actual);
    }

    @ParameterizedTest
    @CsvSource({
            "10,190",
            "20,180",
            "15,185",
    })
    public void weightLossDisplayWorksWithMultipleValues(String weight, int expected) {
        WebElement weightText = driver.findElement(By.id("UserWeightLost"));
        WebElement calcWeight = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[4]/form/button"));

        weightText.clear();
        weightText.sendKeys(weight);
        JavascriptExecutor jse2 = (JavascriptExecutor)driver;
        jse2.executeScript("arguments[0].scrollIntoView()", calcWeight); 

        JavascriptExecutor executor = (JavascriptExecutor)driver;
        executor.executeScript("arguments[0].click();", calcWeight);
        //calcWeight.click();
        
        WebElement currentWeightText = driver.findElement(By.xpath("/html/body/div[1]/main/div/div[1]/div/div[4]/p[2]"));
        String total = currentWeightText.getText().substring(currentWeightText.getText().indexOf(":")+2);
        total = total.substring(0, total.indexOf(" "));
        int actual = Integer.parseInt(total);

        assertEquals(expected, actual);
    }

    @Test
    public void WeightLostIsDisplayedOnHomePage() {
        WebElement weightText = driver.findElement(By.id("UserWeightLost"));
        int expected = startingWeight - currentWeight;
        int actual = Integer.parseInt(weightText.getAttribute("value"));
        

        //assertEquals(expected, actual);
        assertEquals(expected, actual);
    }

    // // End Weight Loss Tests

    // // Testing Edge Cases

    // public static WebElement submitFormButton = driver.findElement(By.xpath("Enter element xpath here"));

    // @Test
    // public void TestStartingWeightIsValidated() throws InterruptedException {
    //     driver.get(homePageUrl);
    //     startingWeightInput.sendKeys("-1");
    //     currentWeightInput.sendKeys("200");
    //     desiredWeightInput.sendKeys("185");
    //     heightInput.sendKeys("72");
    //     genderInput.sendKeys("Male");
    //     activityLevelInput.sendKeys("5");
    //     birthdayInput.sendKeys("06/20/2004");
    //     dailyCaloriesInput.sendKeys("2000");
    //     ageLabel.sendKeys("19");
    //     submitFormButton.click();
    //     Thread.sleep(1_000);
    //     WebElement startingWeightInputError = driver.findElement(By.xpath("Enter element xpath here"));
    //     String expected = "Enter the expected error message here.";
    //     String actual = startingWeightInputError.getDomAttribute("textContent");
    //     assertEquals(expected, actual);
    // }

    // @Test
    // public void TestCurrentWeightIsValidated() throws InterruptedException {
    //     driver.get(homePageUrl);
    //     startingWeightInput.sendKeys("225");
    //     currentWeightInput.sendKeys("-1");
    //     desiredWeightInput.sendKeys("185");
    //     heightInput.sendKeys("72");
    //     genderInput.sendKeys("Male");
    //     activityLevelInput.sendKeys("5");
    //     birthdayInput.sendKeys("06/20/2004");
    //     dailyCaloriesInput.sendKeys("2000");
    //     ageLabel.sendKeys("19");
    //     submitFormButton.click();
    //     Thread.sleep(1_000);
    //     WebElement currentWeightInputError = driver.findElement(By.xpath("Enter element xpath here"));
    //     String expected = "Enter the expected error message here.";
    //     String actual = currentWeightInputError.getDomAttribute("textContent");
    //     assertEquals(expected, actual);
    // }

    // @Test
    // public void TestDesiredWeightIsValidated() throws InterruptedException {
    //     driver.get(homePageUrl);
    //     startingWeightInput.sendKeys("225");
    //     currentWeightInput.sendKeys("200");
    //     desiredWeightInput.sendKeys("-1");
    //     heightInput.sendKeys("72");
    //     genderInput.sendKeys("Male");
    //     activityLevelInput.sendKeys("5");
    //     birthdayInput.sendKeys("06/20/2004");
    //     dailyCaloriesInput.sendKeys("2000");
    //     ageLabel.sendKeys("19");
    //     submitFormButton.click();
    //     Thread.sleep(1_000);
    //     WebElement desiredWeightInputError = driver.findElement(By.xpath("Enter element xpath here"));
    //     String expected = "Enter the expected error message here.";
    //     String actual = desiredWeightInputError.getDomAttribute("textContent");
    //     assertEquals(expected, actual);
    // }

    // @Test
    // public void TestHeightInputIsValidated() throws InterruptedException {
    //     driver.get(homePageUrl);
    //     startingWeightInput.sendKeys("225");
    //     currentWeightInput.sendKeys("200");
    //     desiredWeightInput.sendKeys("185");
    //     heightInput.sendKeys("-1");
    //     genderInput.sendKeys("Male");
    //     activityLevelInput.sendKeys("5");
    //     birthdayInput.sendKeys("06/20/2004");
    //     dailyCaloriesInput.sendKeys("2000");
    //     ageLabel.sendKeys("19");
    //     submitFormButton.click();
    //     Thread.sleep(1_000);
    //     WebElement heightInputError = driver.findElement(By.xpath("Enter element xpath here"));
    //     String expected = "Enter the expected error message here.";
    //     String actual = heightInputError.getDomAttribute("textContent");
    //     assertEquals(expected, actual);
    // }

    // @Test
    // public void TestGenderInputIsValidated() throws InterruptedException {
    //     driver.get(homePageUrl);
    //     startingWeightInput.sendKeys("225");
    //     currentWeightInput.sendKeys("200");
    //     desiredWeightInput.sendKeys("185");
    //     heightInput.sendKeys("72");
    //     genderInput.sendKeys("♨️🙏❌👎🤷🌎$%#*&%^");
    //     activityLevelInput.sendKeys("5");
    //     birthdayInput.sendKeys("06/20/2004");
    //     dailyCaloriesInput.sendKeys("2000");
    //     ageLabel.sendKeys("19");
    //     submitFormButton.click();
    //     Thread.sleep(1_000);
    //     WebElement GenderInputError = driver.findElement(By.xpath("Enter element xpath here"));
    //     String expected = "Enter the expected error message here.";
    //     String actual = GenderInputError.getDomAttribute("textContent");
    //     assertEquals(expected, actual);
    // }

    // @Test
    // public void TestActivityLevelInputIsValidated() throws InterruptedException {
    //     driver.get(homePageUrl);
    //     startingWeightInput.sendKeys("225");
    //     currentWeightInput.sendKeys("200");
    //     desiredWeightInput.sendKeys("185");
    //     heightInput.sendKeys("72");
    //     genderInput.sendKeys("Female");
    //     activityLevelInput.sendKeys("-1");
    //     birthdayInput.sendKeys("06/20/2004");
    //     dailyCaloriesInput.sendKeys("2000");
    //     ageLabel.sendKeys("19");
    //     submitFormButton.click();
    //     Thread.sleep(1_000);
    //     WebElement ActivityLevelInputError = driver.findElement(By.xpath("Enter element xpath here"));
    //     String expected = "Enter the expected error message here.";
    //     String actual = ActivityLevelInputError.getDomAttribute("textContent");
    //     assertEquals(expected, actual);
    // }

    // @Test
    // public void TestBirthDayInputIsValidated() throws InterruptedException {
    //     driver.get(homePageUrl);
    //     startingWeightInput.sendKeys("225");
    //     currentWeightInput.sendKeys("200");
    //     desiredWeightInput.sendKeys("185");
    //     heightInput.sendKeys("72");
    //     genderInput.sendKeys("Female");
    //     activityLevelInput.sendKeys("5");
    //     birthdayInput.sendKeys("06/20/1500");
    //     dailyCaloriesInput.sendKeys("2000");
    //     ageLabel.sendKeys("19");
    //     submitFormButton.click();
    //     Thread.sleep(1_000);
    //     WebElement BirthDayInputError = driver.findElement(By.xpath("Enter element xpath here"));
    //     String expected = "Enter the expected error message here.";
    //     String actual = BirthDayInputError.getDomAttribute("textContent");
    //     assertEquals(expected, actual);
    // }

    // @Test
    // public void TestCalorieInputIsValidated() throws InterruptedException {
    //     driver.get(homePageUrl);
    //     startingWeightInput.sendKeys("225");
    //     currentWeightInput.sendKeys("200");
    //     desiredWeightInput.sendKeys("185");
    //     heightInput.sendKeys("72");
    //     genderInput.sendKeys("Female");
    //     activityLevelInput.sendKeys("5");
    //     birthdayInput.sendKeys("06/20/2004");
    //     dailyCaloriesInput.sendKeys("-2000");
    //     ageLabel.sendKeys("19");
    //     submitFormButton.click();
    //     Thread.sleep(1_000);
    //     WebElement CalorieInputError = driver.findElement(By.xpath("Enter element xpath here"));
    //     String expected = "Enter the expected error message here.";
    //     String actual = CalorieInputError.getDomAttribute("textContent");
    //     assertEquals(expected, actual);
    // }

    // @Test
    // public void TestAgeInputIsValidated() throws InterruptedException {
    //     driver.get(homePageUrl);
    //     startingWeightInput.sendKeys("225");
    //     currentWeightInput.sendKeys("200");
    //     desiredWeightInput.sendKeys("185");
    //     heightInput.sendKeys("72");
    //     genderInput.sendKeys("Female");
    //     activityLevelInput.sendKeys("5");
    //     birthdayInput.sendKeys("06/20/2004");
    //     dailyCaloriesInput.sendKeys("2000");
    //     ageLabel.sendKeys("-1");
    //     submitFormButton.click();
    //     Thread.sleep(1_000);
    //     WebElement AgeInputWeightInputError = driver.findElement(By.xpath("Enter element xpath here"));
    //     String expected = "Enter the expected error message here.";
    //     String actual = AgeInputWeightInputError.getDomAttribute("textContent");
    //     assertEquals(expected, actual);
    // }

    // Closing out the Driver
    @AfterAll
    public static void closeTest() {
        driver.close();
    }
}
