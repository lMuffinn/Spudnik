using UnityEngine;

public class CombinedItem : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    public Sprite onRocket;
    public Sprite offRocket;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.name == "Rocket")
        {
            spriteRenderer.sprite = onRocket;
        }
        else spriteRenderer.sprite = offRocket;
    }
}
