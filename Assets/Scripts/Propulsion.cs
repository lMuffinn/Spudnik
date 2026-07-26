using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Propulsion : MonoBehaviour
{

    public float propulsionPower = 100;
    public float propulsionLength = 10;

    float timer = 0;

    Rigidbody2D rb;
    public GameObject upReference;

    public ParticleSystem particles;
    bool particlesPlayed = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (upReference == null)
        {
            //Debug.Log("no preassigned up");
            upReference = transform.GetChild(0).gameObject;
            if (upReference == null)
            {
                //Debug.Log("no children to make up");
                upReference = new GameObject("Up Reference");
                upReference.transform.SetParent(transform, false);
                upReference.transform.position = upReference.transform.position + new Vector3(0, 1, 0);
            }
        }
        particles = GetComponentInChildren<ParticleSystem>();
        if (particles != null)
        {
            var main = particles.main;
            main.duration = propulsionLength;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            if (particles != null && !particlesPlayed)
            {
                particlesPlayed=true;
                particles.Play();
            }
            Vector2 upDir = upReference.transform.position - rb.transform.position;
            rb.AddForce(upDir * propulsionPower * Time.deltaTime);
            //Debug.Log(timer);
        }
        timer -= Time.deltaTime;
    }

    public void Launch()
    {
        timer = propulsionLength;
    }
}
