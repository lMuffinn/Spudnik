using UnityEngine;
using TMPro;

public class DisplayHeight : MonoBehaviour
{

    GameManager gameManager;
    TextMeshProUGUI tmpgui;
    static RectTransform rectTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
        if (gameManager == null) Debug.Log("no gamemanager found");
        else Debug.Log("game manager found, plz don't dissappear");
        tmpgui = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Display height view of gameManager.launched: " + GameManager.launched);
        if (GameManager.launched)
        {
            //Debug.Log("setting height text");
            tmpgui.text = $"Height: {GameManager.highestY:F2}m";
        }
        else
        {
            tmpgui.text = "Height: 0.00m";
        }
    }

    public static void ChangePos(Vector3 pos)
    {
        rectTransform.anchoredPosition = pos;
    }
}
