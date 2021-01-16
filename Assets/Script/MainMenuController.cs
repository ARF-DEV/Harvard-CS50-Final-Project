using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuController : MonoBehaviour
{
    public GameObject levelSelector;
    public GameObject MainMenu;

    public Button playBtn;
    public Button quitBtn;
    public Button MenuBtn;
    public Button Level1Btn;

    void Awake()
    {
        playBtn.onClick.AddListener(SelectPlay);
        quitBtn.onClick.AddListener(SelectQuit);
        Level1Btn.onClick.AddListener(Level1);
        MenuBtn.onClick.AddListener(Menu);    
    }
    void SelectPlay()
    {
        MainMenu.SetActive(false);
        
        levelSelector.SetActive(true);
    }
    void SelectQuit()
    {
        Application.Quit();
    }
    void Level1()
    {
        SceneManager.LoadScene("Scenes/Level1");
    }

    void Menu()
    {
        MainMenu.SetActive(true);
        
        levelSelector.SetActive(false);
    }
}