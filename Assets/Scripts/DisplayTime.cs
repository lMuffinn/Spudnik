using UnityEngine;
using TMPro;

public class DisplayTime : MonoBehaviour
{

    GameManager gameManager;
    TextMeshProUGUI tmp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
        if (gameManager == null) Debug.Log("no game manager found");
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.timer > 0)
        {
            tmp.text = Mathf.Ceil(gameManager.timer).ToString();
        }
        else
        {
            tmp.text = "0";
        }
        if (gameManager.launched) gameObject.SetActive(false);
    }
}
