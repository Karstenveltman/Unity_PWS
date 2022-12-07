using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detonation : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] Object bomb;
    [SerializeField] Camera mainCamera;
    [SerializeField] float fusetime;
    [SerializeField] Object clustel;
    public Vector3 explosionSize;
    public bool clusterTrue;
    public int clustelCount;
    void Start()
    {
        Invoke("spawning", fusetime);
        Destroy(bomb, fusetime);
    }


    void spawning() {
        if (clusterTrue) {
            for (int i = 0; i < clustelCount; i++){
                Vector2 uitwijking = Random.insideUnitCircle.normalized * 0.3f;
                Instantiate(clustel, transform.localPosition + new Vector3(uitwijking.x, uitwijking.y, 0), Quaternion.identity);
            }
        }
        Instantiate(explosion, transform.localPosition, Quaternion.identity);
    }
    void Update()
    {

    }
}
