using UnityEngine;
using System.Collections;

public class ShipControl : MonoBehaviour {


	////////////////////////// General //////////////////////////
	[Header("Component")]
	public Transform modelAndCam;
	public Transform model;
	public MoveCameraFromSpeed cameraScript;
	public LNFManager LNF;
	public bool isDead = false;

	////////////////////////// Forward Speed //////////////////////////
	[Header("Forward Speed")]
	public float forwardSpeed;

    private float speedIncrementPerLevel = 0.01f;
	private float speedIncrementPerSecond = 0.0f;
	private float startSpeed = 2.0f;
	[HideInInspector] public float additionalSpeed = 0f;

	////////////////////////// Side Speed //////////////////////////
	[Header("Side Speed")]
	public float sideSpeed; //lerp from 0 to maxSideSpeed using curSideSpeedMultiplier as "t"
	public float tiltAngle;
	public float control; //change in sideSpeed per second( 1 means it takes 1sec to reach max sidespeed; 2 means it takes 0.5sec)
	public float stabilizingControlratio = 0.5f; //When no key is pressed, ratio of the control the ship uses to stabilize
	public float sideSpeedLimit = 0.5f;
	public float maxTiltAngle = 5;
	private float curSideSpeedMultiplier = 0; //Variable between 0 and 1 dictating speed
	private float deadTiltZone = 0.025f; // if sidespeed is lower than this, it equals 0
    public bool slowMoActive = false;

	////////////////////////// Death //////////////////////////
	[Header("Death")]
	[HideInInspector] public bool slowMoEnded = false;
	private RotatingPlatform rotatePlatform;
	private Translation translation;

	////////////////////////// SUPER TILT //////////////////////////
	[Header("SUPER TILT")]
	public float tiltCooldown = 0.5f;
	public float tiltTime = 1.5f;
	private float tiltTimeAnimation = 0.3f;
	public GameObject[] objectsToSuperTilt;
	private bool isSuperTilting = false;
	private bool isCurrentlyTilted = false;

	////////////////////////// BArrel Roll  //////////////////////////
	[Header("Barrel Roll")]
	public float barrelRollCooldown = 1f;
	public float BarrelRollSideSpeed = 2f;
	private float OverriddenSideSpeed;
	public float barrelRollTimeAnimation = 0.8f;
	private bool barrelRollOnCD = false;
	private bool isBarrelRollin = false;
    
	////////////////////////// ALEX stuff //////////////////////////
	[Header("Alex Stuff")] // Really ? -_-
	private const float bulletTimeDivisor = 1.25f;
    private const int slowSmothness = 15;

    private float old_speedIncrementPerLevel;
    private float old_startSpeed;
    private float old_sideSpeedLimit;
    public bool itemChoseLock = true;

	////////////////////////// Special POWER //////////////////////////
	[Header("Special Power")]
	public GameObject SpecialPowerObj;
	private SpecialPower specialPower;
	public float specialPowerCooldown = 3f;
	private bool specialPowerOnCooldown = false;

	////////////////////////// SHAKE SHAKE SHAKE //////////////////////////
	[Header("SHAKE SHAKE SHAKE")]
	public ShakeShakeShake shakeshakeshake;
	public float speedToConstantShake = 3f;
	public float maxShakeAmount = 0.5f;
	public float speedForMaxShake = 12f;

	////////////////////////// Inputs //////////////////////////
	public bool Input_Left = false;
	public bool Input_Right = false;
	public bool Input_Special = false;
	public bool Input_LeftRoll_OneTime = false;
	public bool Input_RightRoll_OneTime = false;
    public bool Input_Shoot = false;

	[HideInInspector] public float SpeedDeltaTimeAdjuster = 40f;

	////////////////////////// CANNON //////////////////////////
	public Player_BossTurret bossTurret;
	private bool cannonIsOn = false;
	
	void Awake() {
		rotatePlatform = (RotatingPlatform) gameObject.GetComponentInChildren<RotatingPlatform>();
		translation = (Translation) gameObject.GetComponentInChildren<Translation>();
		shakeshakeshake = (ShakeShakeShake) gameObject.GetComponentInChildren<ShakeShakeShake>();
		LNF = (LNFManager) GameObject.FindGameObjectWithTag("LNF").GetComponent<LNFManager>();
	}
	void Start () {
		forwardSpeed = startSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		//Calculate Forward Speed
		CalculateForwardSpeed ();
		//Calculate Side Speed
		CalculateSideSpeed ();


		if (!isDead && !slowMoActive) {
			//Check Super Power
			CheckSpecialPower ();
			//Check Barrel Roll
			CheckBarrelRoll ();
            //Check Shoot
            CheckShoot();
		}
		//Move & Tilt
		MoveShip ();
		//Add SHAKESHAKESHAKE on speed
		CheckShake ();

		//DEBUG CANNON BOSS
        if (Input.GetKeyDown(KeyCode.Z)) {
            Debug.Log(bossTurret.canChangeActivation);
        }
		if (Input.GetKeyDown (KeyCode.Z) && bossTurret.canChangeActivation) {
			if(cannonIsOn) {
				DeactivateTurretMode();
			} else {
				ActivateTurretMode();	
			}
		}
	}

	public void ActivateTurretMode(){

		bossTurret.gameObject.SetActive (true);
		bossTurret.Activate ();
        cannonIsOn = true;
        
	}

	public void DeactivateTurretMode() {
       
		bossTurret.Deactivate ();
		//bossTurret.gameObject.SetActive (false);
        cannonIsOn = false;
       
	}

	void MoveShip() {
		if(isDead == false) { //Move the ship based on its rotation
			float newZ = transform.rotation.eulerAngles.z;
			Vector3 horizontalVectorSpeed;
			if(isBarrelRollin) {
				//horizontalVectorSpeed = new Vector3(OverriddenSideSpeed,0,0);
				sideSpeed = OverriddenSideSpeed;
			}
			horizontalVectorSpeed = new Vector3(sideSpeed,0,0);
			horizontalVectorSpeed = Quaternion.AngleAxis(newZ,transform.forward) * horizontalVectorSpeed;
			
			transform.position += horizontalVectorSpeed * Time.deltaTime * SpeedDeltaTimeAdjuster; //Move the ship horizontally
			transform.position += new Vector3(0,0,forwardSpeed) * Time.deltaTime * SpeedDeltaTimeAdjuster; //Move the ship forward
			TiltShip(); //Tilt the ship
		}
	}

    public float getForwardSpeed() {
        return (startSpeed + additionalSpeed);
    }

	void CalculateForwardSpeed() {
		//forwardSpeed = startSpeed + (speedIncrementPerLevel* StatManager.Instance.Speed.Level); //Removed since rework
        forwardSpeed = startSpeed;
		forwardSpeed += additionalSpeed;
	}

	void CalculateSideSpeed() {
		if (!slowMoActive && !isBarrelRollin) {
			//UpdateControlStat();
			if ((Input_Left || Input_Right) && !(Input_Left && Input_Right) ) {
				if(Input_Left) ChangeSideSpeed(-1);
				else if(Input_Right) ChangeSideSpeed(1);
			}
			else {
				StabilizeSideSpeed();
			}
				
		}
	}

	void CheckBarrelRoll() {
		if( Input_RightRoll_OneTime && isSuperTilting == false && barrelRollOnCD == false && isDead == false&& !slowMoActive) {
			Debug.Log ("Super Barrel Roll Activated Right");
			isBarrelRollin = true;
			barrelRollOnCD = true;
			StartCoroutine (SuperBarrelRollPlanner(false));
		}
		if( Input_LeftRoll_OneTime && isSuperTilting == false && barrelRollOnCD == false && isDead == false&& !slowMoActive) {
			Debug.Log ("Super Barrel Roll Activated Left");
			isBarrelRollin = true;
			barrelRollOnCD = true;
			StartCoroutine (SuperBarrelRollPlanner(true));
		}
	}

	void CheckSpecialPower() {
		//OLD_SuperPower(); //old super power before refactor (infinite vertical tilt)
		if(Input_Special && !specialPowerOnCooldown && !isDead && !slowMoActive) {
			Debug.Log ("Super Power");
			specialPowerOnCooldown = true;
			StartCoroutine(StartSpecialPowerCooldown ());
			specialPower = (Instantiate(SpecialPowerObj)).GetComponent<SpecialPower> ();
			specialPower.Activate(this);
		}

	}

    void CheckShoot() {
        if (Input_Shoot && cannonIsOn) {
            bossTurret.Shoot();
        }
    }

	/*
	void OLD_SuperPower() {
		if(Input.GetAxis("Fire1") != 0 && isSuperTilting == false && isDead == false && !slowMoActive) {
			Debug.Log ("SuperTilt Activated");
			isSuperTilting = true;
			StartCoroutine ("SuperTiltPlanner");
		}
		if(Input.GetAxis("Fire1") == 0 && isCurrentlyTilted == true && isSuperTilting == true && isDead == false&& !slowMoActive) {
			Debug.Log ("SuperTilt Deactivated");
			isCurrentlyTilted = false;
			StopCoroutine ("SuperTiltPlanner");
			StopCoroutine(SuperTilt(new Vector3(0,0,0), new Vector3(0,0,90),tiltTimeAnimation));
			StartCoroutine (SuperTiltUnplanner());
		}
	}
	*/

	void CheckShake() {
		if(forwardSpeed > speedToConstantShake) {
			shakeshakeshake.noTimer = true;
			float step = (forwardSpeed-speedToConstantShake)/(speedForMaxShake-speedToConstantShake);
			step = Mathf.Clamp(step,0f,1f);
			shakeshakeshake.SetShake(1,Mathf.Lerp (0,maxShakeAmount,step));
			}
	}

	void UpdateControlStat() {
		//if(StatManager.Instance.Speed.Level != 0) control = (StatManager.Instance.Handling.Level / StatManager.Instance.Speed.Level);
		//control = Mathf.Clamp(control,1f,3f);
		//Remove control by stat for test
		//control = 2f;
		//Change side Speed limit
		float LevelBonusSideSpeed = Mathf.Lerp (0f,8f,StatManager.Instance.Handling.Level/10000f);

        
        if (!slowMoActive)
        {
            sideSpeedLimit = 0.5f + LevelBonusSideSpeed;

        }

		
	}


	void StabilizeSideSpeed() { //Stabilize the ship when no key is pressed
		int axis;
		if(sideSpeed > 0) axis = -1;
		else axis = 1;

		if(Mathf.Abs(sideSpeed) <= deadTiltZone) {
			sideSpeed = 0;
			curSideSpeedMultiplier = 0;
		} else {
		

			curSideSpeedMultiplier += Mathf.Sign(axis) * (stabilizingControlratio*control*Time.deltaTime);
			curSideSpeedMultiplier = Mathf.Clamp(curSideSpeedMultiplier,-1,1);

			sideSpeed = Mathf.Lerp(0, sideSpeedLimit, Mathf.Abs (curSideSpeedMultiplier));
			sideSpeed *= Mathf.Sign(curSideSpeedMultiplier);

		}

		////Debug.Log (curSideSpeedMultiplier);
	}

	void ChangeSideSpeed(float axis) { //Change side speed when a key is pressed
		////Debug.Log ("side Sped");
		curSideSpeedMultiplier += Mathf.Sign(axis) * (control*Time.deltaTime);
		curSideSpeedMultiplier = Mathf.Clamp(curSideSpeedMultiplier,-1,1);

		sideSpeed = Mathf.Lerp(0, sideSpeedLimit, Mathf.Abs (curSideSpeedMultiplier));
		sideSpeed *= Mathf.Sign(curSideSpeedMultiplier);

	}

	void TiltShip() {
		//Calculate New Tilt
		float speedMultiplier = Mathf.Abs(sideSpeed)/sideSpeedLimit;
		float newTilt = Mathf.Lerp (0,maxTiltAngle,speedMultiplier);
		newTilt *= Mathf.Sign(-sideSpeed);

		//Apply new Tilt
		modelAndCam.localRotation = Quaternion.Euler(new Vector3(0,0,newTilt));

	}


	public void Kill(){
        if (!isDead)
        {
            isDead = true;
			RunEndUIBehaviour.Instance.OnRunEnd();
			ScoreUIBehaviour.Instance.TweenRunsOnRunEnd();
            StartCoroutine(DeathAnimation());
            Destroy(model.gameObject);
        }
	}

	public void Spawn(){
		isDead = false;
	}

	public IEnumerator DeathAnimation() {
		//move camera
		rotatePlatform.ManuallyRotate(-75,2f,1);
		translation.Activate();

		yield return new WaitForSeconds(0.05f);
		Time.timeScale = 0.05f;
		LNF.GetComponent<AudioSource>().pitch = 0.5f;
		StartCoroutine(fadeOutSong());
		yield return new WaitForSeconds(1.5f*(Time.timeScale)); // 
		Time.timeScale = 1f;
		//LNF.audio.pitch = 1f;
		slowMoEnded = true;

		// Reload if have runs left
		if (StatManager.Instance.HaveRunsLeft) {
			yield return new WaitForSeconds (5f);
			Application.LoadLevel (Application.loadedLevel);
		}
	}

    public void StartBullteTime(float time)
    {
        if (!slowMoActive)
        {
            StartCoroutine(bulletTime(time));
        }
    }

    private IEnumerator bulletTime(float time)
    {
        yield return new WaitForSeconds(0.2f);
        slowMoActive = true;
        
        old_speedIncrementPerLevel = speedIncrementPerLevel;
        old_startSpeed = startSpeed;
        old_sideSpeedLimit = sideSpeedLimit;
        for (int i = 1; i < slowSmothness; i++)
        {

            Time.timeScale /= (bulletTimeDivisor);
            LNF.GetComponent<AudioSource>().pitch = Mathf.Min(1, Time.timeScale*2);
            speedIncrementPerLevel /= (bulletTimeDivisor);
            startSpeed /= (bulletTimeDivisor);
            sideSpeedLimit /= (bulletTimeDivisor);
            ChangeSideSpeed(0);
            if (i == slowSmothness / 2)
            {
                itemChoseLock = false;
            }
            yield return new WaitForSeconds(0.0075f);
        }
        StartCoroutine(stopBulletTime(old_speedIncrementPerLevel, old_startSpeed, old_sideSpeedLimit,time));
    }


    public void callStopBullteTime(float time)
    {
        if (slowMoActive)
        {
            StartCoroutine(stopBulletTime(old_speedIncrementPerLevel, old_startSpeed, old_sideSpeedLimit, time));
        }
    }

    private IEnumerator stopBulletTime(float old_speedIncrementPerLevel, float old_startSpeed, float old_sideSpeedLimit, float durr)
    {

            yield return new WaitForSeconds(durr * Time.timeScale);
            for (int i = 1; i < slowSmothness / 2 && slowMoActive; i++)
            {
                Time.timeScale *= (bulletTimeDivisor);
                LNF.GetComponent<AudioSource>().pitch = Mathf.Min(1, Time.timeScale * 2);
                speedIncrementPerLevel *= (bulletTimeDivisor);
                startSpeed *= (bulletTimeDivisor);
                sideSpeedLimit *= (bulletTimeDivisor);
                ChangeSideSpeed(0);
                if (i == 4)
                {
                    ItemUIBehaviour.Instance.outOtimeAutoPick();
                }
                yield return new WaitForSeconds(0.0005f);
            }

            speedIncrementPerLevel = old_speedIncrementPerLevel;
            startSpeed = old_startSpeed;
            sideSpeedLimit = old_sideSpeedLimit;
            ChangeSideSpeed(0);
            Time.timeScale = 1f;
            LNF.GetComponent<AudioSource>().pitch = Time.timeScale;
            slowMoActive = false;
            itemChoseLock = true;
            
   
    }

	public void BarrelRoll(float degrees, float time) {
		if(isSuperTilting == false) {
			isSuperTilting = true;
			StartCoroutine (SuperTilt(new Vector3(0,0,0), new Vector3(0,0,degrees),time));
			StartCoroutine(GiveBackSuperTilt(time));
		}
	}

	private IEnumerator StartSpecialPowerCooldown() {
		specialPowerOnCooldown = true;
		yield return new WaitForSeconds (specialPowerCooldown);
		specialPowerOnCooldown = false;
	}

	private IEnumerator GiveBackSuperTilt(float time) {
		yield return new WaitForSeconds(time);
		isSuperTilting = false;
	}

	private IEnumerator SuperBarrelRollPlanner(bool isLeft) {
		if (isLeft) {
			StartCoroutine (SuperTilt (new Vector3 (0, 0, 0), new Vector3 (0, 0, 720), barrelRollTimeAnimation));
			OverriddenSideSpeed = -BarrelRollSideSpeed;
			curSideSpeedMultiplier = -1;
		} else {
			StartCoroutine (SuperTilt (new Vector3 (0, 0, 0), new Vector3 (0, 0, -720), barrelRollTimeAnimation));
			OverriddenSideSpeed = BarrelRollSideSpeed;
			curSideSpeedMultiplier = 1;
		}
		yield return new WaitForSeconds(barrelRollTimeAnimation);
		isBarrelRollin = false;
		yield return new WaitForSeconds(barrelRollCooldown);
		barrelRollOnCD = false;
	}

	private IEnumerator SuperTiltPlanner() {
		//START ANIMATION
		StartCoroutine (SuperTilt(new Vector3(0,0,0), new Vector3(0,0,90),tiltTimeAnimation));
		yield return new WaitForSeconds(tiltTimeAnimation);
		//TILTED TIME
		isCurrentlyTilted = true;
		while(true) {
			foreach (GameObject g in objectsToSuperTilt) {
				if(g!= null) g.transform.localEulerAngles = new Vector3(0,0,90);
			}
			yield return null;
		}


	}

	private IEnumerator SuperTiltUnplanner() {
		//END ANIMATION
		//yield return new WaitForSeconds(tiltTime);
		StartCoroutine (SuperTilt(new Vector3(0,0,objectsToSuperTilt[0].transform.rotation.eulerAngles.z), new Vector3(0,0,0),tiltTimeAnimation));
		yield return new WaitForSeconds(tiltTimeAnimation);
		//COOLDOWN
		isSuperTilting = false;
		isCurrentlyTilted = false;
	}

	private IEnumerator fadeOutSong() {
		for(float i=1; i>=0; i-= Time.deltaTime/4f) {
			LNF.GetComponent<AudioSource>().volume = i;
			yield return null;
		}
	}

	private IEnumerator SuperTilt(Vector3 startingRotation, Vector3 endRotation, float time) {
		float step = 0f; //raw step
		float rate = 1f/time; //amount to add to raw step
		float smoothStep = 0f; //current smooth step
		float lastStep = 0f; //previous smooth step
		while(step < 1f) { // until we're done
			step += Time.deltaTime * rate; 
			smoothStep = Mathf.SmoothStep(0f, 1f, step); // finding smooth step
			foreach (GameObject g in objectsToSuperTilt) {
				//if(g!= null) g.transform.localRotation = Quaternion.Euler(Vector3.Lerp(startingRotation, endRotation, (smoothStep)));
				if(g!= null) g.transform.localEulerAngles = Vector3.Lerp(startingRotation, endRotation, (smoothStep));

			}
			lastStep = smoothStep; //get previous last step
			yield return null;
		}
		if(step > 1.0) {
			foreach (GameObject g in objectsToSuperTilt) {
				//if(g!= null) g.transform.localRotation = Quaternion.Euler(Vector3.Lerp(startingRotation, endRotation, 1f));
				if(g!= null) g.transform.localEulerAngles = Vector3.Lerp(startingRotation, endRotation, 1);
			}
		}

	}


}
