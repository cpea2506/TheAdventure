using UnityEngine;
using TMPro;

public enum GameState
{
    InGame,
    Win,
    GameOver,
}

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;

    [HideInInspector]
    public GameState gameState;

    private int coinCount = 0;
    private TextMeshProUGUI coinText;

    [SerializeField]
    private GameOverScreen gameOverScreen;

    [SerializeField]
    private Canvas gameplayCanvas;

    [SerializeField]
    private GameObject[] healthUI;

    [SerializeField]
    private int initialHealth = 2;
    private int healthCount;

    private void Awake()
    {
        instance = this;
        gameState = GameState.InGame;
        coinText = GameObject.FindWithTag(TagManager.COIN_TEXT_TAG).GetComponent<TextMeshProUGUI>();
        healthCount = initialHealth;
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.GameOver:
                gameplayCanvas.enabled = false;
                gameOverScreen.Setup(gameState);
                break;
            case GameState.Win:
                gameplayCanvas.enabled = false;
                gameOverScreen.Setup(gameState);
                break;
        }
    }

    public void GameOver()
    {
        gameState = GameState.GameOver;
    }

    public void GameWin()
    {
        gameState = GameState.Win;
    }

    public void SetCoinCount(int value)
    {
        coinCount += value;
        coinText.text = coinCount.ToString();
    }

    public int CoinCount => coinCount;

    public void SetHealthCount(int value)
    {
        if (value < 0)
        {
            healthUI[healthCount].SetActive(false);
            healthCount += value;
        }
        else
        {
            healthCount += value;
            healthUI[healthCount].SetActive(true);
        }
    }

    public int HealthCount => healthCount;
}
