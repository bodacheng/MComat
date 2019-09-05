using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject Effect;
	public GameObject Good;
	public GameObject Great;
	public GameObject LevelUp;

	public GameObject player1;
	public GameObject player2;

	public int count = 0;

	void Start(){
		Good.transform.localScale = new Vector3 (0.25f, 0.25f, 0.25f);
		Great.transform.localScale = new Vector3 (0.25f, 0.25f, 0.25f);
		LevelUp.transform.localScale = new Vector3 (0.25f, 0.25f, 0.25f);
		count = 0;
	}

	void Update () {
		if (Input.GetKey ("right")) {
			transform.position += transform.right * 0.02f;
		}

		if (Input.GetKey ("left")) {
			transform.position -= transform.right * 0.02f;
		}

		if (Input.GetKeyDown ("space")) {
			Instantiate (Effect, this.transform.position, Quaternion.identity);
			player1.active = false;
			player2.active = true;
		}
		if (Input.GetKeyUp ("space")) {
			player1.active = true;
			player2.active = false;
		}

	}

	void HitEnemy(){
		count++;
		switch(count)
		{
		case 1:
			MakeGood ();
			break;
		case 2:
			MakeGreat ();
			break;
		case 3:
			MakeLevelUp ();
			count = 0;
			break;
		}
	}

	void MakeLevelUp(){
		Instantiate (LevelUp, (this.transform.position + new Vector3(0,1,0)), Quaternion.identity);
	}
	void MakeGood(){
		Instantiate (Good, (this.transform.position + new Vector3(0,1,0)), Quaternion.identity);
	}
	void MakeGreat(){
		Instantiate (Great, (this.transform.position + new Vector3(0,1,0)), Quaternion.identity);
	}


}
