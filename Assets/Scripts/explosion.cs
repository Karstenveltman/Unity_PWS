using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    [SerializeField] Transform transform;
    [SerializeField] Animator animator;
    [SerializeField] Object smoke;
    [SerializeField] float ExplosionRadius = 5;
    [SerializeField] float explosionMultiplier;
    private gyroscope killPlayer = null;
    void Start()
    {
        Explode();
        Destroy(smoke, 0.982f);
    }
    
    void Explode() {
        animator.SetBool("Explosion", true);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);

        foreach (Collider2D nearbyObject in colliders){
            Rigidbody2D rbOther = nearbyObject.GetComponent<Rigidbody2D>();
            if (rbOther != null){
                Vector2 distanceVector = nearbyObject.transform.position - transform.position;
                if (distanceVector.magnitude > 0) {
                    killPlayer = nearbyObject.GetComponent<gyroscope>();
                    if (killPlayer != null){
                        killPlayer.dead = true;
                    }
                    rbOther.AddForce(distanceVector.normalized * explosionMultiplier);

                }
            }
        }
    }

}
