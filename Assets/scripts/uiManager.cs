using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiManager : MonoBehaviour {

	public CarBehavoir vehicle;
	public Text fpsText;
	public Text speedText;
	public Text angleText;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

		int fps = (int)(1 / Time.deltaTime);
		int speed = (int)((vehicle != null ? vehicle.Speed : 0) * fps);
		float angle = (float)((vehicle != null ? vehicle.TurnAngle : 0) * fps);

		//Debug.Log (vehicle.WheelAngle);

		fpsText.text = "Fps: " + fps;
		speedText.text = "Speed: " + speed;
		angleText.text = "Angle: " + angle.ToString("0.000") + " R";
	}

	public void Pause(){
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
	}
}
