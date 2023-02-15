using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class healthchanger : MonoBehaviour
{
    SpriteRenderer healthbar;
    public Sprite twoLeft;
    public Sprite oneLeft;
    public Sprite noneLeft;
    public int lives = 3;
    void Start()
    {
        healthbar = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //changes sprite of the healthbar in singleplayer. In multiplayer it is done with an animation to let it work through the network
        switch(lives) {
            case 2:
                healthbar.sprite = twoLeft;
                break;
            case 1:
                healthbar.sprite = oneLeft;
                break;
            case 0:
                healthbar.sprite = noneLeft;
                break;
            default:
                break;
        }
    }
}
