using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gameController;
    public int health = 1;
    private bool living;
    private Animator anim;

    void Start ()
    {
       GameObject gameControllerObject = GameObject.FindWithTag("GameController");
       if (gameControllerObject != null) 
       {
           gameController = gameControllerObject.GetComponent <GameController>();
       }
       anim.SetBool ("living", true);
    }
   void OnTriggerEnter (Collider other)
   {
       if (other.CompareTag ("Boundary") || other.CompareTag ("Enemy") || other.CompareTag("Boss"))
       {
           return;
       }

       if (explosion !=null)
       {
           Instantiate (explosion, transform.position, transform.rotation);
       }
    
       if (other.CompareTag ("Player"))
       {
           Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
           gameController.GameOver ();
       }

        if(--health <= 0) 
        {
            gameController.AddScore (scoreValue);
            Debug.Log("oof " + other.name);
            Destroy(gameObject);
        }
       Destroy (other.gameObject);
       
   }
}
