using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum Firemode {normal, fire, ice}
[System.Serializable]

public class Enemy 
{
    public EnemyController enemyController;
    public EnemyObject stats;
}
[System.Serializable]
public class Building
{
    public TowerController towerController;
    public TowerObject stats;
}


public class GameController : MonoBehaviour
{   
    public Enemy[] enemys;
    public Building[] buildings;
    private Grid gridSystem;
    public int money = 10;
    public Vector2 startPos;
    public int health;
    public int maxHealth = 5;
    
    public int selectedTower = -1;
    
    public Image healthUI;
    public TextMeshProUGUI moneyUI; 
    public TextMeshProUGUI WaveUI; 
    public SpriteRenderer placementIndicatorUI;
    public Image towerSelectorUI;

    public Color placeableIndicatorColor;
    public Color notPlaceableIndicatorColor;

    public GameObject pauseMenu;
    public GameObject GameOverUI;

    public GameObject BuyPhaseUI;

    public GameObject WinUI;
    public int wave;
    public int curWave = 0;
    
    public static int EnemyAlive = 0;
    private bool isPause = false;
    
    public bool buyPhase = false;

    public bool win = false;
    public bool lose = false;

    void Start()
    {
        WaveUI.text = "Wave Left : " + (wave - curWave).ToString();
        buyPhase = true;
        Time.timeScale = 1;
        placementIndicatorUI.gameObject.SetActive(false);
        towerSelectorUI.gameObject.SetActive(false);
        moneyUI.text = money.ToString();
        health = maxHealth;
        gridSystem = FindObjectOfType<Grid>();
        startPos = new Vector2(gridSystem.start.worldPos.x, gridSystem.start.worldPos.y);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            if (isPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Node currentNode = gridSystem.WorldPointToNode(mousePos);

            
            if (currentNode.isPlaceable)
            {   
                
                if (selectedTower != -1 && money >= buildings[selectedTower].stats.price)
                {
                    TowerController tc = Instantiate(buildings[selectedTower].towerController, currentNode.worldPos, Quaternion.identity);
                    SpriteRenderer renderer = tc.GetComponent<SpriteRenderer>();
                    currentNode.isPlaceable = false;
                    tc.SetStats(buildings[selectedTower].stats);
                    money -= buildings[selectedTower].stats.price;

                }
                else
                {
                    Debug.Log("Cannot build The tower");
                }
            }
        }
        
        
        UpdateUI();

        if (EnemyAlive <= 0)
        {
            if (health <= 0)
            {
                GameOver();
                return;
            }
            if (health > 0 && curWave >= wave )
            {
                win = true;
            }
            else
            {
                buyPhase = true;
            }
            
        }

        if (curWave < wave && buyPhase && Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.timeScale == 0)
            {
                return;
            }
            StartCoroutine(SpawnEnemy(5));
            EnemyAlive = 5;
            buyPhase = !buyPhase;
            curWave++;
        }

        

        
    }
    public void Pause()
    {
        Time.timeScale = 0;
        isPause = true;
        pauseMenu.SetActive(true);

    }
    public void Resume()
    {
        Time.timeScale = 1;
        isPause = false;
        pauseMenu.SetActive(false);
    }
    void GameOver()
    {
        Time.timeScale = 0;
        lose = true;
        GameOverUI.SetActive(true);
    }
    void UpdateUI()
    {
        if (lose)
        {
            return;
        }
        if (win)
        {
            WinUI.SetActive(true);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Tab) && !isPause)
        {
            if (towerSelectorUI.gameObject.activeSelf)
            {   
                
                placementIndicatorUI.gameObject.SetActive(false);
                towerSelectorUI.gameObject.SetActive(false);
                
            }
            else
            {
                selectedTower = -1;
                placementIndicatorUI.gameObject.SetActive(false);
                towerSelectorUI.gameObject.SetActive(true);
            }
        }
        if(buyPhase)
        {
            BuyPhaseUI.SetActive(true);
        }
        else
        {
            BuyPhaseUI.SetActive(false);
        }

        
        if (placementIndicatorUI.gameObject.activeSelf)
        {
            Transform T = placementIndicatorUI.transform;
            Node curNode = gridSystem.WorldPointToNode(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            T.position = curNode.worldPos;

            if (curNode.isPlaceable && buildings[selectedTower].stats.price <= money)
            {
                placementIndicatorUI.color = placeableIndicatorColor;
            }
            else
            {
                placementIndicatorUI.color = notPlaceableIndicatorColor;
            }
        }
        moneyUI.text = money.ToString();
        healthUI.transform.localScale = new Vector3 ((float)health / maxHealth, 1 ,1);

        WaveUI.text = "Wave Left : " + (wave - curWave).ToString();
    }
    IEnumerator SpawnEnemy(int n)
    {
        for (int i = 0; i < n; i++)
        {
            int r = (int)Random.Range(0, enemys.Length);
            EnemyController enemy = Instantiate(enemys[r].enemyController, startPos, Quaternion.identity);
            enemy.SetStats(enemys[r].stats);
            yield return new WaitForSeconds(2f);
        }
        
    }
    public void SelectFirst()
    {
        selectedTower = 0;
        placementIndicatorUI.gameObject.SetActive(true);
        towerSelectorUI.gameObject.SetActive(false);
    }

    public void SelectSecond()
    {
        selectedTower = 1;
        placementIndicatorUI.gameObject.SetActive(true);
        towerSelectorUI.gameObject.SetActive(false);
    }

    public void SelectThird()
    {
        selectedTower = 2;
        placementIndicatorUI.gameObject.SetActive(true);
        towerSelectorUI.gameObject.SetActive(false);    
    }



}