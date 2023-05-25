using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI coinCount;

    [SerializeField]
    private TextMeshProUGUI gameStateText;

    public void Setup(GameState gameState)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameObject.SetActive(true);
        coinCount.text = GameplayManager.instance.CoinCount.ToString();

        switch (gameState)
        {
            case GameState.GameOver:
                gameStateText.text = "GAME OVER";
                break;
            case GameState.Win:
                gameStateText.text = "YOU WIN!";
                break;
        }
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }
}
