using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Play,
        Pause,
    }

    public static GameManager Instance;

    [Header("Game State")]
    public GameState gameState = GameState.Play;
    [Header("UI")]
    public Image pauseImage;

    [Header("Player Properties")]
    public GameObject player;
    private float playerSpeed;
    [Range(0.2f, 3f)]
    public float viewRange = 1f;
    private Transform viewRangeObj;


    private void Start()
    {
        Instance = this;
        viewRangeObj = player.GetComponentInChildren<FogOfWarTrigger>().transform.parent;     
        playerSpeed = player.GetComponent<PlayerController>().navMeshAgent.speed;
    }
    private void Update()
    {
        viewRangeObj.transform.localScale = viewRange * Vector3.one * 20;

        if (Input.GetKeyDown(KeyCode.Escape) && gameState == GameState.Play)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) && gameState == GameState.Pause)
        {
            ContinueGame();
        }

        //Decrease view range
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (viewRange < 0.4f) return;
            viewRange -= 0.2f;
        }
        //Increase view range
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (viewRange > 3f) return;
            viewRange += 0.2f;
        }

    }
    void PauseGame()
    {
        player.GetComponent<PlayerController>().navMeshAgent.speed = 0;
        pauseImage.gameObject.SetActive(true);
        gameState = GameState.Pause;

    }
    void ContinueGame()
    {
        player.GetComponent<PlayerController>().navMeshAgent.speed = playerSpeed;
        pauseImage.gameObject.SetActive(false);
        gameState = GameState.Play;
    }

}
