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

    public void GameOver()
    {
        playerDied = true;
    }

    private void RestartGame()
    {
        Invoke("NewGame", 2f);
    }

    private void NewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void SetCoinCount(int value)
    {
        coinCount += value;
        coinText.text = coinCount.ToString();
    }

    public int GetCoinCount() => coinCount;

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
}
