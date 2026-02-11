using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public GameObject itemPrefab;
    public Sprite icon;
    public float SaveTime = 1;
    float ElapsedTime;
    public string itemName;
    [TextArea(4, 16)]
    public string description;
    public float TrapDelay = 1f;
    public float weight = 0;
    public int quantity = 1;
    public int maxStackableQuantity = 1;
    public bool isStorable = false;
    public bool isConsumable = true;
    [SerializeField]
    bool isPickupOnCollision = false;
    bool isTrap = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isPickupOnCollision)
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
            if(gameObject.tag == "Trap")
            {
                isTrap = true;
                ElapsedTime = SaveTime;
            }
            else
            {
                isTrap = false;
            }
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (isTrap )
        {
            if (ElapsedTime%60 < 0)
            {
                other.SendMessageUpwards("Hit", 10000, SendMessageOptions.DontRequireReceiver);
                ElapsedTime = SaveTime;
            }
            else
            {
                ElapsedTime -= Time.deltaTime;
            }
        }
        else if (isPickupOnCollision && other.tag == "Player")
        {
            Interact();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
    }

    public void DmgPlayer(Collider other)
    {
        
    }


    public void Interact()
    {
        Debug.Log("Picked up" + transform.name);

        if (isStorable)
            Store();
        else 
            Use();
        

    }
    
    void Store()
    {
        Debug.Log("Storing" + transform.name);

        //TODO Inventory System

        Destroy(gameObject);
    }

    void Use()
    {
        Debug.Log("Using" + transform.name);
        if(isConsumable)
        {
            quantity--;
            if(quantity<=0)
            {
                Destroy(gameObject);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
