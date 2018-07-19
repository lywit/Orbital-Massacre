using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour {

    [SerializeField]
    GameObject menu;
    [SerializeField]
    GameObject controlsMenu;

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void ShowControls()
    {
        menu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void HideControls()
    {
        menu.SetActive(true);
        controlsMenu.SetActive(false);
    }
}
