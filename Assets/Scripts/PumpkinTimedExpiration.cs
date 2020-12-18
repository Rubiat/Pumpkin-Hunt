using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinTimedExpiration : MonoBehaviour
{
    [SerializeField]
    float expirationTime;
    float timer;

    private void Start()
    {
        transform.GetComponent<Rigidbody2D>().AddForce(Vector2.one * 10);
    }
    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;
        //if (timer >= expirationTime)
        //{
        //    Destroy(gameObject);
        //}

        if (transform.position.y <= -6)
        {
            Destroy(gameObject);
        }
    }
}
