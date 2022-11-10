using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int damage;
    public int heal;

    [Header("Audio")]
    protected AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip hurtSound;
    public AudioClip healSound;

    protected Animator animator;

    [Header("Dialogues")] public AllDialogues allDialogues;

    public Entity targetEntity;
    public CollisionsManager collisionsManager;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public virtual void Attack()
    {
        audioSource.PlayOneShot(attackSound, 0.5F);
    }

    public virtual void TakeDamage(int damageAmount)
    {
        if (currentHealth == 0) {
            return ;
        }

        currentHealth -= damageAmount;
        if (currentHealth < 0) {
            currentHealth = 0;
            animator.SetTrigger("Die");
            return ;
        }

        animator.SetTrigger("Hurt");
        audioSource.PlayOneShot(hurtSound, 1F);
    }

    public virtual void Heal()
    {
        currentHealth += heal;

        animator.SetTrigger("Heal");
        audioSource.PlayOneShot(healSound, 1F);
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }
}
