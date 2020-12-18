using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PumpkinGenerator : MonoBehaviour
{
    public GameObject pumpkin;
    public GameObject witch;
    public static int pumpkinSpeed = 150;
    float timer = 0;
    public static int pumpkinsCreated = 0;
    public static int pumpkinsRemaining = 10;
    public static int remainingMisses = 3;
    public static int level = 1;
    public static bool intermission = true;
    public static bool newLevel = true;
    public static bool gameOver = true;
    public static bool playAnimation = false;
    static int firstAppearance, secondAppearance; // For the two witch appearances
    Animation levelAnimation;
    TextMeshProUGUI specialModeText;
    public static bool specialModeOn = false;

    public static GameObject ButtonPlay, ButtonTryAgain, ButtonQuit;
    public static GameObject crossHair;
    public static GameObject GameOverText;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        levelAnimation = GameObject.Find("LevelText").GetComponent<Animation>();
        ButtonPlay = GameObject.Find("ButtonPlay");
        ButtonTryAgain = GameObject.Find("ButtonTryAgain");
        ButtonTryAgain.SetActive(false);
        ButtonQuit = GameObject.Find("ButtonQuit");
        crossHair = GameObject.Find("Crosshair");
        crossHair.GetComponent<Renderer>().enabled = false;
        specialModeText = GameObject.Find("SpecialModeText").GetComponent<TextMeshProUGUI>();
        GameOverText = GameObject.Find("GameOverText");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            if (playAnimation)
            {
                GameOverText.SetActive(true);
                GameObject.Find("GameOverText").GetComponent<TextMeshProUGUI>().text = "Game over \n Score: " + Crosshair.points;
                ButtonTryAgain.SetActive(true);
                ButtonQuit.SetActive(true);
                GameObject.Find("GameOverText").GetComponent<Animation>().Play();
                ButtonTryAgain.GetComponentInChildren<TextMeshProUGUI>().GetComponent<Animation>().Play();
                ButtonQuit.GetComponentInChildren<TextMeshProUGUI>().GetComponent<Animation>().Play();
                playAnimation = false;
                Cursor.visible = true;
                crossHair.GetComponent<Renderer>().enabled = false;
            }

            //Reset everything
            pumpkinsCreated = 0;
            pumpkinsRemaining = 10;
            remainingMisses = 3;
            level = 1;
            newLevel = true;
            Crosshair.points = 0;
            pumpkinSpeed = 150;
            intermission = true;
            specialModeText.alpha = 0;
        }
        else if (!intermission && pumpkinsCreated != 10)
        {

            timer += Time.deltaTime;

            if (timer >= 1.0f)
            {
                float x = Random.Range(-7.95f, 7.93f);
                Instantiate(pumpkin, new Vector3(x, -5, 0), Quaternion.identity);
                pumpkinsCreated++;
                timer = 0;

                if (pumpkinsCreated == firstAppearance || pumpkinsCreated == secondAppearance)
                {
                    Instantiate(witch, new Vector3(-10.0f, 2.30f, 0), Quaternion.identity);
                }
            }
            specialModeText.alpha = 255;

            if (Input.GetKeyDown(KeyCode.S))
            {
                specialModeOn = true;
                specialModeText.alpha = 0;
                for (int i = pumpkinsCreated; i < 10; i++)
                {
                    float x = Random.Range(-7.95f, 7.93f);
                    Instantiate(pumpkin, new Vector3(x, -5, 0), Quaternion.identity);
                }
                pumpkinsCreated = 10; // Set it to 10 since all the remaining pumpkins have been spawned
                remainingMisses += 2; // I add 2 extra lives if in special mode since all pumpkins appear at once
                GameObject.Find("LivesText").GetComponent<TextMeshProUGUI>().text = "Lives: " + PumpkinGenerator.remainingMisses;
            }
        }
        else if (newLevel)
        {
            GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Level " + level;
            GameObject.Find("LevelText").GetComponent<Animation>().Play();
            GameObject.Find("RoundText").GetComponent<TextMeshProUGUI>().text = "Round " + level++;
            newLevel = false;
            specialModeOn = false; // Turn it off in case the player turned it on in the previous level/round

            firstAppearance = Random.Range(1, 5); // Generate random spawning times for two witches at the beginning of each level
            secondAppearance = Random.Range(6, 10);
            pumpkinSpeed += 50;
            remainingMisses = 3; // Reset misses to 3
            GameObject.Find("LivesText").GetComponent<TextMeshProUGUI>().text = "Lives: " + PumpkinGenerator.remainingMisses;
        }
        else if (intermission)
        {
            if (!levelAnimation.isPlaying)
            {
                intermission = false;
            }
            specialModeText.alpha = 0;
        }
    }
}
