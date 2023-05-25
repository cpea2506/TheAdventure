using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField]
    private bool isDeathZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
            other.GetComponent<HealthManager>().DamagePlayer(isDeathZone ? 3 : 1);
        }
    }
}
