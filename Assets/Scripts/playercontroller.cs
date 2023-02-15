using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playercontroller: MonoBehaviour {
    [SerializeField] float speed; 
    [SerializeField] Rigidbody2D rb;
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
    public healthchanger healthbar;

    void Update() {
        //allow the accelerometer to recalibrate
        if (Input.touchCount > 0) {
            corrected = false;
        }
    }

    void FixedUpdate (){
        //checks if still alive and if damaged
        if (hit == true && lives != 0 && invulnerable == false) {
            //to keep from losing more than 1 life at once and allow for knockback
            invulnerable = true;
            stunned = true;
            lives--;
            healthbar.lives = lives;
            Invoke("invulnerability", 3f);
            Invoke("revive", 1f);
        }
        if (stunned == false) {
            //if 0 lives, go to the game over screen
            if (lives <= 0) {
                stunned = true;
                SceneManager.LoadScene(3);
            }
            //get the input and change the velocity of the player based on it
            if (!keyboard) {
                if ((Input.acceleration != Vector3.zero) && (!corrected)){
                    CorrectionVector = new Vector3(Input.acceleration.x, Input.acceleration.y, 0);
                    corrected = true;
                }
                Vector3 acc = new Vector3(Input.acceleration.x, Input.acceleration.y, 0) - CorrectionVector;
                if (acc.magnitude >= deadZone) {
                    rb.velocity = acc * speed;
                }
                else {
                    rb.velocity = Vector3.zero;
                }
            }
            if (keyboard) {
                Vector3 acc = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
                rb.velocity = acc * speed;
            }
        }
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        xPos = Mathf.Clamp(xPos, (-mainCamera.aspect * mainCamera.orthographicSize), (mainCamera.aspect * mainCamera.orthographicSize));
        yPos = Mathf.Clamp(yPos, -mainCamera.orthographicSize, mainCamera.orthographicSize);
        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }
    
    void invulnerability() {
        invulnerable = false;
        hit = false;
    }
    void revive() {
        stunned = false;
    }
 }