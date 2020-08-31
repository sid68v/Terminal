using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public enum Screen
{
    MainMenu, Password, Win
}


public class Hacker : MonoBehaviour
{
    [Serializable]
    public class PassWords
    {
        //public const string[] LEVEL_1_PASSWORDS = new string[]["books", "teacher", "maths", "science", "play"]; //library
        //public const string LEVEL_2_PASSWORDS = ["warden", "confinement", "prisoner", "violence"]; // police station
        //public const string LEVEL_3_PASSWORDS = ["supernova", "eclipse", "andromeda", "curiosity", "relativity"]; // NASA

        public List<PasswordsForLevel> PasswordsForLevels = new List<PasswordsForLevel>();
    }

    [Serializable]
    public class PasswordsForLevel
    {
        public string levelName;
        public List<string> passwordList = new List<string>();
    }


    public Screen currentScreen;
    int level;
    string currentPassword;
    public PassWords allPasswords = new PassWords();


    // Start is called before the first frame update
    void Start()
    {
        InitialiseGame();
    }

    void InitialiseGame()
    {

        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("Hello Stranger.");
        Terminal.WriteLine("What would you like to hack into ?");
        Terminal.WriteLine("Press 1 to select library.");
        Terminal.WriteLine("Press 2 to select police db.");
        Terminal.WriteLine("Press 3 to select NASA mainframe.");
        Terminal.WriteLine("Enter your selection.");
    }

    void OnUserInput(string input)
    {
        if (input == "menu")
        {
            InitialiseGame();
        }

        HandleinputsForScreens(input);
    }

    void HandleinputsForScreens(string input)
    {
        switch (currentScreen)
        {
            //Main menu.
            case Screen.MainMenu:
                switch (input)
                {
                    case "1": StartGame(input); break;
                    case "2": StartGame(input); break;
                    case "3": StartGame(input); break;
                    default: Terminal.WriteLine("Incorrect entry."); break;
                }
                break;

            // Password puzzle.
            case Screen.Password:
                if (IsPasswordValid(input, level))
                {
                    GameWon();
                }
                else
                {
                    Terminal.WriteLine("Incorrect Password.");
                    SetRandomPassword();
                }
                break;
            default: break;
        }
    }

    void GameWon()
    {
        Terminal.ClearScreen();
        currentScreen = Screen.Win;
        DisplayReward();
    }

    bool IsPasswordValid(string passwordEntered, int currentLevel)
    {
        return passwordEntered == currentPassword;

    }

    void StartGame(string enteredLevel)
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("Entered level " + enteredLevel);

        //assign level.
        int.TryParse(enteredLevel, out level);

        //assign current screen.
        currentScreen = Screen.Password;

        SetRandomPassword();
    }

    void SetRandomPassword()
    {
        Terminal.WriteLine("Password required");
        //assign password;
        currentPassword = allPasswords.PasswordsForLevels[level - 1].passwordList[
               Mathf.FloorToInt(
                   UnityEngine.Random.Range(0,
                   allPasswords.PasswordsForLevels[level - 1].passwordList.Count)
                   )
               ];

        Terminal.WriteLine("Password hint : " + currentPassword.Anagram());
        Debug.Log(currentPassword);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void DisplayReward()
    {
        Terminal.WriteLine("Type menu to go to home page.");
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Congrats. You have His diary.");
                Terminal.WriteLine(@"
    _/      Y      \_
   // ~~ ~~ | ~~ ~  \\
  // ~ ~ ~~ | ~~~ ~~ \\     
 //________.|.________\\    
`----------`-'----------'

                                    ");
                break;
            case 2:
                Terminal.WriteLine("He drew this map on the wall before he left.");
                Terminal.WriteLine(@"

      > _)
        \_.           /
           < S.Paulo /
            \   *_ /
             > º
            /    /
           <    /
            \^./
                                    ");
                break;
            case 3:
                Terminal.WriteLine("This was his last clue.");
                Terminal.WriteLine(@"
     ________________________________
    /                                |-_          
   /      .  |  . +91 9497515733          \          
  /      : \ | / :                       \         
 /        '-___-'                         \      
/_________________________________________ \      



                                    ");
                break;
        }
    }

}
