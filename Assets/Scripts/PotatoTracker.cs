using UnityEngine;

public class PotatoTracker : MonoBehaviour
{

    GameObject potato;
    bool launchStarted = false;
    float startingY;
    public float minYForXFollow = 10;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        potato = GameObject.Find("Potato");
        startingY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (launchStarted && potato.transform.position.y > startingY)
        {
            transform.position = new Vector3(transform.position.x, GameManager.highestY, transform.position.z);
            if (potato.transform.position.y > minYForXFollow)
            {
                transform.position = new Vector3(potato.transform.position.x, GameManager.highestY, transform.position.z);
            }
        }
    }

    public void Launch()
    {
        launchStarted = true;
    }
}
