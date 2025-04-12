using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Assertions;

public class LowHealthEffect : MonoBehaviour
{
    CanvasGroup lowHealthCanvasGroup;
    public PlayerInfo playerInfo;

    void Start()
    {
        lowHealthCanvasGroup = GetComponent<CanvasGroup>();
        lowHealthCanvasGroup.alpha = 0f;
    }

    void Update()
    {
        if ((playerInfo.currentHealth <= playerInfo.healthThreshold * 0.25) && (playerInfo.currentHealth > 0))
        {
            lowHealthCanvasGroup.alpha = 1f;
        }
        else{
            lowHealthCanvasGroup.alpha = 0f;
        }
    }
}