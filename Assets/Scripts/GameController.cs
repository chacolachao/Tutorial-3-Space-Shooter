using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class GameController : MonoBehaviour
{
   public GameObject[] hazards;
   public GameObject boss;
   public GameObject bossSpawn;
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
   private bool win;
   public Text winText;
   public Text creditText;
   private bool bossInitiate;
   public Text warningText;
   public Text redScreen;
   public AudioSource bossMusic;
   public AudioSource regMusic;
   public AudioSource warningMusic;
   public AudioSource winMusic;
   public AudioSource gmOver;
   public BGScroller bgScroller;
   public ParticleSystem[] stars;
   private bool living;


    void Start ()
    {
        gameOver = false;
        restart = false;
        win = false;
        bossInitiate = false;
        RestartText.text = "";
        GameOverText.text = "";
        winText.text = "";
        creditText.text = "";
        warningText.enabled = false; 
        StartCoroutine (SpawnWaves ());
        score = 0;
        UpdateScore();  
    }

    void Update ()
    {
        if (restart == true)
        {
            if (Input.GetKey(KeyCode.Return))
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
                GameObject hazard = hazards[Random.Range (0, hazards.Length)];

                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);

            if (gameOver)
            {
                win = false;
                restart = true;
                bossInitiate = false;
                RestartText.text = "Press 'ENTER' to Restart";
                break;
            }

            Debug.Log(bossInitiate);
            if (bossInitiate)
            {
                restart = false;
                break;
            }
        }

        if(bossInitiate) {
            StartCoroutine(SpeedupScroll());
            
            warningText.enabled = true;
            warningMusic.Play ();

            yield return new WaitForSeconds(4);

            warningText.enabled = false;

            regMusic.Stop ();
            bossMusic.Play ();

            Instantiate(boss, bossSpawn.transform.position, bossSpawn.transform.rotation);
        }
    }

    IEnumerator SpeedupScroll() 
    {
        float orig = bgScroller.scrollSpeed;
        float target = orig * 200;

        bgScroller.scrollSpeed = target;

        foreach(ParticleSystem star in stars) {
            var trails = star.trails;
            trails.enabled = true;

            var main = star.main;
            main.startSpeedMultiplier += 200;

            var oldSimSpeed = main.simulationSpeed;
            main.simulationSpeed *= 10000;
            yield return new WaitForSecondsRealtime(0.1f);
            main.simulationSpeed = oldSimSpeed;

            Debug.Log(main.startSpeedMultiplier);
        }
    }
    public void AddScore(int newScoreValue)
    {
        if (gameOver == false)
        {
            score += newScoreValue;
            UpdateScore();
        }
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 50)
          {
            
            gameOver = false;
            restart = false;
            bossInitiate = true;
           }
        if (score >= 1000)
        {
            gameOver = false;
            restart = true;
            winText.text = "YOU WON";
            creditText.text = "Game Created by Elizabeth Richards";
            RestartText.text = "Press 'ENTER' to Restart";
            bossMusic.Stop ();
            winMusic.Play ();

        }
    }


    public void GameOver()
    {
        if (win == false)
        {
            GameOverText.text = "Game Over";
            gameOver = true;
            bossMusic.Stop ();
            regMusic.Stop ();
            gmOver.Play ();
            restart = true;
            RestartText.text = "Press 'ENTER' to Restart";
        }
        
    }
}
        

       
        
