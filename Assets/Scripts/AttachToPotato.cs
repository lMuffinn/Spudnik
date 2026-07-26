using UnityEngine;

public class AttachToPotato : MonoBehaviour
{

    GameObject potato;
    FixedJoint2D joint;
    HingeJoint2D hinge;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        potato = GameObject.FindAnyObjectByType<Potato>(FindObjectsInactive.Include).gameObject;
        joint = GetComponent<FixedJoint2D>();
        //Debug.Log(gameObject.name + ": tried to get joint, now getting hinge");
        hinge = GetComponent<HingeJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(hinge);
        if (joint != null)
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
        if (hinge != null)
        {
            //Debug.Log("hinge exists, now trying to attack it to rocket");
            if (transform.parent.transform.parent.name == "Rocket")
            {
                hinge.connectedBody = potato.GetComponent<Rigidbody2D>();
            }
        }
    }
}
