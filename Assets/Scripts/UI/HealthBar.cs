using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Entity entity;

    // Update is called once per frame
    void Update()
    {
        Transform bar = transform.Find("Bar");
        bar.localScale = new Vector3((100f * (float)entity.currentHealth / (float)entity.maxHealth)/100f, 1f);
    }
}
