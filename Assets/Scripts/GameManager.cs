using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    GameObject rocket;

    public float timer = 10;

    Vector2 mouseScreenPos;
    Vector2 mouseWorldPos;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rocket = GameObject.Find("Rocket");   
        if ( rocket == null)
        {
            Debug.Log("Rocket Not Found");
        }
    }

    // Update is called once per frame
    void Update()
    {

        //countdown to launch
        timer -= Time.deltaTime;
        if (rocket != null)
        {
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
    }

    public void Launch()
    {
        Rigidbody2D[] children = rocket.GetComponentsInChildren<Rigidbody2D>();
        Propulsion[] propulsyThingies = GameObject.FindObjectsByType<Propulsion>(FindObjectsSortMode.None);
        foreach (Propulsion propulsyThingy in propulsyThingies)
        {
            propulsyThingy.Launch();
        }
        foreach (Rigidbody2D rb in children)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
