using System;
using System.Drawing;
using UnityEngine;

public class Lever : MonoBehaviour
{
    bool OnOff = false;
    public GameObject Switch;
    public GameObject On, Off;

    public GameObject ObjectAffected;
    public GameObject Open, Closed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Switch = this.gameObject;
        ColourSwap();
        LeverRotate();
        LeverAction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlipSwitch()
    {
        Debug.Log("Lever Flipped");
        OnOff = !OnOff;
        ColourSwap();
        LeverRotate();
        LeverAction();

    }

    void ColourSwap()
    {
        if (OnOff)
        {
            Debug.Log("ON");
            Renderer rend = this.gameObject.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material = new Material(rend.material);
                if (rend.material.HasProperty("_Color"))
                {
                    rend.material.SetColor("_Color", UnityEngine.Color.green);
                }
                else
                {
                    Debug.LogWarning("Material does not have a _Color property.");
                }
            }
        }
        else
        {
            Debug.Log("OFF");
            Renderer rend = this.gameObject.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material = new Material(rend.material);
                if (rend.material.HasProperty("_Color"))
                {
                    rend.material.SetColor("_Color", UnityEngine.Color.red);
                }
                else
                {
                    Debug.LogWarning("Material does not have a _Color property.");
                }
            }
        }
    }
    void LeverRotate()
    {
        if (OnOff)
        {
            this.gameObject.transform.position = On.transform.position;
            this.gameObject.transform.rotation = On.transform.rotation;
            return;
        }
        this.gameObject.transform.position = Off.transform.position;
        this.gameObject.transform.rotation = Off.transform.rotation;
    }
    void LeverAction()
    {
        if (OnOff)
        {
            ObjectAffected.gameObject.transform.position = Open.transform.position;
            ObjectAffected.gameObject.transform.rotation = Open.transform.rotation;
            return;
        }
        ObjectAffected.gameObject.transform.position = Closed.transform.position;
        ObjectAffected.gameObject.transform.rotation = Closed.transform.rotation;
    }
}
