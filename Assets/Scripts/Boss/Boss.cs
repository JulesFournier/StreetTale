using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;

public class Boss : Entity
{
    public ParticleSystem BossHurtParticleSystem;
    [Header("Spawn points")]
    public GameObject projectileSpawnPoint;
    public GameObject centerBox;
    public List<GameObject> cornersPoints;

    public List<DialogueHolder> dialogueHolders;
    public AudioManager audioManager;
    public AudioClip victoryMusic;

    [Header("Attack")]
    public List<BossAttack> attacks;

    public Animator explosion;
    public EndDisplay endDisplay;
    public PlayerHealth playerHealth;

    protected override void Init()
    {
        base.Init();
        StartCoroutine(AttackPlayer());
    }

    IEnumerator AttackPlayer()
    {
        while (targetEntity.currentHealth > 0 && currentHealth > 0) {
            if (IsIntroOver()) {
                animator.SetBool("IsAttacking", true);
                IEnumerator attack = attacks[Random.Range(0, attacks.Count)].Attack();
                StartCoroutine(attack);
                yield return new WaitForSeconds(Random.Range(2f, 5f));
            }
            yield return new WaitForSeconds(1f);
        }
        foreach (BossAttack attack in attacks) {
            attack.StopAllCoroutines();
        }
    }

    public override void TakeDamage(int damageAmount)
    {
        allDialogues.EnableRandomizedDialogue("BossHurtDialogue", 20);

        if (currentHealth == 0) {
            return ;
        }

        currentHealth -= damageAmount;
        if (currentHealth <= 0) {
            currentHealth = 0;
            StartCoroutine(Die());
            return ;
        }
        
        animator.SetTrigger("Hurt");
        audioSource.PlayOneShot(hurtSound, 1F);
        BossHurtParticleSystem.Play();
    }

    IEnumerator Die()
    {
        animator.SetTrigger("Die");
        StartCoroutine(audioManager.StopBossMusic(victoryMusic));
        collisionsManager.DestroyForEnd();
        yield return new WaitForSeconds(1.5f);
        explosion.gameObject.SetActive(true);
        explosion.Play("Explosion");
        yield return new WaitForSeconds(explosion.GetCurrentAnimatorStateInfo(0).length);
        explosion.gameObject.SetActive(false);

        playerHealth.isGameOver = true;
        endDisplay.gameObject.SetActive(true);
        endDisplay.DisplayWin();
    }

    bool IsIntroOver()
    {
        foreach (DialogueHolder dialogue in dialogueHolders) {
            if (dialogue.isFinished) {
                return (true);
            }
        }
        return (false);
    }
}
