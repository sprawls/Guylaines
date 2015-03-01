using UnityEngine;
using System.Collections;

public class LNFManager : MonoBehaviour {

	private AudioSource _audio;
	private int _pick;
	private bool _dropIsPlaying = false;

	public Material[] skyboxes;
	public AudioClip[] songs;
	public AudioClip[] drops;

	private Intro intro;
	
	void Awake () {
		_audio = GetComponent<AudioSource> ();
		_pick = Random.Range (0, skyboxes.Length);
		intro = (Intro) GameObject.FindGameObjectWithTag("intro").GetComponent<Intro>();

		SelectSkybox ();
		StartDrop ();
	}

	void Update() {
		if (!_audio.isPlaying) {
			StartSong();
		}
	}

	private bool dropHasFinished() {
		return (_dropIsPlaying && !_audio.isPlaying);
	}

	private void SelectSkybox() {
		RenderSettings.skybox = skyboxes [_pick];
	}

	private void StartDrop() {
		_audio.clip = drops [_pick];
		_audio.Play();
		_dropIsPlaying = true;


	}

	private void StartSong() {
		_audio.clip = songs [_pick];
		_audio.Play ();

		if(intro != null) intro.StartGame();
	}
}
