using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class laatmijfpszien : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fpscounter;
    private float pollingTime = 1f;
    private float time;
    private int frameCount;
    void Update() {
        time += Time.deltaTime;
        frameCount ++;
        if (time >= pollingTime) {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            fpscounter.text = "FPS: " + frameRate.ToString();

            time -= pollingTime;
            frameCount = 0;
        }
    }
}
