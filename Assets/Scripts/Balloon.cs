using UnityEngine;

public class Balloon : MonoBehaviour
{

    Transform upDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upDir = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        upDir.transform.position = transform.position + Vector3.up;
    }
}
