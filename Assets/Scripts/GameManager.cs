using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Owns game-flow state: score, win/lose, restart, quit.
// PlayerControl only reports what it touched; this script decides what that means for the game.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI countText;
    public TextMeshProUGUI winText;
    public GameObject winPanel; // assign once the Restart/Exit panel exists in the scene

    private int count;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        count = 0;
        if (countText != null) countText.text = "Count - 0";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void CollectCoin()
    {
        count++;
        if (countText != null) countText.text = "Count - " + count.ToString();
        if (count >= 10)
            Win();
    }

    private void Win()
    {
        if (winText != null) winText.gameObject.SetActive(true);
        if (winPanel != null) winPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Wired to the win panel's Exit button, once it exists.
    // Application.Quit() is a documented no-op in the Editor's Play mode, so this branch
    // keeps it testable there and does the real thing in a build.
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
