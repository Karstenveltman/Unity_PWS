using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detonation : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] Object bomb;
    [SerializeField] float fusetime;
    [SerializeField] Object clustel;
    [SerializeField] Rigidbody2D rb;
    GameObject[] playerCount;
    [SerializeField] float speed;
    public bool clusterTrue;
    public int clustelCount;
    public bool homingTrue;
    void Start()
    {
        playerCount = GameObject.FindGameObjectsWithTag("Player");
        Invoke("spawning", fusetime);
        Destroy(bomb, fusetime);
    }

    void spawning() {
        //if it's a clusterbomb, spawn the small bombs
        if (clusterTrue) {
            for (int i = 0; i < clustelCount; i++){
                Vector2 uitwijking = Random.insideUnitCircle.normalized * 0.3f;
                Instantiate(clustel, transform.localPosition + new Vector3(uitwijking.x, uitwijking.y, 0), Quaternion.identity);
            }
        }
        //spawn the explosion
        Instantiate(explosion, transform.localPosition, Quaternion.identity);
    }
    void Update()
    {
        //if it's a homing bomb, follow the player
        if (homingTrue) {
        GameObject target = GetClosestEnemy(playerCount);
        Vector3 moveDir = (target.transform.position - transform.position).normalized;
        rb.velocity = moveDir * speed;
        }
    }
    //find the closest player
    GameObject GetClosestEnemy(GameObject[] players) {
        GameObject nearestPlayer = null;
        float minDist = Mathf.Infinity;
         foreach (GameObject nearbyPlayer in players)
        {
            float dist = Vector3.Distance(nearbyPlayer.transform.position, transform.position);
            if (dist < minDist)
            {
                nearestPlayer = nearbyPlayer;
                minDist = dist;
            }
        }
        return nearestPlayer;
    }
}
