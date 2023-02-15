using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bombspawner : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI waveCounter;
    [SerializeField] Object normalBomb;
    [SerializeField] Object bigBomb;
    float bigProbability = 0.1f;
    [SerializeField] Object fastBomb;
    float fastProbability = 0.25f;
    [SerializeField] Object homingBomb;
    float homingProbability = 0.3f;
    [SerializeField] Object clusterBomb;
    float clusterProbability = 0.4f;
    [SerializeField] Camera mainCamera;
    float[] probabilities;
    // Dictionary<string, float> probabilities = new Dictionary<string, float>(){
    //     {"bigBomb", 0.1f},
    //     {"fastBomb", 0.25f},
    //     {"homingBomb", 0.3f},
    //     {"clusterBomb", 0.4f}
    // };

    float normalAmount;
    float bigAmount;
    float clusterAmount;
    float fastAmount;
    float homingAmount;
    int amount;
    public int level = 0;
    int extraBomb = 0;
    int newBombs = 10;
    GameObject[] players;
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        //decides probability of certain bombtypes spawning
        probabilities = new float[] {bigProbability, fastProbability, homingProbability, clusterProbability};
        InvokeRepeating("spawning", 0f, 7f);
    }

    void Place_bombs() {
        Vector3 lastCoords = Vector3.zero;
        //spawns 'amount' bombs
        for (int i = 0; i < amount; i++) {
            float rand = Random.value;
            //picks coordinates for the bomb to spawn anywhere inside the screen and not in the player
            Vector3 bombCoords = new Vector3(Random.Range(-mainCamera.aspect * mainCamera.orthographicSize, mainCamera.aspect * mainCamera.orthographicSize), Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize));
            foreach (GameObject player in players) {
                Vector3 bottomLeft = player.transform.position - new Vector3(0.19f, 0.16f, 0f);
                Vector3 topRight = player.transform.position + new Vector3(0.16f, 0.19f, 0f);
                while (bombCoords.x >= bottomLeft.x && bombCoords.x <= topRight.x && bombCoords.y >= bottomLeft.y && bombCoords.y <= topRight.y) { //if bombscoords is in player
                    bombCoords = new Vector3(Random.Range(-mainCamera.aspect * mainCamera.orthographicSize, mainCamera.aspect * mainCamera.orthographicSize), Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize));
                }
            }
            //picks which bomb to spawn and spawns it based on the random generated number of rand
            if (rand >= 0f && rand < bigProbability) {
                Instantiate(bigBomb, bombCoords, Quaternion.identity);
            }
            else if (rand >= bigProbability && rand < fastProbability) {
                //makes sure the fast bomb spawns close to other bombs
                if (lastCoords != Vector3.zero) {
                    Vector2 uitwijking = Random.insideUnitCircle.normalized * 0.5f;
                    Instantiate(fastBomb, lastCoords + new Vector3(uitwijking.x, uitwijking.y, 0f), Quaternion.identity);    
                }
                else {
                    Instantiate(fastBomb, bombCoords, Quaternion.identity);
                }
            }
            else if (rand >= fastProbability && rand < homingProbability) {
                Instantiate(homingBomb, bombCoords, Quaternion.identity);
            }
            else if (rand >= 0.3f && rand < clusterProbability) {
                Instantiate(clusterBomb, bombCoords, Quaternion.identity);
            }
            else {
                Instantiate(normalBomb, bombCoords, Quaternion.identity);
            }
            lastCoords = bombCoords;
        }
    }
    //function to decide how many bombs spawn and to update the levelcounter
    void spawning() {
        level++;
        waveCounter.text = "Wave: " + level.ToString();
        PlayerPrefs.SetInt("score", level);
        if (level == 15) {
            increaseProbability(bigProbability, 0.05f);
            increaseProbability(fastProbability, 0.05f);
            increaseProbability(homingProbability, 0.05f);
            increaseProbability(clusterProbability, 0.05f);
        }
        if (newBombs == 10 && extraBomb <= 10) {
            amount++;
        }
        else if (extraBomb >= newBombs) {
            extraBomb = 0;
            newBombs++;
            amount++;
        }
        Place_bombs();
        if (newBombs < 25 && newBombs > 10) {
            extraBomb++;
        }
        extraBomb++;
    }
    //makes it more likely for a certain type of bomb to spawn
    void increaseProbability(float chosenProbability, float increase) {
        for(int i = 0; i < probabilities.Length; i++) {
            if (probabilities[i] > chosenProbability) {
                probabilities[i] += increase;
                Debug.Log(probabilities[i]);
            }
        }
        chosenProbability += increase;
    }

}