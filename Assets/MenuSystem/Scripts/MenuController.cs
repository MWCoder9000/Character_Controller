
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    #region Scenes
    [SerializeField] string playScene = "PlayerSandboxScene";
    [SerializeField] string mainMenuScene = "StartScreen";
    #endregion

    #region Panels
    [Tooltip("Drag in an options menu panel, if one exists")]
    [SerializeField] GameObject optionsMenuPanel;

    [Tooltip("Drag in an pause menu panel, if one exists")]
    [SerializeField] GameObject pauseMenuPanel;

    [Tooltip("Drag in a high scores panel, if one exists")]
    [SerializeField] GameObject highScoresPanel;
    #endregion

    [SerializeField] bool IsPauseMenuAvailable = false;
    [HideInInspector] public static bool IsGamePaused = false;
    [SerializeField] public bool InMainMenu = true;

    PlayerInput playerInput;
    InputAction escapeAction;
    void OnEnable()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerInput = player.GetComponent<PlayerInput>();
            var map = playerInput.currentActionMap;

            escapeAction = map.FindAction("Escape", true);
        }
    }

    void Update()
    {
        if (!InMainMenu)
        {
            GameManager.Instance.Paused = IsGamePaused;
        }
        PauseMenu();
    }

    public void HighScoreMenuClose()
    {
        highScoresPanel.SetActive(false);
    }

    public void HighScoreMenuOpen()
    {
        highScoresPanel.GetComponent<HighScoreSystem>().UpdateHighScoreUI();
        highScoresPanel.SetActive(true);
    }

    public void PauseMenu()
    {
        if (!GameManager.Instance.Dead)
        {
            if (IsPauseMenuAvailable)
            {
                if (escapeAction.triggered)
                {
                    if (IsGamePaused)
                    {
                        Resume();
                    }
                    else
                    {
                        Pause();
                    }
                }
            }
        }
    }

    public void OptionsMenuClose()
    {
        optionsMenuPanel.SetActive(false);
    }

    public void OptionsMenuOpen()
    {
        optionsMenuPanel.SetActive(true);
    }

    public void Pause()
    {
        Cursor.visible = true;
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void Resume()
    {
        Cursor.visible = false;
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

    public void ReturnToMainMenu()
    {
        Resume();
        Cursor.visible = true;
        InMainMenu = true;
        SceneManager.LoadScene(mainMenuScene);
    }

    public void StartGame()
    {
        Cursor.visible = false;
        InMainMenu = false;
        SceneManager.LoadScene(playScene);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT GAME");
        Application.Quit();
    }
}