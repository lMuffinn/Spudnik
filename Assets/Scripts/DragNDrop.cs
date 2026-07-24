using UnityEngine;
using UnityEngine.InputSystem;

public class DragNDrop : MonoBehaviour
{
    PointAndClickActions actions;
    InputAction pickUp;
    Collider2D col;
    public bool held = false;

    private void Awake()
    {
        actions = new PointAndClickActions();
    }

    private void OnEnable()
    {
        pickUp = actions.ClickAndPoint.leftclick;
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
            // you have to input the vector 3 explicitly because if the z value is off bounds.contains wont register the click
            if (col.bounds.Contains(new Vector3(mouseWorldPos.x, mouseWorldPos.y, col.bounds.center.z)))
            {
                held = true;
                Debug.Log("ive been clicked");
            }
        }
        if (held)
        {
            transform.position = new Vector3 (mouseWorldPos.x,mouseWorldPos.y, transform.position.z);
        }
        if (pickUp.WasReleasedThisFrame())
        {
            held = false;
        }
    }
}
