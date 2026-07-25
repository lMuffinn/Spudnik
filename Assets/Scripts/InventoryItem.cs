using UnityEngine;

public class InventoryItem : MonoBehaviour
{

    GameObject backPack;
    DragNDrop dragNDrop;
    Transform parent;
    GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backPack = GameObject.Find("Backpack");
        dragNDrop = GetComponent<DragNDrop>();
        parent = transform.parent;
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dragNDrop.held)
        {
            if (backPack.GetComponent<Collider2D>().bounds.Contains(new Vector3(transform.position.x,transform.position.y,backPack.transform.position.z)))
            {
                transform.SetParent(backPack.transform);
            }
            else
            {
                if (gameManager.rocket == null)
                {
                    transform.SetParent(parent.transform);
                    transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y, transform.position.z);
                }
                else
                {
                    transform.SetParent(gameManager.rocket.transform);
                }
            }
        }
    }
}
