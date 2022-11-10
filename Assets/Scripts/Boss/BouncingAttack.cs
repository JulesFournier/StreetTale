using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingAttack : BossAttack
{
    public float liveTime;

    public override void Init()
    {
        base.Init();
        spawnPoints = new List<GameObject>();
        foreach(GameObject spawnPoint in boss.cornersPoints) {
            spawnPoints.Add(spawnPoint);
        }
    }

    void Update()
    {
        
    }

    //Projectiles that bounces on the limits of the fight box
    public override IEnumerator Attack()
    {
        GameObject spawnPoint;
        Vector2 force;

        for (int i = 0; i != nbProjectile; i++) {
            spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count-1)];
            force = new Vector2(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            );

            GameObject projectileGO = Instantiate(projectileTemplate);
            BouncingProjectile newProjectile = projectileGO.GetComponent<BouncingProjectile>();

            newProjectile.InvokeDestroy(liveTime);
            newProjectile.collisionsManager = collisionsManager;
            collisionsManager.AddProjectile(newProjectile);
            projectileGO.transform.position = spawnPoint.transform.position;
            
            boss.Attack();
            for (float alphaIndex = 0; alphaIndex < interval; alphaIndex += 0.1f) {
                if (!newProjectile) {
                    yield break;
                }
                newProjectile.visual.color = new Color(
                    newProjectile.visual.color.r,
                    newProjectile.visual.color.g,
                    newProjectile.visual.color.b,
                    newProjectile.visual.color.a + 0.05f
                );
                yield return new WaitForSeconds(0.1f);
            }
            newProjectile.c2D.enabled = true;
            newProjectile.rb.AddRelativeForce(force * forceMultiplier);
        }
        bossAnimator.SetBool("IsAttacking", false);
    }
}
