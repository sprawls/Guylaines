using UnityEngine;
using System.Collections;

public class MenuCameraBehaviour : MonoBehaviour {

	public float _rotateSpeed;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Return)) {
			MainMenuBehaviour.Instance.EnterGame ();
		}

        if (Input.GetAxis("Fire1") != 0)
        {
            MainMenuBehaviour.Instance.EnterGame();
        }

		transform.Rotate (0, Time.deltaTime * _rotateSpeed, 0);
	}


}
