using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBehaviour : MonoBehaviour
{

    public Text scoreText;

    private int score;
	
	// Update is called once per frame
	void Update () {
	    this.scoreText.text = this.score.ToString();
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ball")
        {
            this.score++;
        }
      
    }
}
