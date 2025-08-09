using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitor : MonoBehaviour
{
    public GameObject[] pages; // 0 is Home Page, 1 is Miner Page, 2 is Shop Page, 3 is Help Page
    public GameObject[] pointers; // 0 is basic dot pointer, 1 is mouse cursor
    public GameObject[] buyables; // 0 is Loopi box, 1, 2, and 3 are collectors
    public float[] prices; // 0 is Loopi price, 1, 2, and 3 are collector prices
    public Transform spawnPoint;
    public AudioClip clickSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GameObject.Find("SFX").GetComponent<AudioSource>();

        ChangePage(0);
        ChangePointer(0);
    }

    public void ChangePage(int pageIndex)
    {
        audioSource.PlayOneShot(clickSound);

        for (int i = 0; i < pages.Length; i++)
        {
            if (pageIndex == i)
            {
                pages[i].SetActive(true);
            }
            else
            {
                pages[i].SetActive(false);
            }
        }
    }

    public void ChangePointer(int pointerIndex)
    {
        for (int i = 0; i < pointers.Length; i++)
        {
            if (pointerIndex == i)
            {
                pointers[i].SetActive(true);
            }
            else
            {
                pointers[i].SetActive(false);
            }
        }
    }

    public void Buy(int index)
    {
        audioSource.PlayOneShot(clickSound);

        if (Manager.Instance.money >= prices[index])
        {
            Instantiate(buyables[index], spawnPoint.position, Quaternion.identity);
            Manager.Instance.money -= prices[index];
            Manager.Instance.UpdateMoneyText();
        }
    }

    public void Quit()
    {
        audioSource.PlayOneShot(clickSound);

        Application.Quit();
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
