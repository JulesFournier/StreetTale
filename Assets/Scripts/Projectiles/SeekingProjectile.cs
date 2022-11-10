using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingProjectile : Projectile
{
    public Transform target;
    Vector3 targetPosition;

    public float followSpeed;
    public float slowRadius;

    Vector2 velocity;
    public SpriteRenderer visual;
    public Animator animator;

    public bool canFollow = false;

    void Update()
    {
        if (!canFollow) {
            return ;
        }
        targetPosition = target.transform.position;
        float targetSpeed;
        float distance = (targetPosition - transform.position).magnitude;

        if (distance < slowRadius) {
            targetSpeed = followSpeed * distance / slowRadius;
        } else {
            targetSpeed = followSpeed;
        }

        Vector2 targetVelocity = targetPosition - transform.position;
        targetVelocity.Normalize();
        targetVelocity *= targetSpeed;

        velocity = targetVelocity;
    }

    void LateUpdate()
    {
        if (!canFollow) {
            return ;
        }
        rb.velocity = velocity;
    }

    public void InvokeDestroy(float timeToInvoke)
    {
        StartCoroutine(DestroyProjectile(timeToInvoke/10f));
    }

    IEnumerator DestroyProjectile(float timeToInvoke)
    {
        yield return new WaitForSeconds(timeToInvoke*9f);
        animator.SetTrigger("Disappear");
        yield return new WaitForSeconds(timeToInvoke);
        collisionsManager.RemoveProjectile(this);
        Destroy(gameObject);
    }
}
