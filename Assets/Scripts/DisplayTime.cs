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
        else Debug.Log("Game manager found! i sure hope it doesn't get deleted");
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.timer > 0)
        {
            tmp.text = Mathf.Ceil(GameManager.timer).ToString();
        }
        else
        {
            tmp.text = "0";
        }
        if (GameManager.launched) gameObject.SetActive(false);
    }
}
