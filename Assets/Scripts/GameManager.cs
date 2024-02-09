using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private Transform _uiRoot;

    [SerializeField]
    private TextMeshProUGUI _coinsText;

    public static Action<int> OnAddScoreEvent;
    public static Action<bool> OnGameOverEvent;

    [SerializeField]
    public GameObject Panel;
    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; }

    private void Awake()
    {
        if(Instance != null)                                // if there is already an instance running then we destroy it
        {   
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);              // else we assign instance to the current gameObject           
            
        }
    }

    private void OnDestroy()                        // destroy the gameObject on game end
    {
        if(Instance == this)
        {
            Instance = null;
        }
        OnAddScoreEvent -= SetCoinDisplay;
    }

    private void Start()
    {
        NewGame();
        OnAddScoreEvent += SetCoinDisplay;
    }

    private void OnEnable()
    {
        //_coinsText.text = GameManager.Instance.GetCoins().ToString();
    }

    private void NewGame()                                      // starts a new game
    {
        lives = 3;
        coins = 0;
        LoadLevel(1,1);
        Panel.SetActive(false);
    }

    private void LoadLevel(int world, int stage)            // loads a new level
    {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene($"{world}-{stage}");         // the name of the level should be 1-1 1-2 2-1 2-2
    }   

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);                  // resets the level
    }

    public void ResetLevel()                                    // resets the level
    {
        --lives;

        if(lives > 0)
        {
            LoadLevel(world,stage);
        }
        else
        {
            GameOver();
        }
    }
    public void GameOver()                                 // triggers the game over screen
    {
        Panel.SetActive(true);
       // OnGameOverEvent -= ToggleScreen;
        //NewGame();                                          // starts a new game
        
    }

    public int AddCoin()                   // increments the coins for mario
    {
        coins++;
        return coins;
    }

    public int GetCoins()
    {
        return coins;
    }

    public void AddLife()           // adds a new life if we use 1Up mushroom
    {
        ++lives;
    }

    public void ToggleScreen(bool toggle)
    {
        _uiRoot.gameObject.SetActive(toggle);
    }

    public void RestartLevel()
    {
        NewGame();
    }

    private void SetCoinDisplay(int coins)
    {
        _coinsText.text = coins.ToString();
    }
}
