using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button startButton;
    public Button optionsButton;
    public Button quitButton;

    public Transform target;
    public Camera mainCamera;

    void Start()
    {   
        if (mainCamera != null && target!=null){
            mainCamera.transform.position = new Vector3(target.position.x, target.position.y, target.position.z);
            mainCamera.transform.LookAt(target);
        }

        Debug.Log("Main Menu Manager started!");
        if(startButton != null && optionsButton != null && quitButton != null){
            startButton.onClick.AddListener(StartGame);
            optionsButton.onClick.AddListener(OpenOptions);
            quitButton.onClick.AddListener(QuitGame);
        }
        else{
            Debug.LogError("One or more buttons are not assigned in the inspector!");
        }
    }
    

    void StartGame()
    {
        Debug.Log("Start button clicked!");
        SceneManager.LoadScene("Main");
    }

    void OpenOptions()
    {
        Debug.Log("Option button clicked!");
        SceneManager.LoadScene("OptionsScene");
    }

    void QuitGame()
    {
        Debug.Log("Quit button clicked!");
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}