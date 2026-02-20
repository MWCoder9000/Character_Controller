using System.Buffers;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }

    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Death
    public string killedBy = "";
    public bool Dead = false;
    #endregion

    #region map
    public InputActionMap map;
    PlayerInput playerInput;
    private void OnEnable()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        playerInput = GetComponent<PlayerInput>();
        map = playerInput.currentActionMap;
    }
    #endregion
}