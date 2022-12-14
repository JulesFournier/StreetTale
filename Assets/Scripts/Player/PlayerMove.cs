using UnityEngine;
using System.Collections;
using DialogueSystem;

public class PlayerMove : MonoBehaviour {
    public float speed = 10;
    private Rigidbody2D rigidBody2D;
    public Transform corner_max;
    public Transform corner_min;
    public Animator playerAnim;
    public Animator bossAnim;
    public Animator animator;
    private float x_min;
    private float y_min;
    private float x_max;
    private float y_max;
    private bool moving = false;
    private bool hasEverMoved = false;

    public PlayerHealth playerHealth;
    public float damageRecoveryTime;
    float timeSinceDamage;

    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        x_max = corner_max.position.x;
        x_min = corner_min.position.x;
        y_max = corner_max.position.y; 
        y_min = corner_min.position.y;
        playerAnim.SetTrigger("Attack");
        timeSinceDamage = damageRecoveryTime;
    }

    void FixedUpdate()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");
        float xSpeed = xMove * speed;
        float ySpeed = yMove * speed;
        Vector2 newVelocity = new Vector2(xSpeed, ySpeed);
        if (rigidBody2D.velocity != newVelocity) {
            moving = true;
            if (moving && !hasEverMoved) {
                playerHealth.allDialogues.EnableDialogue("BossUpsetDialogue");
            }
            hasEverMoved = true;
        }
        if (timeSinceDamage != damageRecoveryTime) {
            timeSinceDamage += Time.fixedDeltaTime;
            if (timeSinceDamage > damageRecoveryTime) {
                timeSinceDamage = damageRecoveryTime;
                animator.Play("New State");
            }
        }
        
        rigidBody2D.velocity = newVelocity;
        KeepWithinMinMaxRectangle();
    }
	
    private void KeepWithinMinMaxRectangle()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        float clampedX = Mathf.Clamp(x, x_min, x_max);
        float clampedY = Mathf.Clamp(y, y_min, y_max);
        transform.position = new Vector3(clampedX, clampedY, z);
    }
    
    void OnDrawGizmos(){
        Vector3 top_right = Vector3.zero; Vector3 bottom_right = Vector3.zero;
        Vector3 bottom_left = Vector3.zero;
        Vector3 top_left = Vector3.zero;
        if(corner_max && corner_min){
            top_right = corner_max.position;
            bottom_left = corner_min.position;
            bottom_right = top_right;
            bottom_right.y = bottom_left.y;
            top_left = top_right;
            top_left.x = bottom_left.x;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(top_right, bottom_right);
        Gizmos.DrawLine(bottom_right, bottom_left);
        Gizmos.DrawLine(bottom_left, top_left);
        Gizmos.DrawLine(top_left, top_right);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag) {
            case ("Projectile"):
                if (timeSinceDamage != damageRecoveryTime) {
                    break ;
                }
                playerHealth.TakeDamage(other.GetComponent<Projectile>().damage);
                timeSinceDamage = 0;
                //Destroy(other.gameObject);
                break;
            case ("Pickup"):
                Pickup pickup = other.GetComponent<Pickup>();
                switch (pickup.pickupType) {
                    case (Pickup.PickupType.ATTACK):
                        playerHealth.Attack();
                        Destroy(other.gameObject);
                        break;
                    case (Pickup.PickupType.HEALTH):
                        playerHealth.Heal();
                        Destroy(other.gameObject);
                        break;
                }
                break;
        }
    }
}