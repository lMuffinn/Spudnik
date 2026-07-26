using UnityEngine;
using UnityEngine.InputSystem;

public class DragNDrop : MonoBehaviour
{
    PointAndClickActions actions;
    InputAction pickUp;
    InputAction rotate;
    Collider2D col;
    public bool held = false;
    public bool selected = false;
    public GameObject highlight;
    SpriteRenderer spriteRenderer;
    float rotationMultiplyer = 180f;
    GameManager gameManager;

    private void Awake()
    {
        actions = new PointAndClickActions();
    }

    private void OnEnable()
    {
        pickUp = actions.ClickAndPoint.leftclick;
        pickUp.Enable();
        rotate = actions.Rotate.rotate;
        rotate.Enable();
    }

    private void OnDisable()
    {
        pickUp.Disable();
        rotate.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
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
                DragNDrop[] dragNDrops = GameObject.FindObjectsByType<DragNDrop>(FindObjectsSortMode.None);
                bool itemAlreadyBeingHeld = false;
                foreach (DragNDrop item in dragNDrops)
                {
                    if (item.held) itemAlreadyBeingHeld = true;
                }
                if (!itemAlreadyBeingHeld)
                {
                    held = true;
                    selected = true;

                }
                //Debug.Log("ive been clicked");
            }
            else selected = false;
        }
        if (GameManager.launched)
        {
            selected = false;
        }
        if (held)
        {
            transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z);
        }
        if (pickUp.WasReleasedThisFrame())
        {
            held = false;
        }
        //handle highlights and rotation
        if (selected && gameManager.rocket != null)
        {
            if (highlight == null || highlight.GetComponent<SpriteRenderer>().sprite != spriteRenderer.sprite)
            {
                if (highlight != null) Destroy(highlight.gameObject);
                highlight = new GameObject();
                highlight.transform.SetParent(transform);
                highlight.transform.position = transform.position + new Vector3(0, 0, .1f);
                highlight.transform.rotation = transform.rotation;
                SpriteRenderer highlightSR = highlight.AddComponent<SpriteRenderer>();
                highlightSR.sprite = spriteRenderer.sprite;
                highlightSR.color = Color.red;
                highlight.transform.localScale = new Vector2(1.1f, 1.1f);
            }
            transform.Rotate(new Vector3(0, 0, -rotate.ReadValue<float>() * rotationMultiplyer * Time.deltaTime));
        }
        else if (highlight != null) Destroy(highlight.gameObject);
    }

    public void Launch()
    {
        if (highlight != null) Destroy(highlight.gameObject);
        selected = false;
    }

}
