using UnityEngine;

public class GameManager : MonoBehaviour
{

    GameObject rocket;

    public float timer = 10;

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
        timer -= Time.deltaTime;
        if (timer < 0) Launch();
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
