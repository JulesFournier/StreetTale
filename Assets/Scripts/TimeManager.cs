using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float timePassed;
    public PlayerHealth playerHealth;

    void Start()
    {
        timePassed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerHealth.isGameOver) {
            timePassed += Time.deltaTime;
        }
    }
}
