using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Entity
{
    public PlayerMove playerMove;

    public EndDisplay endDisplay;
    public AudioClip gameOverMusic;
    public AudioManager audioManager;
    public Animator explosion;

    public bool isGameOver = false;

    public ParticleSystem ParticleSoulHeal;

    public override void Attack()
    {
        base.Attack();
        animator.SetTrigger("Attack");
        targetEntity.TakeDamage(damage);
    }

    public override void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        //base.TakeDamage(damageAmount);
        allDialogues.EnableRandomizedDialogue("PlayerHurtDialogue", 25);
        if (currentHealth <= 0 && isGameOver == false) {
            currentHealth = 0;
            animator.SetTrigger("Die");
            isGameOver = true;
            collisionsManager.DestroyForEnd();
            StartCoroutine(audioManager.StopBossMusic(gameOverMusic));
            StartCoroutine(ManageGameOver());
        }
        playerMove.animator.Play("HeartHurt");
        animator.SetTrigger("Hurt");
        audioSource.PlayOneShot(hurtSound, 1F);
    }

    IEnumerator ManageGameOver()
    {
        yield return new WaitForSeconds(1.5f);
        explosion.gameObject.SetActive(true);
        explosion.Play("Explosion");
        yield return new WaitForSeconds(explosion.GetCurrentAnimatorStateInfo(0).length);
        explosion.gameObject.SetActive(false);
        endDisplay.gameObject.SetActive(true);
        endDisplay.DisplayGameOver();
    }

    public override void Heal()
    {
        allDialogues.EnableRandomizedDialogue("PlayerHealDialogue", 20);
        base.Heal();
        ParticleSoulHeal.Play();
    }
}
