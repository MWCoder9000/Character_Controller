using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerAttack : MonoBehaviour
{
    Transform playerContainer, WeaponContainer;
    Transform cameraTransform;
    [SerializeField]
    float range = 10f;
    [SerializeField]
    float rawDamage = 10f;
    PlayerInput playerInput;
    [SerializeField]
    LayerMask layermask;
    InputAction FireAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();
        var map = playerInput.currentActionMap;
        FireAction = map.FindAction("Attack", true);
    }

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        FireWeapon();
    }

    void FireWeapon()
    {
        SwitchWeapons switchWeapons = GetComponent<SwitchWeapons>();
        if (FireAction.triggered)
        {
            if (switchWeapons.GetWeapons() == SwitchWeapons.Weapons.Gun)
            {
                cameraTransform = Camera.main.transform;
                Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
                RaycastHit raycastHit;
                Debug.DrawRay(cameraTransform.position, cameraTransform.forward * range, Color.black, 1f);
                if (Physics.Raycast(ray, out raycastHit, range, layermask))
                {
                    if (raycastHit.transform != null)
                    {
                        raycastHit.collider.SendMessageUpwards("Hit", rawDamage, SendMessageOptions.DontRequireReceiver);
                    }
                }
                else
                {
                    Debug.Log("NO RAYCAST");
                }

            }
            else //Bow
            {
                Debug.Log("NEED TO CODE - ARROW LAUNCHED");
            }
        }
    }
}
