using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAttack : BossAttack
{
    public int randomThreshold;

    public override void Init()
    {
        base.Init();
        spawnPoints = new List<GameObject>();
        spawnPoints.Add(bossAnimator.GetComponent<Boss>().projectileSpawnPoint);
    }

    //Basic projectiles that are thrown on random
    public override IEnumerator Attack()
    {
        Debug.Log("random attack");
        GameObject spawnPoint;
        Vector2 force;
        
        for (int i = 0; i != nbProjectile; i++) {
            spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count-1)];
            force = new Vector2(Random.Range(-0.8f, 0f), Random.Range(0.1f, -0.7f));

            GameObject projectileGO = Instantiate(projectileTemplate);
            Projectile newProjectile = projectileGO.GetComponent<Projectile>();

            newProjectile.collisionsManager = collisionsManager;
            collisionsManager.AddProjectile(newProjectile);
            projectileGO.transform.position = spawnPoint.transform.position;
            newProjectile.rb.AddRelativeForce(force * Random.Range(forceMultiplier, forceMultiplier + randomThreshold));

            boss.Attack();
            yield return new WaitForSeconds(interval);
        }
        bossAnimator.SetBool("IsAttacking", false);
    }
}
