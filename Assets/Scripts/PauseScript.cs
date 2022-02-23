using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    Button btn;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(PauseClick);
    }

    void PauseClick()
    {
        Time.timeScale = Time.timeScale == 1f ? 0f : 1f;
    }
}
