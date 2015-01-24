using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour 
{
	public float xMargin = 1f;		// Distance in the x axis the player can move before the camera follows.
	public float yMargin = 1f;		// Distance in the y axis the player can move before the camera follows.
	public float xSmooth = 8f;		// How smoothly the camera catches up with it's target movement in the x axis.
	public float ySmooth = 8f;		// How smoothly the camera catches up with it's target movement in the y axis.
	public Vector2 maxXAndY;		// The maximum x and y coordinates the camera can have.
	public Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.

	private Vector3 trackPoint;		// Reference to the player's transform.
    private List<Transform> registeredPlayers;

    private static CameraFollow _instance;

    //public methods
    public static CameraFollow Get() {
        if (_instance == null) {
            _instance = Camera.main.GetComponent<CameraFollow>();
        }
        return _instance;
    }

    public void Register(Transform playerTransform) {
        registeredPlayers.Add(playerTransform);
    }

    //private methods
    private void Awake() {
        registeredPlayers = new List<Transform>();
        _instance = this;
    }

    private bool CheckXMargin() { return Mathf.Abs(transform.position.x - trackPoint.x) > xMargin; }
    private bool CheckYMargin() { return Mathf.Abs(transform.position.y - trackPoint.y) > yMargin; }

    private void UpdateTrackPoint() {
        Vector2 newPoint = Vector2.zero;
        foreach (Transform t in registeredPlayers) {
            newPoint += new Vector2(t.position.x, t.position.y);
        }
        newPoint /= registeredPlayers.Count;
        trackPoint = newPoint;
    }


	private void FixedUpdate () {
        if (registeredPlayers.Count > 0) {
            UpdateTrackPoint();
            TrackPoint();
        }
	}	
	
	private void TrackPoint ()
	{
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		// If the player has moved beyond the x margin...
		if(CheckXMargin())
			// ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
			targetX = Mathf.Lerp(transform.position.x, trackPoint.x, xSmooth * Time.deltaTime);

		// If the player has moved beyond the y margin...
		if(CheckYMargin())
			// ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
			targetY = Mathf.Lerp(transform.position.y, trackPoint.y, ySmooth * Time.deltaTime);

		// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
		targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
		targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}
