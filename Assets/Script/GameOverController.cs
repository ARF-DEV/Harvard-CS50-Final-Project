using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameController gm;
    public Button restartBtn;
    public Button quitBtn;
    
    void Awake()
    {
        if (restartBtn != null)
        {
            restartBtn.onClick.AddListener(Restart);
        }
        
        quitBtn.onClick.AddListener(SelectQuit);
            
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void SelectQuit()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }
    

    
}
