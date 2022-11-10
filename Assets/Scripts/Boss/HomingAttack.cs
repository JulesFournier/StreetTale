using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingAttack : TargetingAttack
{
    public override void Init()
    {
        base.Init();
        player = GameObject.FindGameObjectWithTag("Player");
        spawnPoints = new List<GameObject>();
        spawnPoints.Add(bossAnimator.GetComponent<Boss>().projectileSpawnPoint);
    }

    //Basic projectiles that attack based on the heart position
    public override IEnumerator Attack()
    {
        GameObject spawnPoint;

        for (int i = 0; i != nbProjectile; i++) {
            spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count-1)];

            Vector3 distance = player.transform.position - spawnPoint.transform.position;
            GameObject projectileGO = Instantiate(projectileTemplate);
            Projectile newProjectile = projectileGO.GetComponent<Projectile>();

            newProjectile.collisionsManager = collisionsManager;
            collisionsManager.AddProjectile(newProjectile);
            projectileGO.transform.position = spawnPoint.transform.position;
            newProjectile.rb.AddRelativeForce(distance.normalized * forceMultiplier);

            boss.Attack();
            yield return new WaitForSeconds(interval);

        }
        bossAnimator.SetBool("IsAttacking", false);
    }
}
