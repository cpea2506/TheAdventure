using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField]
    private bool isDeathZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
            if (isDeathZone)
            {

            }
            else
            {
                Debug.Log("Hahah, stupid!");
            }
        }
    }
}
