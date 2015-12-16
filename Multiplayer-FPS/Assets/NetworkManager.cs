using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public GameObject standbyCamera;
	SpawnSpot[] spawnSpots;

	// Use this for initialization
	void Start () {
		spawnSpots = GameObject.FindObjectsOfType<SpawnSpot> ();
		Connect ();	
	}
	
	void Connect(){
		PhotonNetwork.ConnectUsingSettings ("MultiFPS v001");
	}

	void OnGUI(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
	}

	void OnJoinedLobby(){
		PhotonNetwork.JoinRandomRoom ();
		Debug.Log ("joining random room");
	}

	void OnPhotonRandomJoinFailed(){
		Debug.Log ("random room join failed");
		PhotonNetwork.CreateRoom (null);
	}

	void OnJoinedRoom(){
		Debug.Log ("onjoinedroom");
		SpawnMyPlayer ();
	}

	void SpawnMyPlayer(){
		if (spawnSpots == null) {
			Debug.LogError ("no spawns");
			return;
		}

		SpawnSpot mySpawnSpot = spawnSpots [Random.Range (0, spawnSpots.Length)];
		GameObject myPlayerGO = (GameObject) PhotonNetwork.Instantiate ("PlayerController", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
		standbyCamera.SetActive(false);

		((MonoBehaviour)myPlayerGO.GetComponent("FirstPersonController")).enabled = true;
		myPlayerGO.transform.FindChild ("FirstPersonCharacter").gameObject.SetActive(true);
	}
}
