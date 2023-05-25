using UnityEngine;
using System.Collections;
using TMPro;

public class Pipe : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI instructionText;


    private BoxCollider pipeCollider;
    private bool StandOn;

    private void OnTriggerEnter(Collider col)
    {
        StandOn = true;

        instructionText.enabled = true;
    }

    private void OnTriggerExit(Collider col)
    {
        StandOn = false;
        instructionText.enabled = false;
    }

    // Use this for initialization
    private void Start()
    {
        BoxCollider[] pipeColliders = GetComponentsInChildren<BoxCollider>();
        pipeCollider = pipeColliders[1];
        instructionText.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && StandOn)
        {
            AudioManager.instance.PlaySound(SFX.Pipe);
            StartCoroutine(WaitingForPipe());
        }
    }

    IEnumerator WaitingForPipe()
    {
        pipeCollider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        pipeCollider.enabled = true;
        GameplayManager.instance.GameWin();
    }
}
