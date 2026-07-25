using UnityEngine;

public class AttachToPotato : MonoBehaviour
{

    GameObject potato;
    FixedJoint2D joint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        potato = GameObject.FindAnyObjectByType<Potato>(FindObjectsInactive.Include).gameObject;
        joint = GetComponent<FixedJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.name == "Rocket")
        {
            joint.connectedBody = potato.GetComponent<Rigidbody2D>();
        }
        else
        {
            joint.connectedBody = null;
        }
    }
}
