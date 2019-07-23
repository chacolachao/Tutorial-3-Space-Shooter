using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
   public GameObject hazard;
   public Vector3 spawnValues;
   public int hazardCount;
   public float spawnWait;
   public float startWait;
   public float waveWait;
   private int score;
   public Text ScoreText;
   public Text RestartText;
   public Text GameOverText;
   private bool gameOver;
   private bool restart;

    void Start ()
    {
        gameOver = false;
        restart = false;
        RestartText.text = "";
        GameOverText.text = "";
        StartCoroutine (SpawnWaves ());
        score = 0;
        UpdateScore();  
    }

    void Update ()
    {
        if (restart == true)
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex ) ;
            }
        }
        if (Input.GetKey("escape"))
        {
           Application.Quit(); 
        }

    }
   IEnumerator SpawnWaves ()
   {
       yield return new WaitForSeconds (startWait);
       while(true)
       {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);

            if (gameOver)
            {
                restart = true;
                RestartText.text = "Press 'R' to Restart";
                break;
            }
        }
    }
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        GameOverText.text = "Game Over";
        gameOver = true;
    }
}
        

       
        
