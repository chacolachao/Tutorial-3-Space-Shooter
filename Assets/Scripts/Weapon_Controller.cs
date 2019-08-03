using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Controller : MonoBehaviour
{
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float delay;
    public bool tracer = false;
    private GameObject player;

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource> ();
        InvokeRepeating ("Fire", delay, fireRate);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Fire ()
    {
        Quaternion r = shotSpawn.rotation;

        if(tracer) {
            Vector3 v = (player.transform.position - shotSpawn.position).normalized;
            r = Quaternion.LookRotation(v);
        }

        Instantiate (shot, shotSpawn.position, r);
        audioSource.Play ();
    }
}
