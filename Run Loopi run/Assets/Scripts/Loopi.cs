using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loopi : MonoBehaviour
{
    public SkinnedMeshRenderer[] coloredBodyParts;
    public GameObject[] hands, hats, mouths, eyes;
    public Material[] colors;
    public float productionAmount = 1f;
    public Animator anim;

    private int[] pickedIndexes = { -1, -1, -1, -1, -1 };
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        pickedIndexes[0] = Random.Range(-1, hands.Length);
        pickedIndexes[1] = Random.Range(-1, hats.Length);
        pickedIndexes[2] = Random.Range(0, mouths.Length);
        pickedIndexes[3] = Random.Range(0, eyes.Length);
        pickedIndexes[4] = Random.Range(0, colors.Length);

        ChooseAtribute(hands, pickedIndexes[0]);
        ChooseAtribute(hats, pickedIndexes[1]);
        ChooseAtribute(mouths, pickedIndexes[2]);
        ChooseAtribute(eyes, pickedIndexes[3]);
        ChooseColor();
    }

    void ChooseAtribute(GameObject[] arr, int index)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (i == index)
            {
                arr[i].SetActive(true);
            }
            else
            {
                arr[i].SetActive(false);
            }
        }
    }

    void ChooseColor()
    {
        foreach (var smr in coloredBodyParts)
        {
            smr.material = colors[pickedIndexes[4]];
        }
    }

    public void StartRunning(Transform runningPlace)
    {
        transform.position = runningPlace.position;
        transform.rotation = runningPlace.rotation;
        anim.SetBool("Running", true);
    }

    public void StopRunning()
    {
        anim.SetBool("Running", false);
    }
}
