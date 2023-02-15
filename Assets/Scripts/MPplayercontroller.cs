using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MPplayercontroller: NetworkBehaviour {
    [SerializeField] float speed; 
    [SerializeField] float deadZone;
    [SerializeField] Camera mainCamera;
    private Vector3 CorrectionVector;
    private bool corrected = false;
    bool stunned = false;
    public int lives = 3;
    public bool hit = false;
    bool invulnerable = false;
    public bool keyboard;
    Vector3 acc = Vector3.zero;
    GameObject[] healthbars;
    Animator healthbar1;
    Animator healthbar2;
    GameObject[] players;
    Rigidbody2D rb1;
    Rigidbody2D rb2;
    GameObject joinCodeText;

    void Start() {
        healthbars = GameObject.FindGameObjectsWithTag("healthbar");
        healthbar1 = healthbars[0].GetComponent<Animator>();
        healthbar2 = healthbars[1].GetComponent<Animator>();
        joinCodeText = GameObject.FindGameObjectWithTag("joinCodeText");
    }
    void Update() {
        players = GameObject.FindGameObjectsWithTag("Player");
        //the host controls both players, so it shouls have both rbs
        if (IsHost) {
            rb1 = players[0].GetComponent<Rigidbody2D>();
            if (players.Length == 2) {
                rb2 = players[1].GetComponent<Rigidbody2D>();
                players[1].GetComponent<Animator>().SetInteger("playerID", 2);
                //remove the join code if player 2 joined
                Destroy(joinCodeText);
            }
        }
        //allow the accelerometer to recalibrate
        if (!IsOwner) return;
        if (Input.touchCount > 0) {
            corrected = false;
        }
    }

    void FixedUpdate (){
        //the game doesn't start until both players have joined
        if (players.Length == 2) {
            //checks if still alive and if damaged
            if (hit == true && lives != 0 && invulnerable == false) {
                //to keep from losing more than 1 life at once and allow for knockback
                invulnerable = true;
                stunned = true;
                lives--;
                if (IsOwner) {
                    healthbar1.SetInteger("hearts", lives);
                }
                else {
                    healthbar2.SetInteger("hearts", lives);
                } 
                Invoke("invulnerability", 3f);
                Invoke("revive", 1f);
            }
            if (stunned == false) {
                //if both players are dead, player 1 is dead or player 2 is dead, go to win screen
                if (players[0].GetComponent<MPplayercontroller>().lives <= 0 && players[1].GetComponent<MPplayercontroller>().lives <= 0) {
                    stunned = true;
                    SceneManager.LoadScene(4);
                    PlayerPrefs.SetInt("winner", 0);
                }
                else if (players[0].GetComponent<MPplayercontroller>().lives <= 0) {
                    stunned = true;
                    SceneManager.LoadScene(4);
                    PlayerPrefs.SetInt("winner", 1);
                }
                else if (players[1].GetComponent<MPplayercontroller>().lives <= 0) {
                    stunned = true;
                    SceneManager.LoadScene(4);
                    PlayerPrefs.SetInt("winner", 2);  
                }
                //get the input and send it to the movementserverrpc
                if (!keyboard) {
                    if ((Input.acceleration != Vector3.zero) && (!corrected)){
                        CorrectionVector = new Vector3(Input.acceleration.x, Input.acceleration.y, 0);
                        corrected = true;
                    }
                    Vector3 acc = new Vector3(Input.acceleration.x, Input.acceleration.y, 0) - CorrectionVector;
                    MovementServerRpc(acc);
                }
                if (keyboard) {
                    Vector3 acc = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
                    MovementServerRpc(acc);
                }
            }
        }
    }
    
    void invulnerability() {
        invulnerable = false;
        hit = false;
    }
    void revive() {
        stunned = false;
    }
    //moves both players from the host
    [ServerRpc(RequireOwnership = false)]
    private void MovementServerRpc(Vector3 acc, ServerRpcParams serverRpcParams = default) {
        if (!keyboard) {
            if (acc.magnitude >= deadZone) {
                //SenderClientId of the host is 0, of the client is 1
                if (serverRpcParams.Receive.SenderClientId == 0) {
                    rb1.velocity = acc * speed;
                }
                else {
                    rb2.velocity = acc * speed;
                }
            }
            else {
                if (serverRpcParams.Receive.SenderClientId == 0) {
                    rb1.velocity = Vector3.zero;
                }
                else {
                    rb2.velocity = Vector3.zero;
                }
            }
        }
        if (keyboard) {
            if (serverRpcParams.Receive.SenderClientId == 0) {
                rb1.velocity = acc * speed;
            }
            else {
                rb2.velocity = acc * speed;
            }    
        }    
        float xPos1 = rb1.transform.position.x;
        float yPos1 = rb1.transform.position.y;
        xPos1 = Mathf.Clamp(xPos1, (-mainCamera.aspect * mainCamera.orthographicSize), (mainCamera.aspect * mainCamera.orthographicSize));
        yPos1 = Mathf.Clamp(yPos1, -mainCamera.orthographicSize, mainCamera.orthographicSize);
        rb1.transform.position = new Vector3(xPos1, yPos1, rb1.transform.position.z);
        float xPos2 = rb2.transform.position.x;
        float yPos2 = rb2.transform.position.y;
        xPos2 = Mathf.Clamp(xPos2, (-mainCamera.aspect * mainCamera.orthographicSize), (mainCamera.aspect * mainCamera.orthographicSize));
        yPos2 = Mathf.Clamp(yPos2, -mainCamera.orthographicSize, mainCamera.orthographicSize);
        rb2.transform.position = new Vector3(xPos2, yPos2, rb2.transform.position.z);
    }
 }