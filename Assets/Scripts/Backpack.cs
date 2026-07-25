using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;

public class Backpack : MonoBehaviour
{

    private static Backpack instance;
    GameManager gameManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            instance.transform.position = transform.position;
            DragNDrop[] items = instance.gameObject.GetComponentsInChildren<DragNDrop>();
            List<Quaternion> itemAngles = new List<Quaternion>();
            foreach (DragNDrop item in items)
            {
                itemAngles.Add(item.transform.rotation);
            }
            instance.transform.rotation = transform.rotation;
            for (int i = 0; i < items.Length; i++)
            {
                items[i].transform.rotation = itemAngles[i];
            }
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveBackpack()
    {
        gameObject.SetActive(false);
    }

    public void UnRemoveBackpack()
    {
        gameObject.SetActive(true);
    }
}
