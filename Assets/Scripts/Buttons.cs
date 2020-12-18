using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Buttons : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    GameObject waitObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().color = new Color(255 / 255, 217 / 255f, 26 / 255f, 255 / 255f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().color = new Color(162 / 255f, 139 / 255f, 24 / 255f, 255 / 255f);
    }

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().color = new Color(162 / 255f, 139 / 255f, 24 / 255f, 255 / 255f);

        if (gameObject.GetComponentInChildren<TextMeshProUGUI>().text == "Quit")
        {
            Application.Quit();
        }
        else if (gameObject.GetComponentInChildren<TextMeshProUGUI>().text == "Try Again")
        {
            Instantiate(waitObject, new Vector3(-10.0f, 2.30f, 0), Quaternion.identity);
            GameObject.Find("GameOverText").GetComponent<TextMeshProUGUI>().alpha = 0;
            PumpkinGenerator.GameOverText.SetActive(false);
            PumpkinGenerator.ButtonTryAgain.GetComponentInChildren<TextMeshProUGUI>().alpha = 0;
            PumpkinGenerator.ButtonQuit.GetComponentInChildren<TextMeshProUGUI>().alpha = 0;
            PumpkinGenerator.ButtonTryAgain.SetActive(false);
            PumpkinGenerator.ButtonQuit.SetActive(false);
            Cursor.visible = false;
            PumpkinGenerator.crossHair.GetComponent<Renderer>().enabled = true;
        }
        else if (gameObject.GetComponentInChildren<TextMeshProUGUI>().text == "Play")
        {
            Instantiate(waitObject, new Vector3(-10.0f, 2.30f, 0), Quaternion.identity);
            GameObject.Find("GameStartText").GetComponent<TextMeshProUGUI>().alpha = 0;
            PumpkinGenerator.ButtonQuit.GetComponentInChildren<TextMeshProUGUI>().alpha = 0;
            PumpkinGenerator.ButtonQuit.SetActive(false);
            PumpkinGenerator.ButtonPlay.GetComponentInChildren<TextMeshProUGUI>().alpha = 0;
            PumpkinGenerator.ButtonPlay.SetActive(false);
            Cursor.visible = false;
            PumpkinGenerator.crossHair.GetComponent<Renderer>().enabled = true;
        }

    }

    void IDeselectHandler.OnDeselect(BaseEventData eventData)
    {
        Debug.Log("Unselected");
        gameObject.GetComponentInChildren<TextMeshProUGUI>().color = new Color(162 / 255f, 139 / 255f, 24 / 255f, 255 / 255f);
    }
}
