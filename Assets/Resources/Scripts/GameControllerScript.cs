using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameUI))]
public class GameControllerScript : MonoBehaviour
{
    public static GameControllerScript Instance { get; private set; }

    [SerializeField]
    private int knifeCount;
    [Header("Knife Spawning")]
    [SerializeField]
    private Vector2 knifeSpawnPosition;
    [SerializeField]
    private GameObject knifeObject;

    public GameUI GameUI { get; private set; }

    private void Awake(){
        Instance = this;
        GameUI = GetComponent<GameUI>();
    }

    private void Start(){
        GameUI.SetInitialDisplayKnifeCount(knifeCount);
        SpawnKnife();
    }

    private void OnSuccessfullKnifeHit(){
        if(knifeCount > 0){
            SpawnKnife()
        } else {
            StartGameOverSequence();
        }
    }

    private void SpawnKnife(){
        knifeCount--;
        Instantiate(knifeObject, knifeSpawnPosition, Quaternion.identity);
    }

    public void StartGameOverSequence(bool win){
        StartCoroutine("GameOverSequenceCoroutine",win);
    }

    private IEnumerator GameOverSequenceCoroutine(bool win){
        if(win){
            yield return new WaitForSecondsRealtime(0.3f);
        }else{
            GameUI.DisplayRestartButton();
        }
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
    }
}
