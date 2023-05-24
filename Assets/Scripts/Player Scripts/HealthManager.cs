using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] robotParts;

    [SerializeField]
    private GameObject robot;

    [SerializeField]
    private int maxHealth = 3;
    private int health;

    [SerializeField]
    private GameObject deathFx;

    [SerializeField]
    private float invisibleDuration = 2f;
    private float invisibleTimer;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        health = maxHealth;
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        MakeRobotInvisible();
    }

    private void MakeRobotInvisible()
    {
        if (invisibleTimer > 0f)
        {
            invisibleTimer -= Time.deltaTime;
            bool isInvisible = Mathf.Floor(invisibleTimer * 5) % 2 == 0 || invisibleTimer <= 0f;

            for (int i = 0; i < robotParts.Length; i++)
            {
                robotParts[i].SetActive(isInvisible);
            }
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        if (invisibleTimer > 0f)
        {
            return;
        }

        health -= damageAmount;

        if (health <= 0)
        {
            GameplayManager.instance.GameOver();
            PlayerDied();
        }
        else
        {
            AudioManager.instance.PlaySound(SFX.TakeDamage);

            invisibleTimer = invisibleDuration;
            playerMovement.KnockBack();

            GameplayManager.instance.SetHealthCount(-1);
        }
    }

    public void IncreaseHealth()
    {
        if (health < maxHealth)
        {
            health += 1;
            GameplayManager.instance.SetHealthCount(1);

        }

        AudioManager.instance.PlaySound(SFX.HeartPickup);
    }

    private void PlayerDied()
    {
        robot.SetActive(false);
        AudioManager.instance.PlaySound(SFX.Death);
        Instantiate(deathFx, transform.position, Quaternion.identity);

        for (int i = 0; i < 3; i++)
        {
            GameplayManager.instance.SetHealthCount(-1);
        }
    }
}
