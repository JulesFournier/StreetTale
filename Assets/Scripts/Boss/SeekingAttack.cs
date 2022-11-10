using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingAttack : TargetingAttack
{
    public float liveTime;

    public override void Init()
    {
        base.Init();
        player = GameObject.FindGameObjectWithTag("Player");
        spawnPoints = new List<GameObject>();
        spawnPoints.Add(boss.centerBox);
    }

    //Projectiles that follow the heart for a certain time
    public override IEnumerator Attack()
    {
        GameObject spawnPoint;
        
        for (int i = 0; i != nbProjectile; i++) {
            spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count-1)];

            GameObject projectileGO = Instantiate(projectileTemplate);
            SeekingProjectile newProjectile = projectileGO.GetComponent<SeekingProjectile>();

            newProjectile.target = player.transform;
            newProjectile.collisionsManager = collisionsManager;
            newProjectile.InvokeDestroy(liveTime);
            collisionsManager.AddProjectile(newProjectile);
            projectileGO.transform.position = spawnPoint.transform.position;

            boss.Attack();
            for (float alphaIndex = 0; alphaIndex < interval; alphaIndex += 0.1f) {
                if (!newProjectile) {
                    yield break ;
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
            newProjectile.canFollow = true;
        }
        bossAnimator.SetBool("IsAttacking", false);
    }
}
