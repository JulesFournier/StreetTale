using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayAttack : BossAttack
{
    public override void Init()
    {
        base.Init();
        spawnPoints = new List<GameObject>();
        spawnPoints.Add(bossAnimator.GetComponent<Boss>().projectileSpawnPoint);
    }

    //Basic projectiles that are thrown to spray the fight box
    public override IEnumerator Attack()
    {
        GameObject spawnPoint;
        Vector2 force = new Vector2(-0.75f, 0.15f);

        for (int i = 0; i != nbProjectile; i++) {
            spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count-1)];
            force = new Vector2(force.x + 0.05f, force.y - 0.05f);

            GameObject projectileGO = Instantiate(projectileTemplate);
            Projectile newProjectile = projectileGO.GetComponent<Projectile>();
            
            newProjectile.collisionsManager = collisionsManager;
            collisionsManager.AddProjectile(newProjectile);
            projectileGO.transform.position = spawnPoint.transform.position;
            newProjectile.rb.AddRelativeForce(force * forceMultiplier);
            
            boss.Attack();
            yield return new WaitForSeconds(interval);
        }
        bossAnimator.SetBool("IsAttacking", false);
    }
}
