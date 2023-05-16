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
            PlayerDied();
        }
        else
        {
            invisibleTimer = invisibleDuration;
        }
    }

    public void IncreaseHealth()
    {
        if (health < maxHealth)
        {
            health += 1;
        }
    }

    private void PlayerDied()
    {
        robot.SetActive(false);

        Instantiate(deathFx, transform.position, Quaternion.identity);
    }
}
