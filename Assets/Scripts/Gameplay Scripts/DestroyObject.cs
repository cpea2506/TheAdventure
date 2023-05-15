using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField]
    private float timer = 2f;

    [SerializeField]
    private bool shouldDestroy;

    private void Start()
    {
        Invoke("DeactivateObject", timer);
    }

    private void DeactivateObject()
    {
        if (shouldDestroy)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
