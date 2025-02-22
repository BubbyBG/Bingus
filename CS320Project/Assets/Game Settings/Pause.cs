using UnityEngine;
using UnityEngine.InputSystem;

/*
    Simple script to "pause" the game by freezing the timescale when escape is pressed (unpauses when esc pressed again)
    Will add an actual pause menu soon with options to exit, resume, to be discussed.
*/
public class Pause : MonoBehaviour
{
    private bool paused;
    void Start() {
        paused = false;
    }

    void Update() {
       
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if(paused == false) 
            {
                PauseGame();
                paused = true;
            }
            else {
                ResumeGame();
                paused = false;
            }
        }

    }

    public void PauseGame() {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

    public void ResumeGame() {
       
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
    }
}


