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
    [SerializeField]
    Button btnAJugar;
    [SerializeField]
    Button btnRegresar;

    AudioSource aud;
    [SerializeField]
    AudioClip clickSFX;

    IEnumerator timerPlay, timerCredits, timerExit, timerAJugar, timerRegresar;
    
    void Start()
    {
        aud = GetComponent<AudioSource>();
        timerPlay = TPlay();
        timerCredits = TCredits();
        timerExit = TExit();
        timerAJugar = TAJugar();
        timerRegresar = TRegresar();

        btnPlay.onClick.AddListener(()=> {
            StartCoroutine(timerPlay);
        });
        btnCredits.onClick.AddListener(()=> {
            StartCoroutine(timerCredits);
        });
        btnExit.onClick.AddListener(()=> {
            StartCoroutine(timerExit);
        });

        //segunda pantalla
        btnAJugar.onClick.AddListener(()=> {
            StartCoroutine(timerAJugar);
        });

        //credits
        btnRegresar.onClick.AddListener(()=> {
            StartCoroutine(timerRegresar);
        });
    }

    IEnumerator TPlay()
    {
        aud.PlayOneShot(clickSFX, 1f);
        yield return new WaitForSeconds(.6f);
        SceneManager.LoadScene(1);
    }
    IEnumerator TCredits()
    {
        aud.PlayOneShot(clickSFX, 1f);
        yield return new WaitForSeconds(.6f);
        SceneManager.LoadScene(3);
    }
    IEnumerator TExit()
    {
        aud.PlayOneShot(clickSFX, 1f);
        yield return new WaitForSeconds(.6f);
        Application.Quit();
    }
    IEnumerator TAJugar()
    {
        aud.PlayOneShot(clickSFX, 1f);
        yield return new WaitForSeconds(.6f);
        SceneManager.LoadScene(2);
    }
    IEnumerator TRegresar()
    {
        aud.PlayOneShot(clickSFX, .6f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }


}
