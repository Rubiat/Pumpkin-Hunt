using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    Vector3 mousePos;
    public float mouseSensitivity;

    AudioSource gunSound;

    [SerializeField]
    GameObject brokenPumpkinPrefab;

    public static int points = 0;
    [SerializeField]
    float shootingSpeed; // By default it's set at 0.3 sec/shot
    float shootingTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = Vector2.Lerp(transform.position, mousePos, mouseSensitivity);

        gunSound = this.transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = Vector2.Lerp(transform.position, mousePos, mouseSensitivity);

        if (Input.GetMouseButton(0))
        {
            if (PumpkinGenerator.specialModeOn)
            {
                shootingTimer += Time.deltaTime;
                if (shootingTimer > shootingSpeed || Input.GetMouseButtonDown(0)) // Timer is > 0.3, OR this was the first click
                {
                    shootingTimer = 0;
                    if (!(PumpkinGenerator.gameOver))
                    {
                        gunSound.Play();
                    }

                    Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                    RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);
                    if (hits.Length != 0)
                    {
                        int pumpkinCount = 0;
                        for (int i = 0; i < hits.Length; i++)
                        {
                            if (hits[i].collider.gameObject.tag == "Pumpkin")
                            {
                                pumpkinCount++;
                                Destroy(hits[i].collider.gameObject);
                                PumpkinGenerator.pumpkinsRemaining--;
                                points++; // In special mode only add 1 point for each pumpkin (even if they overlap)

                                Instantiate(brokenPumpkinPrefab, transform.position, transform.rotation);
                                if (PumpkinGenerator.pumpkinsRemaining == 0)
                                {
                                    PumpkinGenerator.pumpkinsRemaining = 10;
                                    PumpkinGenerator.pumpkinsCreated = 0;
                                    PumpkinGenerator.intermission = true;
                                    PumpkinGenerator.newLevel = true;
                                }
                            }
                            else if (hits[i].collider.gameObject.tag == "Witch")
                            {
                                hits[i].collider.gameObject.GetComponent<Witch>().decreaseHP();
                                if (hits[i].collider.gameObject.GetComponent<Witch>().getHP() == 0)
                                {
                                    points += 5;
                                    hits[i].collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                                    hits[i].collider.gameObject.GetComponent<Rigidbody2D>().angularDrag = 0.05f;
                                    hits[i].collider.gameObject.GetComponent<Rigidbody2D>().AddTorque(1440, ForceMode2D.Impulse);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (points > 3)
                        {
                            points -= 3;
                        }
                        else if (points <= 3)
                        {
                            points = 0;
                        }

                        if (!(PumpkinGenerator.gameOver))
                        {
                            GameObject scarecrowHolder = GameObject.Find("ScarecrowHolder");
                            GameObject scarecrow = GameObject.Find("Scarecrow");
                            Vector3 pos = scarecrowHolder.transform.position;
                            pos.x = mousePos2D.x;
                            scarecrowHolder.transform.position = pos;
                            scarecrow.GetComponent<Animation>().Play();
                        }
                    }
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                if (!(PumpkinGenerator.gameOver))
                {
                    gunSound.Play();
                }

                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);
                if (hits.Length != 0)
                {
                    int pumpkinCount = 0;
                    for (int i = 0; i < hits.Length; i++)
                    {
                        if (hits[i].collider.gameObject.tag == "Pumpkin")
                        {
                            pumpkinCount++;
                            Destroy(hits[i].collider.gameObject);
                            PumpkinGenerator.pumpkinsRemaining--;

                            Instantiate(brokenPumpkinPrefab, transform.position, transform.rotation);
                            if (PumpkinGenerator.pumpkinsRemaining == 0)
                            {
                                PumpkinGenerator.pumpkinsRemaining = 10;
                                PumpkinGenerator.pumpkinsCreated = 0;
                                PumpkinGenerator.intermission = true;
                                PumpkinGenerator.newLevel = true;
                            }
                        }
                        else if (hits[i].collider.gameObject.tag == "Witch")
                        {
                            hits[i].collider.gameObject.GetComponent<Witch>().decreaseHP();
                            if (hits[i].collider.gameObject.GetComponent<Witch>().getHP() == 0)
                            {
                                points += 5;
                                hits[i].collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                                hits[i].collider.gameObject.GetComponent<Rigidbody2D>().angularDrag = 0.05f;
                                hits[i].collider.gameObject.GetComponent<Rigidbody2D>().AddTorque(1440, ForceMode2D.Impulse);
                            }
                        }
                    }

                    if (pumpkinCount > 1)
                    {
                        for (int i = 0; i < pumpkinCount; i++)
                        {
                            points += 6; // 6 points each instead of 3 for bonus point?
                        }
                    }
                    else if (pumpkinCount == 1)
                    {
                        points += 3;
                    }
                }
                else
                {
                    if (points > 0)
                    {
                        points--;
                    }

                    if (!(PumpkinGenerator.gameOver))
                    {
                        GameObject scarecrowHolder = GameObject.Find("ScarecrowHolder");
                        GameObject scarecrow = GameObject.Find("Scarecrow");
                        Vector3 pos = scarecrowHolder.transform.position;
                        pos.x = mousePos2D.x;
                        scarecrowHolder.transform.position = pos;
                        scarecrow.GetComponent<Animation>().Play();
                    }
                }
            }
        }

        if (!(PumpkinGenerator.gameOver))
        {
            GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = "Score: " + points;
        }
    }
}
