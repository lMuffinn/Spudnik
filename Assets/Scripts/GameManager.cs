using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject rocket;

    Potato potato;
    public static float highestY;

    static float rocketBuildingTime = 10;
    public static float timer = rocketBuildingTime;

    Vector2 mouseScreenPos;
    Vector2 mouseWorldPos;

    public static bool launched = false;

    private static GameManager instance;

    float lossTimer;
    public float timeToLose = 4;


    private void Awake()
    {
        if (instance == null)
        {
            Debug.Log("no gamemanager exists yet, that gets to be me!");
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Theres already a gamemanager, goodbye cruel world :(");
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
        //Debug.Log("looking for rocket");
        rocket = GameObject.Find("Rocket");   
        if (rocket == null)
        {
            //Debug.Log("Rocket Not Found");
        }
        highestY = 0;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        potato = GameObject.FindFirstObjectByType<Potato>();
        {
            Debug.Log("there aint no potatoes here");
        }
        lossTimer = timeToLose;
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
            //Debug.Log("Starting Timer");
            timer -= Time.deltaTime;
            if (timer < 0 && !launched) Launch();
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
        if (potato == null) potato = GameObject.FindAnyObjectByType<Potato>();
        else if (launched && potato.transform.position.y > highestY)
        {
            highestY = potato.transform.position.y;
            lossTimer = timeToLose;
        }

        //track time to lose
        if (launched)
        {
            //Debug.Log(highestY);
            lossTimer -= Time.deltaTime;
        }
        if (lossTimer < 0)
        {
            GameObject.FindFirstObjectByType<CompleteScreen>(FindObjectsInactive.Include).gameObject.SetActive(true);
            DisplayHeight.ChangePos(new Vector3(0, 200, 0));
        }

        //Debug.Log("GameManager View of GameManager.Launched:" + launched);

    }

    public void Launch()
    {
        Rigidbody2D[] children = rocket.GetComponentsInChildren<Rigidbody2D>();
        Propulsion[] propulsyThingies = GameObject.FindObjectsByType<Propulsion>(FindObjectsSortMode.None);
        DragNDrop[] dragNDrops = GameObject.FindObjectsByType<DragNDrop>(FindObjectsSortMode.None);
        
        //activate the rocket
        foreach (Propulsion propulsyThingy in propulsyThingies)
        {
            if (propulsyThingy.transform.parent.gameObject.name == "Rocket")
            {
                propulsyThingy.Launch();
            }
        }

        //enable physics for items making up the rocket
        foreach (Rigidbody2D rb in children)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        //turn off ability to move objects, disable highlight on selected items
        foreach (DragNDrop dragyThingy in dragNDrops)
        {
            dragyThingy.Launch();
            dragyThingy.enabled = false;
        }

        //potato tracker stays at the potatoes y pos so the camera can follow it
        PotatoTracker potatoTracker = GameObject.FindAnyObjectByType<PotatoTracker>();
        potatoTracker.Launch();

        launched = true;

        //make the backpack invisable for the launch
        GameObject.FindFirstObjectByType<Backpack>().RemoveBackpack();
    }

    public void NextScene()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene + 1);
    }

    public static void LoadGatherScene()
    {
        //Scene gatherScene = SceneManager.GetSceneByName("Gathering");
        SceneManager.LoadScene(0);
    }
    
    public static void LoadTitleScene()
    {
        //Scene titleScene = SceneManager.GetSceneByName("Title Screen");
        Reset();
        SceneManager.LoadScene(3);
    }

    public static void LoadCreditsScene()
    {
        //Scene creditsScene = SceneManager.GetSceneByName("Credits");
        Reset();
        SceneManager.LoadScene(2);
    }

    public static void LoadRocketScene()
    {
        //Scene rocketScene = SceneManager.GetSceneByName("Rocket Builder");
        SceneManager.LoadScene(1);
    }

    public static void Reset()
    {
        launched = false;
        timer = rocketBuildingTime;
        highestY = 0;
        Backpack backpack = GameObject.FindFirstObjectByType<Backpack>(FindObjectsInactive.Include);
        if (backpack != null )
        {
            Destroy(backpack.gameObject);
        }
        Destroy(GameObject.FindFirstObjectByType<GameManager>().gameObject);

    }

    public static void Retry()
    {
        Reset();
        LoadGatherScene();
    }

}
