using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    Rigidbody2D pumpkinRigidBody;

    [SerializeField]
    Vector2 lastVelocty;
    [SerializeField]
    GameObject brokenPumpkinPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 dir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(0.5f, 1.0f)).normalized;
        pumpkinRigidBody = GetComponent<Rigidbody2D>();
        pumpkinRigidBody.AddForce(dir * PumpkinGenerator.pumpkinSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocty = pumpkinRigidBody.velocity;

        if (transform.position.y <= -6)
        {
            Destroy(gameObject);
            PumpkinGenerator.pumpkinsRemaining--;
            PumpkinGenerator.remainingMisses--;
            GameObject.Find("LivesText").GetComponent<TextMeshProUGUI>().text = "Lives: " + PumpkinGenerator.remainingMisses;
            if (PumpkinGenerator.remainingMisses <= 0)
            {
                PumpkinGenerator.gameOver = true;
                PumpkinGenerator.playAnimation = true; // This bool trigers the end of game "Game Over" text animation
                GameObject[] remainingPumpkins = GameObject.FindGameObjectsWithTag("Pumpkin");
                for (int i = 0; i < remainingPumpkins.Length; i++)
                {
                    Destroy(remainingPumpkins[i]);
                    Instantiate(brokenPumpkinPrefab, remainingPumpkins[i].transform.position, remainingPumpkins[i].transform.rotation);
                }

                GameObject remainingWitch = GameObject.FindGameObjectWithTag("Witch");
                if (remainingWitch != null)
                {
                    remainingWitch.GetComponent<Rigidbody2D>().gravityScale = 1;
                    remainingWitch.GetComponent<Rigidbody2D>().angularDrag = 0.05f;
                    remainingWitch.GetComponent<Rigidbody2D>().AddTorque(1440, ForceMode2D.Impulse);
                }
            }

            if (PumpkinGenerator.pumpkinsRemaining == 0)
            {
                PumpkinGenerator.pumpkinsRemaining = 10;
                PumpkinGenerator.pumpkinsCreated = 0;
                PumpkinGenerator.intermission = true;
                PumpkinGenerator.newLevel = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var speed = lastVelocty.magnitude;
        var direction = Vector2.Reflect(lastVelocty.normalized, collision.contacts[0].normal);
        pumpkinRigidBody.velocity = direction * Mathf.Max(speed, 0);
    }
}
