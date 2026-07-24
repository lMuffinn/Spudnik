using UnityEngine;
using UnityEngine.InputSystem;

public class ClickableArea : MonoBehaviour
{

    GameObject parent;
    public GameObject objectToReplaceParent;
    PointAndClickActions inputActions;
    InputAction click;
    public Texture2D curser;
    Collider2D col;

    private void Awake()
    {
        inputActions = new PointAndClickActions();
    }

    private void OnEnable()
    {
        click = inputActions.ClickAndPoint.leftclick;
        click.Enable();
    }

    private void OnDisable()
    {
        click.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parent = transform.parent.gameObject;
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        if (col.bounds.Contains(mouseWorldPos))
        {
            Cursor.SetCursor(curser,new Vector2(16,16),CursorMode.Auto);
        }
        if (click.WasPressedThisFrame())
        {
            if (col.bounds.Contains(mouseWorldPos))
            {
                objectToReplaceParent.SetActive(true);
                parent.SetActive(false);
            }
        }
    }
}
