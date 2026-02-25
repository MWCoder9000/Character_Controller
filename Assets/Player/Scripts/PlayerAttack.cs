using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerAttack : MonoBehaviour
{
    Transform playerContainer, WeaponContainer;
    Transform cameraTransform;
    [SerializeField] GameObject shotSpawn;
    [SerializeField] GameObject shot;
    [SerializeField]
    float range = 10f;
    [SerializeField]
    float rawDamage = 10f;
    PlayerInput playerInput;
    [SerializeField]
    LayerMask layermask;
    InputAction FireAction;

    [SerializeField] float rateOfFire = 2f;
    [SerializeField] float shotSpeed = 800f;
    [SerializeField] float ElapsedTime = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();
        var map = playerInput.currentActionMap;
        FireAction = map.FindAction("Attack", true);
    }

    void Start()
    {
        ElapsedTime = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.Paused && !GameManager.Instance.Dead)
        {
            FireWeapon();
        }
    }

    void FireWeapon()
    {
        SwitchWeapons switchWeapons = GetComponent<SwitchWeapons>();
        if (FireAction.triggered)
        {
            if (switchWeapons.GetWeapons() == SwitchWeapons.Weapons.Gun)
            {
                Gun();
            }
            else if (switchWeapons.GetWeapons() == SwitchWeapons.Weapons.Bow)
            {
                Bow();
            }
            else
            {
                Sword();
            }
        }
    }

    void Gun()
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

    void Bow()
    {
        if (ElapsedTime % 60 < 0)
        {
            GameObject shotInstance = Instantiate(shot,
                                          shotSpawn.transform.position,
                                          shotSpawn.transform.rotation);

            shotInstance.GetComponent<Rigidbody>()
                .AddForce(shotSpawn.transform.forward * shotSpeed);
            ElapsedTime = rateOfFire;
        }
        else
        {
            ElapsedTime -= Time.deltaTime;
        }
    }

    void Sword()
    {

    }
}
