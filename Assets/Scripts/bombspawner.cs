using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bombspawner : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI waveCounter;
    [SerializeField] Object normalBomb;
    [SerializeField] Object bigBomb;
    [SerializeField] Object clusterBomb;
    [SerializeField] Object fastBomb;
    [SerializeField] Object homingBomb;
    [SerializeField] Camera mainCamera;
    float normalAmount;
    float bigAmount;
    float clusterAmount;
    float fastAmount;
    float homingAmount;
    public int level = 0;
    int extraBomb = 0;
    int newBombs = 10;
    void Start()
    {
       InvokeRepeating("spawning", 0f, 7f);
    }

    void Place_bombs() {
        for(int i = 0; i < normalAmount; i++){
            Instantiate(normalBomb, new Vector3(Random.Range(-mainCamera.aspect * mainCamera.orthographicSize, mainCamera.aspect * mainCamera.orthographicSize), Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize)), Quaternion.identity);
        }
        for(int i = 0; i < bigAmount; i++){
            Instantiate(bigBomb, new Vector3(Random.Range(-mainCamera.aspect * mainCamera.orthographicSize, mainCamera.aspect * mainCamera.orthographicSize), Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize)), Quaternion.identity);
        }
        for(int i = 0; i < clusterAmount; i++){
            Instantiate(clusterBomb, new Vector3(Random.Range(-mainCamera.aspect * mainCamera.orthographicSize, mainCamera.aspect * mainCamera.orthographicSize), Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize)), Quaternion.identity);
        }
        for(int i = 0; i < fastAmount; i++){
            Instantiate(fastBomb, new Vector3(Random.Range(-mainCamera.aspect * mainCamera.orthographicSize, mainCamera.aspect * mainCamera.orthographicSize), Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize)), Quaternion.identity);
        }
        for(int i = 0; i < homingAmount; i++){
            Instantiate(homingBomb, new Vector3(Random.Range(-mainCamera.aspect * mainCamera.orthographicSize, mainCamera.aspect * mainCamera.orthographicSize), Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize)), Quaternion.identity);
        }
    }
    void Random_bomb() {
        switch(Random.Range(1,5)) {
            case 1:
                bigAmount++;
                break;
            case 2:
                fastAmount++;
                break;
            case 3:
                homingAmount++;
                break;
            case 4:
                clusterAmount++;
                break;
        }
    }
    void spawning() {
        level++;
        waveCounter.text = level.ToString();
        if (newBombs == 10 && extraBomb <= 10) {
            if ((((extraBomb % 3) == 0) || (extraBomb == 4)) && (extraBomb != 0)) {
                Random_bomb();
            }
            else {
                normalAmount++;
            }
        }
        else if (extraBomb >= newBombs) {
            if ((newBombs % 3) == 0) {
                Random_bomb();
            }
            else {
                normalAmount++;
            }
            extraBomb = 0;
            newBombs++;
        }
        Place_bombs();
        if (newBombs < 25 && newBombs > 10) {
            extraBomb++;
        }
        extraBomb++;
    }

}