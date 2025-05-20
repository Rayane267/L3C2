using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Characters;
using AI_Class;
using Mono.Data.Sqlite;
using System.Data;
using System;


public enum PlayerType : byte { HUMAN, AI_CPU_Offense, AI_EASY, AI_MEDIUM, AI_HARD, AI_CPU_Defense };

// Data structure used to send game info when switching scenes
//Edited By Rayane TALEB L3C2
public class StartGameData
{
    public bool loadSave;
    public List<CharClass> charsTeam1 = new List<CharClass>();
    public List<CharClass> charsTeam2 = new List<CharClass>();
    public PlayerType player1Type;
    public PlayerType player2Type;
    public int mapChosen; // 0 = Ruins, 1 = Random
    public int nbGames;
    public float slider;
    public StartGameData() { }
}

///<summary>
///Functions and methods for the Main menu
///</summary>
// edited by Mengqian Xu L3C2
public class MainMenu : MonoBehaviour
{
    public static StartGameData startGameData;
    public Texture secondBackground;
    public Texture firstBackground;
    int nb = 5; //nb of characters (1 to 5)
    public GameObject background;
    public GameObject mainMenu;
    public GameObject CharSelectMenu;
    public GameObject advancedOptionsMenu;
    public List<Texture> charCards;
    public GameObject charsTeam1Display;
    public GameObject charsTeam2Display;
    public GameObject teamSelectHighlight;
    public GameObject credits;
    public GameObject guide;
    public GameObject buttonRandom1;
    public GameObject buttonRandom2;
    public GameObject GuideInterface; 
    public Text errorLoad;
    // Main menu buttons
    public Button buttonPlay;
    public Button buttonStats;
    public Button buttonLoad;
    public Button buttonQuit;
    public Button buttonCredits;
    public Button buttonGuide;
    // Char select menu buttons
    public List<Button> buttonCharCards;
    public List<Button> buttonTeam1Cards;
    public List<Button> buttonTeam2Cards;
    public Button buttonBackTeam1;
    public Button buttonBackTeam2;
    public Button buttonReadyTeam1;
    public Button buttonReadyTeam2;
    public Button buttonToAdvancedOptions;
    public Button buttonRandomTeam1;
    public Button buttonRandomTeam2;
    public Button buttonAide;
    // Advanced options menu buttons
    public Button buttonToCharSelect;
    public Button v1;
    public Button v2;
    public Button v3;
    public Button v4;
    public Button v5;
    public Toggle toggleConsoleMode;
    public Slider sliderNbGames;
    public GameObject textNbGames;

    bool v5Pressed = false;
    bool v4Pressed = false;
    bool v3Pressed = false;
    bool v2Pressed = false;
    bool v1Pressed = false;
    bool buttonPlayPressed = false;
    bool buttonLoadPressed = false;
    bool buttonStatsPressed = false;
    bool buttonQuitPressed = false;
    bool buttonCreditsPressed = false;
    bool buttonGuidePressed = false;
    bool buttonAidePressed = false; // edited by Mengqian Xu L3C2
    bool[] buttonCharCardsPressed = new bool[8] { false, false, false, false, false, false, false, false };// edited by Mengqian Xu L3C2
    bool[] buttonTeam1Enable = new bool[8] { true, true, true, true, true, true, true, true };// edited by Mengqian Xu L3C2
    bool[] buttonTeam2Enable = new bool[8] { true, true, true, true, true, true, true, true };// edited by Mengqian Xu L3C2
    bool[] buttonTeam1CardsPressed = new bool[5] { false, false, false, false, false };
    bool[] buttonTeam2CardsPressed = new bool[5] { false, false, false, false, false };
    bool buttonBackTeam1Pressed = false;
    bool buttonBackTeam2Pressed = false;
    bool buttonReadyTeam1Pressed = false;
    bool buttonReadyTeam2Pressed = false;
    bool buttonRandomTeam1Pressed = false;
    bool buttonRandomTeam2Pressed = false;
    bool buttonToAdvancedOptionsPressed = false;
    bool buttonToCharSelectPressed = false;

    public Slider slider;
    public Dropdown dropdownPlayer1Type;
    public Dropdown dropdownPlayer2Type;
    public Dropdown dropdownMap;
    PlayerType player1Type;
    PlayerType player2Type;

    List<CharClass> charsTeam1 = new List<CharClass>();
    List<CharClass> charsTeam2 = new List<CharClass>();
    int currentTeam = 0;
    bool consoleMode;
    int nbGames;

    //Awake is called before Start
    void Awake()
    {
        Application.targetFrameRate = 75;
        QualitySettings.vSyncCount = 0;
    }

    // Start is called before the first frame update
    // edited by Mengqian Xu L3C2
    void Start()
    {

        mainMenu.SetActive(true);
        buttonPlay.onClick.AddListener(buttonPlayPressed_);
        buttonLoad.onClick.AddListener(buttonLoadPressed_);
        buttonQuit.onClick.AddListener(buttonQuitPressed_);
        buttonStats.onClick.AddListener(buttonStatsPressed_);
        buttonCredits.onClick.AddListener(buttonCreditsPressed_);
        buttonGuide.onClick.AddListener(buttonGuidePressed_);
        charsTeam1Display.transform.GetChild(1).gameObject.SetActive(true);

        buttonCharCards[0].onClick.AddListener(() => buttonCharCardsPressed_(0));
        buttonCharCards[1].onClick.AddListener(() => buttonCharCardsPressed_(1));
        buttonCharCards[2].onClick.AddListener(() => buttonCharCardsPressed_(2));
        buttonCharCards[3].onClick.AddListener(() => buttonCharCardsPressed_(3));
        buttonCharCards[4].onClick.AddListener(() => buttonCharCardsPressed_(4));
        buttonCharCards[5].onClick.AddListener(() => buttonCharCardsPressed_(5));
        buttonCharCards[6].onClick.AddListener(() => buttonCharCardsPressed_(6));
        buttonCharCards[7].onClick.AddListener(() => buttonCharCardsPressed_(7));


        buttonTeam1Cards[0].onClick.AddListener(() => buttonTeam1CardsPressed_(0));
        buttonTeam1Cards[1].onClick.AddListener(() => buttonTeam1CardsPressed_(1));
        buttonTeam1Cards[2].onClick.AddListener(() => buttonTeam1CardsPressed_(2));
        buttonTeam1Cards[3].onClick.AddListener(() => buttonTeam1CardsPressed_(3));
        buttonTeam1Cards[4].onClick.AddListener(() => buttonTeam1CardsPressed_(4));
        buttonTeam2Cards[0].onClick.AddListener(() => buttonTeam2CardsPressed_(0));
        buttonTeam2Cards[1].onClick.AddListener(() => buttonTeam2CardsPressed_(1));
        buttonTeam2Cards[2].onClick.AddListener(() => buttonTeam2CardsPressed_(2));
        buttonTeam2Cards[3].onClick.AddListener(() => buttonTeam2CardsPressed_(3));
        buttonTeam2Cards[4].onClick.AddListener(() => buttonTeam2CardsPressed_(4));

        buttonBackTeam1.onClick.AddListener(buttonBackTeam1Pressed_);
        buttonBackTeam2.onClick.AddListener(buttonBackTeam2Pressed_);
        buttonReadyTeam1.onClick.AddListener(buttonReadyTeam1Pressed_);
        buttonRandomTeam1.onClick.AddListener(buttonRandomTeam1Pressed_);
        buttonRandomTeam2.onClick.AddListener(buttonRandomTeam2Pressed_);
        buttonReadyTeam2.onClick.AddListener(buttonReadyTeam2Pressed_);
        buttonToAdvancedOptions.onClick.AddListener(buttonToAdvancedOptionsPressed_);
        buttonAide.onClick.AddListener(buttonAidePressed_); 

        v5.onClick.AddListener(button5v5Pressed_);
        v4.onClick.AddListener(button4v4Pressed_);
        v3.onClick.AddListener(button3v3Pressed_);
        v2.onClick.AddListener(button2v2Pressed_);
        v1.onClick.AddListener(button1v1Pressed_);
        buttonToCharSelect.onClick.AddListener(buttonToCharSelectPressed_);

        //show load button if there is a save file
        if (File.Exists(Application.streamingAssetsPath + "/Save/saveGame.db"))
        {
            string conn = "URI=file:" + Application.streamingAssetsPath + "/Save/saveGame.db"; //Path to database.
            IDbConnection dbconn = (IDbConnection)new SqliteConnection(conn);
            
            dbconn.Open(); //Open connection to the database
        
            IDbCommand dbcmd = dbconn.CreateCommand();
            IDataReader reader;
           
            dbcmd.CommandText = "SELECT player1 NBchar FROM game";
            reader = dbcmd.ExecuteReader();
            reader.Read();
            int res = reader.GetInt32(0);
            Debug.Log("Res = " + res);
            
            if (res == -1)
            {
                buttonLoad.gameObject.SetActive(false);
            }
            else
            {
                buttonLoad.gameObject.SetActive(true);
            }
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            dbconn = null;
        }
        else
        {
            errorLoad.gameObject.SetActive(true);
        }


        consoleMode = true;
        nbGames = 1;
        // Read Options from options file
        if (File.Exists(Application.streamingAssetsPath + "/Data/Options/options"))
        {
            loadOptions();
        }
        else
        {
            saveOptions();
        }
        toggleConsoleMode.isOn = consoleMode;
        sliderNbGames.value = Mathf.Sqrt((float)nbGames);
        if (startGameData != null)
        {
            slider.value = startGameData.slider;
        }
    }

    
    // edited by Mengqian Xu L3C2
    void Update()
    {
        // MAIN MENU
        if (mainMenu.activeInHierarchy)
        {
            // Go to character selection menu
            if (buttonPlayPressed)
            {
                credits.SetActive(false);
                guide.SetActive(false);
                mainMenu.SetActive(false);
                CharSelectMenu.SetActive(true);
                initCharSelectMenu();
                buttonPlayPressed = false;
                background.GetComponent<RawImage>().texture = secondBackground;
            }
            // Load saved game
            if (buttonLoadPressed)
            {
                MainGame.startGameData = new StartGameData();
                MainGame.startGameData.loadSave = true;
                SceneManager.LoadScene(1);
                buttonLoadPressed = false;
            }
            // Show credits
            if (buttonCreditsPressed)
            {
                credits.SetActive(!credits.activeInHierarchy);
                buttonCreditsPressed = false;
            }

            //Guide
            if (buttonGuidePressed)
            {
                guide.SetActive(!guide.activeInHierarchy);
                buttonGuidePressed = false;
            }

            if (buttonStatsPressed)
            {
                SceneManager.LoadScene(2);
                buttonStatsPressed = false;
            }
            //Quit
            if (buttonQuitPressed)
            {
                quitGame();
            }

            

            // *************************
            // CHARACTER SELECTION MENU
            // *************************
        }
        else if (CharSelectMenu.activeInHierarchy)
        {
            //Show GuideInterface
            if(buttonAidePressed)
            {
                GuideInterface.SetActive(!GuideInterface.activeInHierarchy);
                buttonAidePressed = false;
            }
            // Back to main menu
            if (buttonBackTeam1Pressed)
            {
                for (int i = 0; i < 8; i++)
                {
                    buttonCharCards[i].gameObject.SetActive(true);
                    buttonTeam1Enable[i] = true;
                    buttonTeam2Enable[i] = true;
                }
                mainMenu.SetActive(true);
                background.GetComponent<RawImage>().texture = firstBackground;
                CharSelectMenu.SetActive(false);
                buttonBackTeam1Pressed = false;
            }

            // Back to team 1
            if (buttonBackTeam2Pressed)
            {
                for (int i = 0; i < 8; i++)
                    buttonCharCards[i].gameObject.SetActive(buttonTeam1Enable[i]);
                charSelectMenuPreviousPlayer();
                buttonBackTeam2Pressed = false;
            }

            {
                List<CharClass> charsTeam = (currentTeam == 0) ? charsTeam1 : charsTeam2;
                GameObject charsTeamDisplay = (currentTeam == 0) ? charsTeam1Display : charsTeam2Display;
                bool[] buttonTeamCardsPressed = (currentTeam == 0) ? buttonTeam1CardsPressed : buttonTeam2CardsPressed;

                //Display 1, 2, 3, 4 or 5 character slot
                if (v5Pressed)
                {
                    // Activate all character slot 
                    buttonTeam1Cards[4].gameObject.SetActive(true);
                    buttonTeam2Cards[4].gameObject.SetActive(true);
                    buttonTeam1Cards[3].gameObject.SetActive(true);
                    buttonTeam2Cards[3].gameObject.SetActive(true);
                    buttonTeam1Cards[2].gameObject.SetActive(true);
                    buttonTeam2Cards[2].gameObject.SetActive(true);
                    buttonTeam1Cards[1].gameObject.SetActive(true);
                    buttonTeam2Cards[1].gameObject.SetActive(true);

                    nb = 5;

                    testAndDisplayCharRoster();
                    v5Pressed = false;
                }
                if (v4Pressed)
                {
                    for (int i = 4; i > 3; i--)
                    {
                        charsTeam1Display.transform.GetChild(0).transform.GetChild(i).GetComponent<RawImage>().texture = charCards[8];
                        charsTeam2Display.transform.GetChild(0).transform.GetChild(i).GetComponent<RawImage>().texture = charCards[8];

                        if (charsTeam1.Count > i)
                        {
                            buttonTeam1Enable[(int)charsTeam1[i]] = true;
                            charsTeam1.RemoveAt(i);
                        }
                        if (charsTeam2.Count > i)
                        {
                            buttonTeam2Enable[(int)charsTeam2[i]] = true;
                            charsTeam2.RemoveAt(i);
                        }

                    }

                    // Activate character slot 1, 2, 3
                    buttonTeam1Cards[3].gameObject.SetActive(true);
                    buttonTeam2Cards[3].gameObject.SetActive(true);
                    buttonTeam1Cards[2].gameObject.SetActive(true);
                    buttonTeam2Cards[2].gameObject.SetActive(true);
                    buttonTeam1Cards[1].gameObject.SetActive(true);
                    buttonTeam2Cards[1].gameObject.SetActive(true);
                    // Deactivate character slot 4
                    buttonTeam1Cards[4].gameObject.SetActive(false);
                    buttonTeam2Cards[4].gameObject.SetActive(false);

                    nb = 4;

                    testAndDisplayCharRoster();

                    v4Pressed = false;
                }
                if (v3Pressed)
                {
                    for (int i = 4; i > 2; i--)
                    {
                        charsTeam1Display.transform.GetChild(0).transform.GetChild(i).GetComponent<RawImage>().texture = charCards[8];
                        charsTeam2Display.transform.GetChild(0).transform.GetChild(i).GetComponent<RawImage>().texture = charCards[8];

                        if (charsTeam1.Count > i)
                        {
                            buttonTeam1Enable[(int)charsTeam1[i]] = true;
                            charsTeam1.RemoveAt(i);
                        }
                        if (charsTeam2.Count > i)
                        {
                            buttonTeam2Enable[(int)charsTeam2[i]] = true;
                            charsTeam2.RemoveAt(i);
                        }

                        //buttonTeam1Cards[i].gameObject.SetActive(false);
                        //buttonTeam2Cards[i].gameObject.SetActive(false);
                    }
                    // Activate slot 1 and 2
                    buttonTeam1Cards[2].gameObject.SetActive(true);
                    buttonTeam2Cards[2].gameObject.SetActive(true);
                    buttonTeam1Cards[1].gameObject.SetActive(true);
                    buttonTeam2Cards[1].gameObject.SetActive(true);

                    // Deactivate character slot 3 and 4
                    buttonTeam1Cards[4].gameObject.SetActive(false);
                    buttonTeam2Cards[4].gameObject.SetActive(false);
                    buttonTeam1Cards[3].gameObject.SetActive(false);
                    buttonTeam2Cards[3].gameObject.SetActive(false);
                    nb = 3;

                    testAndDisplayCharRoster();

                    v3Pressed = false;
                }
                if (v2Pressed)
                {
                    for (int i = 4; i > 1; i--)
                    {
                        charsTeam1Display.transform.GetChild(0).transform.GetChild(i).GetComponent<RawImage>().texture = charCards[8];
                        charsTeam2Display.transform.GetChild(0).transform.GetChild(i).GetComponent<RawImage>().texture = charCards[8];

                        if (charsTeam1.Count > i)
                        {
                            buttonTeam1Enable[(int)charsTeam1[i]] = true;
                            charsTeam1.RemoveAt(i);
                        }
                        if (charsTeam2.Count > i)
                        {
                            buttonTeam2Enable[(int)charsTeam2[i]] = true;
                            charsTeam2.RemoveAt(i);
                        }

                    }



                    // Activate character slot 1
                    buttonTeam1Cards[1].gameObject.SetActive(true);
                    buttonTeam2Cards[1].gameObject.SetActive(true);

                    // Deactivate character slot 2, 3 and 4
                    buttonTeam1Cards[4].gameObject.SetActive(false);
                    buttonTeam2Cards[4].gameObject.SetActive(false);
                    buttonTeam1Cards[3].gameObject.SetActive(false);
                    buttonTeam2Cards[3].gameObject.SetActive(false);
                    buttonTeam1Cards[2].gameObject.SetActive(false);
                    buttonTeam2Cards[2].gameObject.SetActive(false);
                    nb = 2;

                    testAndDisplayCharRoster();

                    v2Pressed = false;
                }
                if (v1Pressed)
                {
                    for (int i = 4; i > 0; i--)
                    {
                        charsTeam1Display.transform.GetChild(0).transform.GetChild(i).GetComponent<RawImage>().texture = charCards[8];
                        charsTeam2Display.transform.GetChild(0).transform.GetChild(i).GetComponent<RawImage>().texture = charCards[8];

                        if (charsTeam1.Count > i)
                        {
                            buttonTeam1Enable[(int)charsTeam1[i]] = true;
                            charsTeam1.RemoveAt(i);
                        }
                        if (charsTeam2.Count > i)
                        {
                            buttonTeam2Enable[(int)charsTeam2[i]] = true;
                            charsTeam2.RemoveAt(i);
                        }

                    }

                    // Deactivate character slot 1, 2, 3 and 4
                    buttonTeam1Cards[4].gameObject.SetActive(false);
                    buttonTeam2Cards[4].gameObject.SetActive(false);
                    buttonTeam1Cards[3].gameObject.SetActive(false);
                    buttonTeam2Cards[3].gameObject.SetActive(false);
                    buttonTeam1Cards[2].gameObject.SetActive(false);
                    buttonTeam2Cards[2].gameObject.SetActive(false);
                    buttonTeam1Cards[1].gameObject.SetActive(false);
                    buttonTeam2Cards[1].gameObject.SetActive(false);
                    nb = 1;

                    testAndDisplayCharRoster();

                    v1Pressed = false;
                }
                // CHARACTER SELECTION
                //Edited By Rayane TALEB L3C2
                for (int i = 0; i < 8; i++)
                {
                    //Edited By Rayane TALEB L3C2
                    if (buttonRandomTeam1Pressed)
                    {
                        int j = (int)UnityEngine.Random.Range(0, 8);
                        if (charsTeam.Count < nb)
                        {
                            
                            
                            charsTeam.Add((CharClass)j);
                            
                            
                            if (currentTeam == 0)
                            {
                                buttonTeam1Enable[j] = false;
                            }
                            else
                            {
                                buttonTeam2Enable[j] = false;
                            }
                            charsTeamDisplay.transform.GetChild(0).transform.GetChild(charsTeam.Count - 1).GetComponent<RawImage>().texture = charCards[j];

                            testAndDisplayCharRoster();                            
                        }
                        buttonRandomTeam1Pressed = false;
                        buttonCharCardsPressed[j] = false;
                    }
                    //Edited By Rayane TALEB L3C2
                    if (buttonRandomTeam2Pressed)
                    {
                        int j = (int)UnityEngine.Random.Range(0, 8);
                        if (charsTeam.Count < nb)
                        {

                            
                            charsTeam.Add((CharClass)j);

                            
                            if (currentTeam == 0)
                            {
                                buttonTeam1Enable[j] = false;
                            }
                            else
                            {
                                buttonTeam2Enable[j] = false;
                            }
                            charsTeamDisplay.transform.GetChild(0).transform.GetChild(charsTeam.Count - 1).GetComponent<RawImage>().texture = charCards[j];

                            testAndDisplayCharRoster();
                        }
                        buttonRandomTeam2Pressed = false;
                        buttonCharCardsPressed[j] = false;
                    }
                    if (buttonCharCardsPressed[i])
                    {
                        if (charsTeam.Count < nb)
                        {
                            charsTeam.Add((CharClass)i);
                            
                            if (currentTeam == 0)
                            {
                                buttonTeam1Enable[i] = false;
                            }
                            else
                            {
                                buttonTeam2Enable[i] = false;
                            }
                            charsTeamDisplay.transform.GetChild(0).transform.GetChild(charsTeam.Count - 1).GetComponent<RawImage>().texture = charCards[i];

                            testAndDisplayCharRoster();
                        }

                        buttonCharCardsPressed[i] = false;
                    }
                }

                // REMOVE CHARACTER FROM TEAM
                for (int i = 0; i < 5; i++)
                {
                    if (buttonTeamCardsPressed[i])
                    {
                        if (charsTeam.Count > i)
                        {
                            int numCard = (int)charsTeam[i];
                            if (currentTeam == 0)
                            {
                                buttonTeam1Enable[numCard] = true;
                            }
                            else
                            {
                                buttonTeam2Enable[numCard] = true;
                            }
                            charsTeam.RemoveAt(i);
                            for (int j = i; j < charsTeam.Count; j++)
                            {
                                charsTeamDisplay.transform.GetChild(0).transform.GetChild(j).GetComponent<RawImage>().texture = charCards[(int)charsTeam[j]];
                            }
                            charsTeamDisplay.transform.GetChild(0).transform.GetChild(charsTeam.Count).GetComponent<RawImage>().texture = charCards[8];

                            testAndDisplayCharRoster();
                        }
                        buttonTeamCardsPressed[i] = false;
                    }
                }

                //Verifie si le joueur 2 n'ait pas autant de personnages que le joueur 1
                if (currentTeam == 1 && nb != charsTeam1.Count)
                {
                    charSelectMenuPreviousPlayer();
                }

                // READY
                if (charsTeam.Count == nb)
                {
                    if (currentTeam == 0) charsTeam1Display.transform.GetChild(2).gameObject.SetActive(true);
                    else charsTeam2Display.transform.GetChild(2).gameObject.SetActive(true);
                }
                else
                {
                    if (currentTeam == 0) charsTeam1Display.transform.GetChild(2).gameObject.SetActive(false);
                    else charsTeam2Display.transform.GetChild(2).gameObject.SetActive(false);
                }
                if (buttonReadyTeam1Pressed)
                {
                    buttonRandom1.gameObject.SetActive(false);
                    buttonRandom2.gameObject.SetActive(true);
                    charSelectMenuNextPlayer();
                    buttonReadyTeam1Pressed = false;
                }
                else if (buttonReadyTeam2Pressed)
                {
                    Debug.Log("Start : P1 " + (PlayerType)dropdownPlayer1Type.value + " P2 " + (PlayerType)dropdownPlayer2Type.value);
                    // Give Main all the info

                    MainGame.startGameData = new StartGameData();
                    MainGame.startGameData.loadSave = false;
                    MainGame.startGameData.charsTeam1 = charsTeam1;
                    MainGame.startGameData.charsTeam2 = charsTeam2;
                    MainGame.startGameData.player1Type = (PlayerType)dropdownPlayer1Type.value;
                    MainGame.startGameData.player2Type = (PlayerType)dropdownPlayer2Type.value;
                    MainGame.startGameData.mapChosen = dropdownMap.value;
                    MainGame.startGameData.nbGames = nbGames;
                    MainGame.startGameData.slider = slider.value;
                    buttonReadyTeam2Pressed = false;


                    // Mode Console deactivated
                    if (MainGame.startGameData.player1Type != PlayerType.HUMAN && MainGame.startGameData.player2Type != PlayerType.HUMAN &&
                        MainGame.startGameData.player1Type != PlayerType.AI_CPU_Offense && MainGame.startGameData.player2Type != PlayerType.AI_CPU_Offense && consoleMode &&
                        MainGame.startGameData.player1Type != PlayerType.AI_CPU_Defense && MainGame.startGameData.player2Type != PlayerType.AI_CPU_Defense && consoleMode)
                    {
                        // Load Console Mode scene
                        SceneManager.LoadScene(3);
                    }
                    else
                    {
                        SceneManager.LoadScene(1);
                    }
                }

                if (buttonToAdvancedOptionsPressed)
                {
                    advancedOptionsMenu.SetActive(true);
                    CharSelectMenu.SetActive(false);
                    buttonToAdvancedOptionsPressed = false;
                }
            }
        }
        else if (advancedOptionsMenu.activeInHierarchy)
        {
            if (buttonToCharSelectPressed)
            {
                advancedOptionsMenu.SetActive(false);
                CharSelectMenu.SetActive(true);
                buttonToCharSelectPressed = false;
                saveOptions();
            }
            consoleMode = toggleConsoleMode.isOn;
            nbGames = (int)(sliderNbGames.value * sliderNbGames.value);
            textNbGames.GetComponent<Text>().text = "(IA vs IA) nombre de parties : " + nbGames;
        }
    }

    void testAndDisplayCharRoster()
    {
        Boolean displayRoster = false;

        if (currentTeam == 0 && charsTeam1.Count < nb)
            displayRoster = true;
        else if (currentTeam == 1 && charsTeam2.Count < nb)
            displayRoster = true;

        for (int i = 0; i < 8; i++)
            buttonCharCards[i].gameObject.SetActive(displayRoster);
    }

    //Edited By Rayane TALEB L3C2
    void initCharSelectMenu()
    {
        charsTeam1 = new List<CharClass>();
        charsTeam2 = new List<CharClass>();
        buttonRandom1.gameObject.SetActive(true);
        for (int i = 0; i < 5; i++) charsTeam1Display.transform.GetChild(0).transform.GetChild(i).GetComponent<RawImage>().texture = charCards[8];
        for (int i = 0; i < 5; i++) charsTeam2Display.transform.GetChild(0).transform.GetChild(i).GetComponent<RawImage>().texture = charCards[8];

        for (int i = 0; i < 5; i++) charsTeam1Display.transform.GetChild(0).transform.GetChild(i).GetComponent<Button>().enabled = true;
        for (int i = 0; i < 5; i++) charsTeam2Display.transform.GetChild(0).transform.GetChild(i).GetComponent<Button>().enabled = false;

        charsTeam1Display.transform.GetChild(1).gameObject.SetActive(true);
        charsTeam1Display.transform.GetChild(2).gameObject.SetActive(false);

        charsTeam2Display.transform.GetChild(1).gameObject.SetActive(false);
        charsTeam2Display.transform.GetChild(2).gameObject.SetActive(false);
    }

    //Edited By Rayane TALEB L3C2
    void charSelectMenuNextPlayer()
    {
        currentTeam = 1;
        buttonRandom1.gameObject.SetActive(false);
        buttonRandom2.gameObject.SetActive(true);
        for (int i = 0; i < 5; i++) charsTeam1Display.transform.GetChild(0).transform.GetChild(i).GetComponent<Button>().enabled = false;
        for (int i = 0; i < 5; i++) charsTeam2Display.transform.GetChild(0).transform.GetChild(i).GetComponent<Button>().enabled = true;

        charsTeam1Display.transform.GetChild(1).gameObject.SetActive(false);
        charsTeam1Display.transform.GetChild(2).gameObject.SetActive(false);
        charsTeam2Display.transform.GetChild(1).gameObject.SetActive(true);
        charsTeam2Display.transform.GetChild(2).gameObject.SetActive(false);

        testAndDisplayCharRoster();

        teamSelectHighlight.transform.localPosition = new Vector3(0, 36 - 183, 0);
    }

    //Edited By Rayane TALEB L3C2
    void charSelectMenuPreviousPlayer()
    {
        currentTeam = 0;
        for (int i = 0; i < 5; i++) charsTeam1Display.transform.GetChild(0).transform.GetChild(i).GetComponent<Button>().enabled = true;
        for (int i = 0; i < 5; i++) charsTeam2Display.transform.GetChild(0).transform.GetChild(i).GetComponent<Button>().enabled = false;
        
        buttonRandom1.gameObject.SetActive(true);
        buttonRandom2.gameObject.SetActive(false);

        charsTeam1Display.transform.GetChild(1).gameObject.SetActive(true);
        charsTeam1Display.transform.GetChild(2).gameObject.SetActive(true);
        charsTeam2Display.transform.GetChild(1).gameObject.SetActive(false);
        charsTeam2Display.transform.GetChild(2).gameObject.SetActive(false);

        testAndDisplayCharRoster();

        teamSelectHighlight.transform.localPosition = new Vector3(0, 36, 0);
    }


    void saveOptions()
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(Application.streamingAssetsPath + "/Data/Options/options", FileMode.Create)))
        {
            writer.Write((int)((consoleMode) ? 1 : 0));
            writer.Write(nbGames);
        }
    }

    void loadOptions()
    {
        using (BinaryReader reader = new BinaryReader(File.Open(Application.streamingAssetsPath + "/Data/Options/options", FileMode.Open)))
        {
            consoleMode = (reader.ReadInt32() == 0) ? false : true;
            nbGames = reader.ReadInt32();
        }
    }

    public void quitGame()
    {
        Application.Quit();
        Debug.Log("You Quit The Game");
    }

    // Events
    // edited by Mengqian Xu L3C2
    void button5v5Pressed_() { v5Pressed = true; }
    void button4v4Pressed_() { v4Pressed = true; }
    void button3v3Pressed_() { v3Pressed = true; }
    void button2v2Pressed_() { v2Pressed = true; }
    void button1v1Pressed_() { v1Pressed = true; }
    void buttonPlayPressed_() { buttonPlayPressed = true; }
    void buttonLoadPressed_() { buttonLoadPressed = true; }
    void buttonQuitPressed_() { buttonQuitPressed = true; }
    void buttonStatsPressed_() { buttonStatsPressed = true; }
    void buttonCreditsPressed_() { buttonCreditsPressed = true; }
    void buttonGuidePressed_() { buttonGuidePressed = true; }
    void buttonCharCardsPressed_(int i) { buttonCharCardsPressed[i] = true; }
    void buttonTeam1CardsPressed_(int i) { buttonTeam1CardsPressed[i] = true; }
    void buttonTeam2CardsPressed_(int i) { buttonTeam2CardsPressed[i] = true; }
    void buttonBackTeam1Pressed_() { buttonBackTeam1Pressed = true; }
    void buttonBackTeam2Pressed_() { buttonBackTeam2Pressed = true; }
    void buttonReadyTeam1Pressed_() { buttonReadyTeam1Pressed = true; }
    void buttonReadyTeam2Pressed_() { buttonReadyTeam2Pressed = true; }
    void buttonRandomTeam1Pressed_() { buttonRandomTeam1Pressed = true; }
    void buttonRandomTeam2Pressed_() { buttonRandomTeam2Pressed = true; }
    void buttonToAdvancedOptionsPressed_() { buttonToAdvancedOptionsPressed = true; }
    void buttonToCharSelectPressed_() { buttonToCharSelectPressed = true; }
    void buttonAidePressed_() { buttonAidePressed = true; } 
}
