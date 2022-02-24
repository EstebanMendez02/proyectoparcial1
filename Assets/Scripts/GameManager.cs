using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    PlayerScript player;
    [SerializeField]
    EnemyScript enemy;
    void Awake()
    {
        if(!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 2)
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
            enemy = GameObject.FindWithTag("Enemy").GetComponent<EnemyScript>();
        }
    }

    public PlayerScript GetPlayer => player;
    public void SetPlayer(PlayerScript player) => this.player = player;
    public EnemyScript GetEnemy => enemy;
}
