using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private Vector3 starPos, endPos;

    [SerializeField]
    private float timeToReach = 5f;

    private float timer;

    private void Start()
    {
        transform.position = starPos;
    }

    private void FixedUpdate()
    {
        if (GameplayManager.instance.playerDied) {
            return;
        }

        timer += Time.deltaTime / timeToReach;
        transform.position = Vector3.Lerp(starPos, endPos, timer);

        if (transform.position == endPos)
        {
            SwitchPosition();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
            other.transform.SetParent(transform);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
            other.transform.SetParent(null);
        }
    }

    private void SwitchPosition()
    {
        var temp = starPos;
        starPos = endPos;
        endPos = temp;
        timer = 0f;
    }
}
