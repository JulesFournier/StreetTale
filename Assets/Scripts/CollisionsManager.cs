using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionsManager : MonoBehaviour
{
    public List<Pickup> pickups;
    public List<Projectile> projectiles;

    public List<Collider2D> fightBoxColliders;

    // Start is called before the first frame update
    void Start()
    {
        pickups = new List<Pickup>();
        projectiles = new List<Projectile>();
    }

    public void AddPickup(Pickup newPickup)
    {
        foreach (Pickup pickup in pickups) {
            Physics2D.IgnoreCollision(newPickup.c2D, pickup.c2D);
        }
        foreach (Projectile projectile in projectiles) {
            Physics2D.IgnoreCollision(newPickup.c2D, projectile.c2D);
        }
        foreach (Collider2D col in fightBoxColliders) {
            Physics2D.IgnoreCollision(newPickup.c2D, col);
        }
        pickups.Add(newPickup);
    }

    public void AddProjectile(Projectile newProjectile)
    {
        foreach (Pickup pickup in pickups) {
            Physics2D.IgnoreCollision(newProjectile.c2D, pickup.c2D);
        }
        foreach (Projectile projectile in projectiles) {
            Physics2D.IgnoreCollision(newProjectile.c2D, projectile.c2D);
        }
        if (!newProjectile.GetComponent<BouncingProjectile>()) {
            foreach (Collider2D col in fightBoxColliders) {
                Physics2D.IgnoreCollision(newProjectile.c2D, col);
            }
            //Ignorer les collisions avec la fightbox si le projectile n'est pas censé rebondir contre
        }
        projectiles.Add(newProjectile);
    }

    public void RemovePickup(Pickup pickup)
    {
        pickups.Remove(pickup);
    }

    public void RemoveProjectile(Projectile projectile)
    {
        projectiles.Remove(projectile);
    }

    public void DestroyForEnd()
    {
        foreach (Projectile projectile in projectiles) {
            Destroy(projectile.gameObject);
        }
        foreach (Pickup pickup in pickups) {
            Destroy(pickup.gameObject);
        }
        projectiles.Clear();
        pickups.Clear();
    }
}
