using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private GameObject pickUpFx;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
            Instantiate(pickUpFx, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
