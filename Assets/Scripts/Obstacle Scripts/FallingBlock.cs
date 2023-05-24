using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    private enum State
    {
        Fall,
        Reset,
        Idle,
    }

    private State state = State.Idle;
    private float fallTime;

    [SerializeField]
    private float fallingDistance = 3.5f;

    [SerializeField]
    private float fallPower = 6f;

    [SerializeField]
    private float idleDuration = 1f;

    [SerializeField]
    private float minIdleDuration = 1f, maxIdleDuration = 2f;

    [SerializeField]
    private float resetDuration = 2f;

    private Vector3 startPos, endPos;

    private void Start()
    {
        startPos = transform.position;
        endPos = startPos - new Vector3(0f, fallingDistance);
    }

    private void Update()
    {
        switch (state)
        {
            case State.Fall:
                fallTime += Time.deltaTime;
                transform.position = Vector3.Lerp(startPos, endPos, Mathf.Pow(fallTime, fallPower));

                if (transform.position == endPos)
                {
                    AudioManager.instance.PlaySound(SFX.CrateCollide, 0.1f);
                    state = State.Reset;
                    fallTime = 0f;
                }

                break;
            case State.Reset:
                fallTime += Time.deltaTime / resetDuration;
                transform.position = Vector3.Lerp(endPos, startPos, fallTime);

                if (transform.position == startPos)
                {
                    state = State.Idle;
                    fallTime = 0f;
                }
                break;
            case State.Idle:
                fallTime += Time.deltaTime;

                if (fallTime > idleDuration)
                {
                    idleDuration = Random.Range(minIdleDuration, maxIdleDuration);
                    state = State.Fall;
                    fallTime = 0f;
                }
                break;
        }
    }
}
