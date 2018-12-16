using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Defeat : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI wavesSurvived;
    private int waveCount;
	
	void Update () {
        waveCount = FindObjectOfType<Spawner>().totalWaves;
        wavesSurvived.text = "You lost at wave " + waveCount.ToString();
	}

}
