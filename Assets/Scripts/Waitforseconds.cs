using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waitforseconds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(setGameOverWithDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator setGameOverWithDelay()
    {
        Debug.Log("Before");
        yield return new WaitForSeconds(0.1f);
        PumpkinGenerator.gameOver = false;
        Debug.Log("After");
    }
}
