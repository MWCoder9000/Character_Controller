using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerLocomotion : MonoBehaviour
{
    CharacterController characterController;
    Transform playerContainer, cameraContainer, WeaponContainer, ArrowSpawnerContainer;
    public Transform ArmContainer;
    private int Perspective;
    private int Weapon;

    #region PlayerStats
    public float speed = 6.0f;
    public float jumpSpeed = 10f;
    public float mouseSensitivity = 0.5f;
    public float gravity = 20.0f;
    public float lookUpClamp = -30f;
    public float lookDownClamp = 60f;
    #endregion

    #region ActionKeys
    private Vector3 moveDirection = Vector3.zero;
    float rotateX, rotateY;
    PlayerInput playerInput;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction crouchAction;
    InputAction lookAction;
    InputAction previousAction;
    InputAction SwapAction;
    #endregion

    void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();
        var map = playerInput.currentActionMap;
        moveAction = map.FindAction("Move", true);
        jumpAction = map.FindAction("Jump", true);
        crouchAction = map.FindAction("Crouch", true);
        lookAction = map.FindAction("Look", true);
        previousAction = map.FindAction("Previous", true);
        SwapAction = map.FindAction("Next", true);
    }


    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        GameManager.ResetGame();
        characterController = GetComponent<CharacterController>();
        SetCurrentCamera();
        WeaponContainer = gameObject.transform.Find("Guns");
        Weapon = 1;
        ArrowSpawnerContainer = gameObject.transform.Find("ArrowSpawner");
    }

    void Update()
    {
        if (!(GameManager.Instance.Paused || GameManager.Instance.Dead))
        {
            Locomotion();
            RotateAndLook();
            PerspectiveCheck();
            WeaponsCheck();
        }
    }

    void SetCurrentCamera()
    {
        SwitchPerspective switchPerspective = GetComponent<SwitchPerspective>();
        if (switchPerspective.GetPerspective() == SwitchPerspective.Perspective.First)
        {
            playerContainer = gameObject.transform.Find("Container1P");
            cameraContainer = playerContainer.transform.Find("Camera1PContainer");
            Perspective = 1;
        }
        else
        {
            playerContainer = gameObject.transform.Find("Container3P");
            cameraContainer = playerContainer.transform.Find("Camera3PContainer");
            Perspective = 3;
        }
    }

    void Locomotion()
    {
        if (characterController.isGrounded) // When grounded, set y-axis to zero (to ignore it)
        {
            Vector2 move = moveAction.ReadValue<Vector2>();
            moveDirection = new Vector3(move.x, 0f, move.y);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (jumpAction.IsPressed())
            {
                moveDirection.y = jumpSpeed;
            }
            if (crouchAction.IsPressed())
            {
                characterController.height = 0.65f;
                characterController.center = new Vector3(0f, 0.5f, 0f);
            }
            else //if crouch unpressed
            {
                characterController.height = 2f;
                characterController.center = new Vector3(0f, 1f, 0f);
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);

    }

    void RotateAndLook()
    {
        Vector2 look = lookAction.ReadValue<Vector2>();

        rotateX = look.x * mouseSensitivity;
        rotateY -= look.y * mouseSensitivity;

        rotateY = Mathf.Clamp(rotateY, lookUpClamp, lookDownClamp);

        transform.Rotate(0f, rotateX, 0f);
        cameraContainer.transform.localRotation = Quaternion.Euler(rotateY, 0f, 0f);
        ArrowSpawnerContainer.transform.localRotation = Quaternion.Euler(rotateY, 0f, 0f);

        if (Perspective == 3)
        {
            WeaponContainer.transform.localRotation = Quaternion.Euler(90 + rotateY, 0f, 0f);
        }
        else
        {
            if (Weapon == 1)
            {
                WeaponContainer.transform.localRotation = Quaternion.Euler(90, 0f, 0f);
            }
        }

    }

    void PerspectiveCheck()
    {
        if (previousAction.WasPressedThisFrame())
        {
            SwitchPerspective switchPerspective = GetComponent<SwitchPerspective>();

            if (switchPerspective != null)
            {
                if (switchPerspective.GetPerspective() == SwitchPerspective.Perspective.First)
                {
                    switchPerspective.SetPerspective(SwitchPerspective.Perspective.Third);
                    Perspective = 3;
                }
                else
                {
                    switchPerspective.SetPerspective(SwitchPerspective.Perspective.First);
                    Perspective = 1;
                }

                SetCurrentCamera();
            }
        }
    }
    void WeaponsCheck()
    {
        SwitchPerspective switchPerspective = GetComponent<SwitchPerspective>();
        if (switchPerspective.GetPerspective() == SwitchPerspective.Perspective.First)
        {
            ArmContainer.gameObject.SetActive(false);
            if (SwapAction.WasPressedThisFrame())
            {
                SwitchWeapons switchWeapons = GetComponent<SwitchWeapons>();

                if (switchWeapons != null)
                {
                    SwapWeapons(false, switchWeapons, true);
                }
            }
        }
        else
        {
            SwitchWeapons switchWeapons = GetComponent<SwitchWeapons>();
            if (switchWeapons != null)
            {
                SwapWeapons(true, switchWeapons, SwapAction.WasPressedThisFrame());
            }
        }
    }

    void SwapWeapons(bool ThirdPerson, SwitchWeapons switchWeapons, bool Swap)
    {
        ArmContainer.gameObject.SetActive(false);
        if(!Swap)
        {
            if (switchWeapons.GetWeapons() == SwitchWeapons.Weapons.Bow)
            {
                SwapWeapons(true, switchWeapons, true);
            }
        }
        else
        {
            if (switchWeapons.GetWeapons() == SwitchWeapons.Weapons.Gun)
            {
                if (ThirdPerson)
                {
                    switchWeapons.SetWeapons(SwitchWeapons.Weapons.Sword);
                    WeaponContainer = gameObject.transform.Find("Swords");
                    ArmContainer.gameObject.SetActive(true);
                    Weapon = 3;
                }
                else
                {
                    switchWeapons.SetWeapons(SwitchWeapons.Weapons.Bow);
                    WeaponContainer = gameObject.transform.Find("Bows");
                    Weapon = 2;
                }
            }
            else if (switchWeapons.GetWeapons() == SwitchWeapons.Weapons.Bow)
            {
                switchWeapons.SetWeapons(SwitchWeapons.Weapons.Sword);
                WeaponContainer = gameObject.transform.Find("Swords");
                if (ThirdPerson)
                {
                    ArmContainer.gameObject.SetActive(true);
                }
                Weapon = 3;
            }
            else
            {
                switchWeapons.SetWeapons(SwitchWeapons.Weapons.Gun);
                WeaponContainer = gameObject.transform.Find("Guns");
                Weapon = 1;
            }
        }
    }
}