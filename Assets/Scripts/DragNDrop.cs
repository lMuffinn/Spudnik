using UnityEngine;
using UnityEngine.InputSystem;

public class DragNDrop : MonoBehaviour
{
    RocketCreatorActions actions;
    InputAction pickUp;
    Collider2D col;
    bool held = false;

    private void Awake()
    {
        actions = new RocketCreatorActions();
    }

    private void OnEnable()
    {
        pickUp = actions.DragandDrop.pickUp;
        pickUp.Enable();
    }

    private void OnDisable()
    {
        pickUp.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        if (pickUp.WasPressedThisFrame())
        {
            if (col.bounds.Contains(mouseWorldPos))
            {
                held = true;
                Debug.Log("ive been clicked");
            }
        }
        if (held)
        {
            transform.position = mouseWorldPos;
        }
        if (pickUp.WasReleasedThisFrame())
        {
            held = false;
        }
    }
}
