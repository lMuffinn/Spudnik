using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject rocket;

    GameObject potato;
    public float highestY;

    public float timer = 10;

    Vector2 mouseScreenPos;
    Vector2 mouseWorldPos;

    public bool launched = false;

    private static GameManager instance;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("looking for rocket");
        rocket = GameObject.Find("Rocket");   
        if (rocket == null)
        {
            Debug.Log("Rocket Not Found");
        }
        highestY = 0;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        potato = GameObject.FindFirstObjectByType<Potato>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        //countdown to launch
        if (rocket == null)
        {
            rocket = GameObject.Find("Rocket");
        }
        if (rocket != null)
        {
            Debug.Log("Starting Timer");
            timer -= Time.deltaTime;
            if (timer < 0) Launch();
        }
        
        //setting cursor to default when not hovering over anything
        mouseScreenPos = Mouse.current.position.ReadValue();
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        bool cursorbeingused = false;

        ClickableArea[] clickableAreas = GameObject.FindObjectsByType<ClickableArea>(FindObjectsSortMode.None);
        foreach (ClickableArea area in clickableAreas)
        {
            if (!area.gameObject.activeInHierarchy) continue; //edge case, don't check them if they are not active
            if (area.gameObject.GetComponent<Collider2D>().bounds.Contains(mouseWorldPos)) cursorbeingused = true;
        }
        if (!cursorbeingused)
        {
            Cursor.SetCursor(null, new Vector2(16, 16), CursorMode.Auto);
        }

        //track potato height
        if (potato.transform.position.y > highestY)
        {
            highestY = potato.transform.position.y;
        }
    }

    public void Launch()
    {
        Rigidbody2D[] children = rocket.GetComponentsInChildren<Rigidbody2D>();
        Propulsion[] propulsyThingies = GameObject.FindObjectsByType<Propulsion>(FindObjectsSortMode.None);
        DragNDrop[] dragNDrops = GameObject.FindObjectsByType<DragNDrop>(FindObjectsSortMode.None);
        foreach (Propulsion propulsyThingy in propulsyThingies)
        {
            if (propulsyThingy.transform.parent.gameObject.name == "Rocket")
            {
                propulsyThingy.Launch();
            }
        }
        foreach (Rigidbody2D rb in children)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        foreach (DragNDrop dragyThingy in dragNDrops)
        {
            dragyThingy.enabled = false;
        }
        PotatoTracker potatoTracker = GameObject.FindAnyObjectByType<PotatoTracker>();
        potatoTracker.Launch();
        launched = true;
    }

    public void NextScene()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene + 1);
    }

}
