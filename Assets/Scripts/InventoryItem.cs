using UnityEngine;

public class InventoryItem : MonoBehaviour
{

    GameObject backPack;
    DragNDrop dragNDrop;
    Transform parent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backPack = GameObject.Find("Backpack");
        dragNDrop = GetComponent<DragNDrop>();
        parent = transform.parent;
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
                transform.SetParent(parent.transform);
                transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y, transform.position.z);
            }
        }
    }
}
