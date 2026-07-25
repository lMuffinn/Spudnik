using UnityEngine;

public class Backpack : MonoBehaviour
{

    private static Backpack instance;
    GameManager gameManager;

    private void Awake()
    {
        if (instance == null) 
        { 
            instance = this; 
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.launched) gameObject.SetActive(false);
    }
}
