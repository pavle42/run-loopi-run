using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public Transform[] loopiSlots;
    public float collectorMultiplier = 1f;
    public Transform parentGO;
    [Header("Boost Settings")]
    public float boostMoneyMultiplier = 5f;
    public float boostAnimSpeedMultiplier = 3f;
    public float boostDuration = 1f;

    private bool shouldProduce;
    private List<Loopi> activeLoopies = new List<Loopi>();
    private Animator anim;
    private Hoverable hoverable;
    private bool isBoosting;
    private float boostTimer;

    void Start()
    {
        anim = GetComponent<Animator>();
        hoverable = GetComponent<Hoverable>();
    }

    void Update()
    {
        if (shouldProduce)
        {
            ProduceMoney();
        }

        if (Input.GetMouseButtonDown(0) && hoverable.isHovered)
        {
            if (!isBoosting) ApplyBoost();
            boostTimer = boostDuration;
        }

        if (isBoosting)
        {
            boostTimer -= Time.deltaTime;
            if (boostTimer <= 0f) EndBoost();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Loopi")
        {
            if (activeLoopies.Count < loopiSlots.Length)
            {
                Loopi loopi = col.gameObject.GetComponent<Loopi>();
                loopi.StartRunning(loopiSlots[activeLoopies.Count]);
                activeLoopies.Add(loopi);
                loopi.gameObject.transform.SetParent(parentGO);

                if (isBoosting) loopi.anim.speed *= boostAnimSpeedMultiplier;

                shouldProduce = true;
                anim.SetBool("Rotate", true);
            }
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Loopi")
        {
            Loopi loopi = col.gameObject.GetComponent<Loopi>();
            loopi.StopRunning();
            activeLoopies.Remove(loopi);
            loopi.gameObject.transform.SetParent(null);

            if (activeLoopies.Count <= 0)
            {
                shouldProduce = false;
                anim.SetBool("Rotate", false);
            }
        }
    }

    void ProduceMoney()
    {
        float moneyMult = isBoosting ? collectorMultiplier * boostMoneyMultiplier : collectorMultiplier;

        foreach (Loopi l in activeLoopies)
        {
            Manager.Instance.money += (l.productionAmount * moneyMult * Time.deltaTime);
        }

        Manager.Instance.UpdateMoneyText();
    }

    void ApplyBoost()
    {
        isBoosting = true;

        // Speed up collector animation
        if (anim) anim.speed *= boostAnimSpeedMultiplier;

        // Speed up every current Loopi animation
        foreach (Loopi l in activeLoopies)
            l.anim.speed *= boostAnimSpeedMultiplier;
    }

    void EndBoost()
    {
        isBoosting = false;

        // Restore collector animation speed
        if (anim) anim.speed /= boostAnimSpeedMultiplier;

        // Restore Loopi animation speed
        foreach (Loopi l in activeLoopies)
            l.anim.speed /= boostAnimSpeedMultiplier;
    }
}
