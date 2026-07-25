using UnityEngine;
using TMPro;

public class DisplayHeight : MonoBehaviour
{

    GameManager gameManager;
    TextMeshProUGUI tmpgui;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
        tmpgui = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.launched)
        {
            tmpgui.text = $"Height: {gameManager.highestY:F2}m";
        }
        else
        {
            tmpgui.text = "Height: 0";
        }
    }
}
