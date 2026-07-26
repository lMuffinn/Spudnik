using UnityEngine;

public class Explosion : MonoBehaviour
{

    Potato potato;
    ParticleSystem particles;
    public float explosionPower = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        potato = GameObject.FindAnyObjectByType<Potato>(FindObjectsInactive.Include);
        particles = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Launch()
    {
        particles.Play();
        particles.transform.SetParent(null);
        Vector2 launchDir = (potato.transform.position - transform.position).normalized;
        Debug.Log(launchDir * explosionPower);
        potato.GetComponent<Rigidbody2D>().AddForce(launchDir *  explosionPower);
        Destroy(gameObject);
    }

}
