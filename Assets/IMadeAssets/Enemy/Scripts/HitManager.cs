using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class HitManager : MonoBehaviour
{
    [SerializeField]
    float hitPoints = 25;
    
    void Hit(float rawDamage)
    {
        Debug.Log("HitPoints: " + hitPoints);
        hitPoints -= rawDamage; 
        if(hitPoints < 1 )
        {
            Invoke("SelfTerminate", 0f);
        }
    }
    void SelfTerminate()
    {
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
