using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    [SerializeField] Transform transform;
    [SerializeField] Animator animator;
    [SerializeField] Object smoke;
    [SerializeField] float explosionRadius = 5;
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

        foreach (Collider2D nearbyObject in colliders){
            Rigidbody2D rbOther = nearbyObject.GetComponent<Rigidbody2D>();
            if (rbOther != null){
                Vector2 distanceVector = nearbyObject.transform.position - transform.position;
                if (distanceVector.magnitude > 0) {
                    damagePlayer = nearbyObject.GetComponent<playercontroller>();
                    if (damagePlayer != null){
                        damagePlayer.lives--;
                    }
                    rbOther.AddForce(distanceVector.normalized * explosionMultiplier);

                }
            }
        }
    }

}
