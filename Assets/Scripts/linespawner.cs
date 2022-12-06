using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linespawner : MonoBehaviour
{
    [SerializeField] Object bom;
    [SerializeField] Camera mainCamera;
    [SerializeField] float amount;
    void Start()
    {
       InvokeRepeating("spawning", 0f, 7f);
    }


    void spawning() {
        for(int i = 0; i < amount; i++){
            Instantiate(bom, new Vector3(Random.Range(-mainCamera.aspect * mainCamera.orthographicSize, mainCamera.aspect * mainCamera.orthographicSize), Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize)), Quaternion.identity);
        }
    }

    void Update()
    {

    }
}
