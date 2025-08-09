using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static bool finished = false;
    public AudioClip clickSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GameObject.Find("SFX").GetComponent<AudioSource>();
    }

    public void TutorialContinue()
    {
        finished = true;
        Cursor.lockState = CursorLockMode.Locked;
        audioSource.PlayOneShot(clickSound);
        gameObject.SetActive(false);
    }
}
