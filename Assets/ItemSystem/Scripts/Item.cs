using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public GameObject itemPrefab;
    public Sprite icon;
    
    float ElapsedTime;
    public string itemName;
    [TextArea(4, 16)]
    public string description;
    public float Damage;
    public float SafeTime = 1;
    public float weight = 0;
    public int quantity = 1;
    public int maxStackableQuantity = 1;
    public bool isStorable = false;
    public bool isConsumable = true;
    public bool ResetOnExit = false;
    [SerializeField]
    bool isPickupOnCollision = false;
    bool isTrap = false;
    bool DmgTaken = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isPickupOnCollision)
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
            if(gameObject.CompareTag("Trap"))
            {
                isTrap = true;
                ElapsedTime = SafeTime;
            }
            else
            {
                isTrap = false;
            }
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (!GameManager.Instance.Dead)
        {
            if (isTrap)
            {
                if (ElapsedTime % 60 < 0)
                {
                    if(!DmgTaken)
                    {
                        other.SendMessageUpwards("Hit", Damage, SendMessageOptions.DontRequireReceiver);
                        ElapsedTime = SafeTime;
                        DmgTaken = true;
                    }
                    
                }
                else
                {
                    ElapsedTime -= Time.deltaTime;
                    DmgTaken = false;
                }
            }
            else if (isPickupOnCollision && other.CompareTag("Player"))
            {
                Interact();
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (isTrap && ResetOnExit)
        {
            ElapsedTime = SafeTime;
        }
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
