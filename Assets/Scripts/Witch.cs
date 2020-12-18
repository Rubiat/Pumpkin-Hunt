using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : MonoBehaviour
{

    int hp = 3;

    Rigidbody2D witchRigidBody;

    [SerializeField]
    int forceSpeed;

    // Start is called before the first frame update
    void Start()
    {
        witchRigidBody = GetComponent<Rigidbody2D>();
        witchRigidBody.AddForce(Vector2.right * forceSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -6 || transform.position.x >= 10.5)
        {
            Destroy(gameObject);
        }
    }

    public void decreaseHP()
    {
        hp--;
    }

    public int getHP()
    {
        return hp;
    }
}
