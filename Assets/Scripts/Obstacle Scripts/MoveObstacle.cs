using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    [SerializeField]
    private Transform endPos;

    private Vector3 startPos, targetPos;

    [SerializeField]
    private float minMoveSpeed = 2f, maxMoveSpeed = 6f;

    private float moveSpeed = 5f;

    [SerializeField]
    private float stoppingDistance = .1f;

    private bool atTheEnd;

    private void Start()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);

        startPos = transform.localPosition;
        targetPos = endPos.localPosition;

        atTheEnd = true;
    }

    private void Update()
    {
        MoveSaw();
    }

    private void MoveSaw()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.localPosition, targetPos) < stoppingDistance)
        {
            if (atTheEnd)
            {
                targetPos = startPos;
                atTheEnd = false;
            }
            else
            {
                targetPos = endPos.localPosition;
                atTheEnd = true;

            }

        }
    }
}
