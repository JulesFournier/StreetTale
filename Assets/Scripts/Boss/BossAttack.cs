using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject projectileTemplate;
    public int nbProjectile;
    public float interval;
    public int forceMultiplier;

    public List<GameObject> spawnPoints;

    public CollisionsManager collisionsManager;
    public Animator bossAnimator;
    public Boss boss;

    void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        collisionsManager = GameObject.Find("CollisionsManager").GetComponent<CollisionsManager>();
        bossAnimator = GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
    }

    public virtual IEnumerator Attack()
    {
        yield return null;
    }
}
