using UnityEngine;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;

    [HideInInspector]
    public bool playerDied;

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
        coinText = GameObject.FindWithTag(TagManager.COIN_TEXT_TAG).GetComponent<TextMeshProUGUI>();
        healthCount = initialHealth;
    }

    private void Update()
    {
        if (playerDied)
        {
            gameplayCanvas.enabled = false;
            gameOverScreen.Setup();
        }
    }

    public void GameOver()
    {
        playerDied = true;
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
