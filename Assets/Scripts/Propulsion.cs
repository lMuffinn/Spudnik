using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Propulsion : MonoBehaviour
{

    public float propulsionPower = 10;
    public float propulsionLength = 10;

    float timer = 0;

    Rigidbody2D rb;
    GameObject upReference;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        upReference = new GameObject("Up Reference");
        upReference.transform.SetParent(transform, false);
        upReference.transform.position = upReference.transform.position + new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            Vector2 upDir = upReference.transform.position - rb.transform.position;
            rb.AddForce(upDir * 10);
            Debug.Log(timer);
        }
        timer -= Time.deltaTime;
    }

    public void Launch()
    {
        timer = propulsionLength;
    }
}
