using UnityEngine;
using System.Collections;
using System.Collections.Generic;
        
public class HealthManager : MonoBehaviour
{
    [SerializeField]
    float hitPoints = 100f;

    void Hit(float rawDamage)
    {
        hitPoints -= rawDamage;
        Debug.Log("OUCH: " + hitPoints.ToString());
        if (hitPoints < 1)
            Debug.Log("TODO: GAME OVER - YOU DIED");
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
