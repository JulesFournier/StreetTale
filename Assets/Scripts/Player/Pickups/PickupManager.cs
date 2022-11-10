using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;

public class PickupManager : MonoBehaviour
{
    [Header("Pickups")]
    public Pickup healthPickup;
    public float healthSpawnRate;

    public Pickup attackPickup;    
    public float attackSpawnRate;

    [Header("Spawn position")]
    public Transform leftLimit;
    public Transform rightLimit;

    public List<DialogueHolder> dialogueHolders;
    public CollisionsManager collisionsManager;

    public PlayerHealth playerHealth;
    public Boss boss;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnHealth", 0, healthSpawnRate);
        InvokeRepeating("SpawnAttack", 0, attackSpawnRate);
    }

    void SpawnHealth()
    {
        if (IsIntroOver() && playerHealth.currentHealth > 0 && boss.currentHealth > 0) {
            SpawnPickup(healthPickup);
        }
    }

    void SpawnAttack()
    {
        if (IsIntroOver() && playerHealth.currentHealth > 0 && boss.currentHealth > 0) {
            SpawnPickup(attackPickup);
        }
    }

    void SpawnPickup(Pickup pickup)
    {
        GameObject newProjectile = Instantiate(pickup).gameObject;
        Pickup newPickup = newProjectile.GetComponent<Pickup>();
        collisionsManager.AddPickup(newPickup);
        newPickup.collisionsManager = collisionsManager;
        newPickup.transform.position = new Vector3(Random.Range(leftLimit.position.x, rightLimit.position.x), leftLimit.position.y, 0);
        newPickup.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1f) * 100);
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
