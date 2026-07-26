using UnityEngine;

public class Battery : MonoBehaviour
{

    bool multipliersAdded = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.name == "Rocket" && GameManager.launched && !multipliersAdded)
        {
            int numChildren = transform.parent.childCount;
            for (int i = 0; i < numChildren; i++)
            {
                Propulsion rocketPart = transform.parent.GetChild(i).GetComponent<Propulsion>();
                if (rocketPart != null)
                {
                    rocketPart.propulsionPower = rocketPart.propulsionPower * 3;
                }
            }
            multipliersAdded = true;
        }
    }
}
