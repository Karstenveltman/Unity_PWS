using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MPdetonation : NetworkBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject bomb;
    [SerializeField] float fusetime;
    [SerializeField] GameObject clustel;
    [SerializeField] Rigidbody2D rb;
    GameObject[] playerCount;
    [SerializeField] float speed;
    public bool clusterTrue;
    public int clustelCount;
    public bool homingTrue;
    void Start()
    {
        if (!IsHost) return;
        playerCount = GameObject.FindGameObjectsWithTag("Player");
        Invoke("spawning", fusetime);
        Destroy(bomb, fusetime);
    }

    void spawning() {
        if (clusterTrue) {
            for (int i = 0; i < clustelCount; i++){
                Vector2 uitwijking = Random.insideUnitCircle.normalized * 0.3f;
                Instantiate(clustel, transform.localPosition + new Vector3(uitwijking.x, uitwijking.y, 0), Quaternion.identity).GetComponent<NetworkObject>().Spawn(true);
            }
        }
        Instantiate(explosion, transform.localPosition, Quaternion.identity).GetComponent<NetworkObject>().Spawn(true);
    }
    void Update()
    {
        if (!IsHost) return;
        if (homingTrue) {
            GameObject target = GetClosestEnemy(playerCount);
            Vector3 moveDir = (target.transform.position - transform.position).normalized;
            rb.velocity = moveDir * speed;
        }
    }

    GameObject GetClosestEnemy(GameObject[] players) {
        GameObject nearestPlayer = null;
        float minDist = Mathf.Infinity;
        foreach (GameObject nearbyPlayer in players)
        {
            if (nearbyPlayer == null) {
                Debug.Log("haii :3");
            }
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
