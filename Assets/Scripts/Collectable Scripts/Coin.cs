using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private GameObject pickUpFx;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
            AudioManager.instance.PlaySound(SFX.CoinPickUp);
            Instantiate(pickUpFx, transform.position, Quaternion.identity);

            GameplayManager.instance.SetCoinCount(1);
        }

        Destroy(gameObject);
    }
}
