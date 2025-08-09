using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopiBox : MonoBehaviour
{
    public GameObject loopi, openEffect;
    public AudioClip poofSound;

    private AudioSource audioSource;
    private Hoverable hoverableScript;

    void Start()
    {
        hoverableScript = GetComponent<Hoverable>();
        audioSource = GameObject.Find("SFX").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && hoverableScript.isHovered)
        {
            Instantiate(loopi, transform.position, Quaternion.identity);
            Instantiate(openEffect, transform.position, Quaternion.identity);
            audioSource.PlayOneShot(poofSound);
            Destroy(gameObject);
        }
    }
}
