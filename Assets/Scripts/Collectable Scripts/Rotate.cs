using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private enum Direction
    {
        X, Y, Z
    }

    [SerializeField]
    private float minRotateSpeed = 100f, maxRotateSpeed = 200f;

    [SerializeField]
    private float rotateSpeed = 100f;

    [SerializeField]
    private bool setSpeed;

    [SerializeField]
    private Direction rotateBy;


    // Start is called before the first frame update
    void Awake()
    {
        if (setSpeed)
        {
            rotateSpeed = Random.Range(minRotateSpeed, maxRotateSpeed);
        }

    }

    // Update is called once per frame
    void Update()
    {
        switch (rotateBy)
        {
            case Direction.X:
                transform.Rotate(Vector3.right * Time.deltaTime * rotateSpeed);
                break;
            case Direction.Y:
                transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
                break;
            case Direction.Z:
                transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
                break;
        }

    }
}
