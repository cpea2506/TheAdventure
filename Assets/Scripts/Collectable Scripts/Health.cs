using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private GameObject healthFx;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
            other.GetComponent<HealthManager>().IncreaseHealth();
            Instantiate(healthFx, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
