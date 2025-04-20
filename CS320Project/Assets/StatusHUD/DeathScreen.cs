using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
// using UnityEngine.Assertions;

public class DeathScreen : MonoBehaviour
{
    public CanvasGroup deathCanvasGroup;

    void Start()
    {
        deathCanvasGroup = GetComponent<CanvasGroup>();
        deathCanvasGroup.alpha = 0f;
    }

    public void activate()
    {
        StartCoroutine(displayScreenLoadScene());
    }

    private IEnumerator displayScreenLoadScene()
    {
        float timeElapsed = 0f;
        float fadeDuration = 2f;
        while (timeElapsed < fadeDuration)
        {
            deathCanvasGroup.alpha = timeElapsed / fadeDuration;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        deathCanvasGroup.alpha = 1f;

        yield return new WaitForSeconds(3);

        SaveManager.isLoadGame = true;
        SceneManager.LoadScene("Progression Main_Outdoor Copy"); // error
    }
}
