using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Object smoke;
    [SerializeField] float explosionRadius;
    [SerializeField] float explosionMultiplier;
    private playercontroller damagePlayer = null;
    void Start()
    {
        Explode();
        Destroy(smoke, 0.982f);
    }
    
    void Explode() {
        animator.SetBool("Explosion", true);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        //checks if there are players inside the explosionradius
        foreach (Collider2D nearbyObject in colliders){
            Rigidbody2D rbOther = nearbyObject.GetComponent<Rigidbody2D>();
            //if there are, the player is hit
            if (rbOther != null){
                Vector2 distanceVector = nearbyObject.transform.position - transform.position;
                if (distanceVector.magnitude > 0) {
                    damagePlayer = nearbyObject.GetComponent<playercontroller>();
                    if (damagePlayer != null){
                        damagePlayer.hit = true;
                    }
                    //knockback
                    rbOther.AddForce(distanceVector.normalized * explosionMultiplier);
                }
            }
        }
    }

}
