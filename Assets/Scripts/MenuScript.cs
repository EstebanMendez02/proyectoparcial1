using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    Button btnPlay;
    [SerializeField]
    Button btnCredits;
    [SerializeField]
    Button btnExit;
    
    void Start()
    {
        btnPlay.onClick.AddListener(()=> {
            SceneManager.LoadScene(1);
        });
        btnCredits.onClick.AddListener(()=> {
            SceneManager.LoadScene(1);
        });
        btnExit.onClick.AddListener(()=> {
            Application.Quit();
        });
    }

}
