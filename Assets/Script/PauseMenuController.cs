using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameController gm;
    public Button resumeBtn;
    public Button quitBtn;
    
    void Awake()
    {
        resumeBtn.onClick.AddListener(Resume);
        quitBtn.onClick.AddListener(SelectQuit);
            
    }
    void Resume()
    {
        gm.Resume();
    }
    void SelectQuit()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }
    

    
}
