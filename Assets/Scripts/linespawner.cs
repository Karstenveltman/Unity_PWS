using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linespawner : MonoBehaviour
{
    [SerializeField] Object bom;
    [SerializeField] Object bigbom;
    [SerializeField] Object clusterbom;
    [SerializeField] Object fastbom;
    [SerializeField] Object homingbom;
    [SerializeField] Camera mainCamera;
    float amount;
    float bigamount;
    float clusteramount;
    float fastamount;
    float homingamount;
    int level = 0;
    int extrabomb = 0;
    int newbombs = 10;
    void Start()
    {
       InvokeRepeating("spawning", 0f, 7f);
    }

    void place_bombs() {
        for(int i = 0; i < amount; i++){
            Instantiate(bom, new Vector3(Random.Range(-mainCamera.aspect * mainCamera.orthographicSize, mainCamera.aspect * mainCamera.orthographicSize), Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize)), Quaternion.identity);
        }
        for(int i = 0; i < bigamount; i++){
            Instantiate(bigbom, new Vector3(Random.Range(-mainCamera.aspect * mainCamera.orthographicSize, mainCamera.aspect * mainCamera.orthographicSize), Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize)), Quaternion.identity);
        }
        for(int i = 0; i < clusteramount; i++){
            Instantiate(clusterbom, new Vector3(Random.Range(-mainCamera.aspect * mainCamera.orthographicSize, mainCamera.aspect * mainCamera.orthographicSize), Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize)), Quaternion.identity);
        }
        for(int i = 0; i < fastamount; i++){
            Instantiate(fastbom, new Vector3(Random.Range(-mainCamera.aspect * mainCamera.orthographicSize, mainCamera.aspect * mainCamera.orthographicSize), Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize)), Quaternion.identity);
        }
        for(int i = 0; i < homingamount; i++){
            Instantiate(homingbom, new Vector3(Random.Range(-mainCamera.aspect * mainCamera.orthographicSize, mainCamera.aspect * mainCamera.orthographicSize), Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize)), Quaternion.identity);
        }
    }
    void random_bomb() {
        switch(Random.Range(1,5)) {
            case 1:
                bigamount++;
                break;
            case 2:
                fastamount++;
                break;
            case 3:
                homingamount++;
                break;
            case 4:
                clusteramount++;
                break;
        }
    }
    void spawning() {
        level++;
        Debug.Log("(beforeif) amount: " + amount + " extrabomb: " + extrabomb + " newbombs: " + newbombs + " level: " + level);
        if (newbombs == 10 && extrabomb <= 10) {
            if ((((extrabomb % 3) == 0) || (extrabomb == 4)) && (extrabomb != 0)) {
                random_bomb();
            }
            else {
                amount++;
            }
            Debug.Log("(jemoeder) amount: " + amount + " extrabomb: " + extrabomb + " newbombs: " + newbombs + " level: " + level);
        }
        else if (extrabomb >= newbombs) {
            if ((newbombs % 3) == 0) {
                random_bomb();
            }
            else {
                amount++;
            }
            extrabomb = 0;
            newbombs++;
            Debug.Log("(afterif) amount: " + amount + " extrabomb: " + extrabomb + " newbombs: " + newbombs + " level: " + level);
        }
        place_bombs();
        if (newbombs < 25 && newbombs > 10) {
            extrabomb++;
        }
        extrabomb++;
    }

    void Update()
    {

    }
}