using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//While it's true that multiple inheritance is a sin,
//Pursuit really is just a subclass of Seek.
public class PursuitState : AbstractState {
	#region variables
	private int transitionID;
	
	private List<GameObject> objectsToSeek = new List<GameObject>();
	private int currentWaypoint = 0;
	#endregion
	
	#region methods
	public PursuitState(int id, AbstractControl controller) : base(id, controller)
	{ transitionID = ID; }
	
	public override void Enter() {
		Debug.Log ("Entering Pursuit State");
		name = "Pursuit";
		
		//Add all waypoints to the list
		foreach (GameObject gO in GameObject.FindGameObjectsWithTag("Waypoint")) {
			objectsToSeek.Add(gO);
		}
		
		objectsToSeek[currentWaypoint].renderer.material.color = Color.grey;
	}
	
	public override void Update() {
		//Switch through the current targets as F is pressed
		if (Input.GetKeyDown (KeyCode.T)) {
			objectsToSeek[currentWaypoint].renderer.material.color = Color.blue;
			currentWaypoint++;
			if (currentWaypoint >=objectsToSeek.Count) {
				currentWaypoint = 0;
			}
			objectsToSeek[currentWaypoint].renderer.material.color = Color.grey;
		}
		
		if (Input.GetKeyDown (KeyCode.F)) 
		{ transitionID = ID + 1; }
		else 
		{transitionID = ID; }
	}
	
	public override void FixedUpdate() {
		Vector3 destination;

		if (Controller.transform.position != objectsToSeek [currentWaypoint].transform.position) {
			if (objectsToSeek[currentWaypoint].rigidbody != null)
			{  destination = objectsToSeek[currentWaypoint].transform.position + objectsToSeek[currentWaypoint].rigidbody.velocity; }
			else
			{ destination = objectsToSeek[currentWaypoint].transform.position; }

			Controller.rigidbody.AddForce(destination -Controller.transform.position);
		}
		
	}
	
	public override void Exit() {
		Debug.Log ("Exiting Pursuit State");
	}	
	
	public override int CheckTransition(){
		return transitionID;
	}
	#endregion
}