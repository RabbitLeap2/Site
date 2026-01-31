using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject Menu;
    public GameObject SettingsMenu;
    private bool SettingsActive;
    private bool PauseActive;
    public string Area;
    void Start()
    {
        PauseActive = false;
        SettingsActive = false;
        Menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseActive = !PauseActive;
            Menu.SetActive(PauseActive);
            Cursor.lockState = PauseActive ? CursorLockMode.Confined : CursorLockMode.Locked;
            if (SettingsActive)
            {
                SettingsMenu.SetActive(false);
                PauseActive = false;
                SettingsActive = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    //Depois tornar em animação
    public void SettingsShow()
    {
        Menu.SetActive(false);
        SettingsMenu.SetActive(true);
        SettingsActive = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(Area);

    }
    public void ExitGame()
    {
        Application.Quit();

    }
}
