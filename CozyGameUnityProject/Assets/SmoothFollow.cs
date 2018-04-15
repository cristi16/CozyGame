using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
	
	[SerializeField] 
	float boundingBoxPadding = 2f;

	[SerializeField] 
	float zoomSpeed = 1.0f;

	[SerializeField] 
	float oSizeMin=5f;

	[SerializeField] 
	float oSizeMax=200f;

	[SerializeField]
	float oSizeRatio=2f;

	[SerializeField] 
	float fieldViewMin=5f;

	[SerializeField] 
	float fieldViewMax=200f;

	[SerializeField]
	float fieldViewRatio=1f;

	Rect boundingBox;

	public void LateUpdate()
	{
        var activePlayers = GameManager.Instance.GetActivePlayers();
        if (activePlayers.Count == 0)
            return;

		boundingBox = CalculateTargetsBoundingBox(activePlayers);
		transform.position = CalculateCameraPosition(boundingBox);

		if (Camera.main.orthographic)
			Camera.main.orthographicSize = CalculateOrthographicSize (boundingBox);
		else
			Camera.main.fieldOfView = CalculateFieldOfView (boundingBox);
	}

	Rect CalculateTargetsBoundingBox(List<PlayerCharacterController> activePlayers)
    { 
		float maxX = float.MinValue;
		float minX = float.MaxValue;
		float maxZ = float.MinValue;
		float minZ = float.MaxValue;

		foreach (PlayerCharacterController pc in activePlayers) {
			if (pc.gameObject.activeSelf) {
				Transform t = pc.transform;
				if (t.position.x < minX) minX = t.position.x;
				if (t.position.x > maxX) maxX = t.position.x;

				if (t.position.z < minZ) minZ = t.position.z;
				if (t.position.z > maxZ) maxZ = t.position.z;
			}
		}

		return Rect.MinMaxRect(minX - boundingBoxPadding, maxZ + boundingBoxPadding, maxX + boundingBoxPadding, minZ - boundingBoxPadding);

	}

	Vector3 CalculateCameraPosition(Rect boundingBox)
	{
		Vector2 boundingBoxCenter = boundingBox.center;
		if (Camera.main.orthographic) {
			return new Vector3 (boundingBoxCenter.x, Camera.main.transform.position.y, boundingBoxCenter.y);
		} else {
			return new Vector3 (boundingBoxCenter.x, Camera.main.transform.position.y, boundingBoxCenter.y);
		}
	}

	float CalculateOrthographicSize(Rect boundingBox)
	{
		float orthographicSize = Camera.main.orthographicSize;
		Vector3 topRight = new Vector3(boundingBox.x + boundingBox.width, 0f, boundingBox.y);
		Vector3 topRightAsViewport = Camera.main.WorldToViewportPoint(topRight);

		if (topRightAsViewport.x >= topRightAsViewport.y)
			orthographicSize = Mathf.Abs(boundingBox.width) / Camera.main.aspect / oSizeRatio;
		else
			orthographicSize = Mathf.Abs(boundingBox.height) / oSizeRatio;

		return Mathf.Clamp(Mathf.Lerp(Camera.main.orthographicSize, orthographicSize, Time.deltaTime * zoomSpeed), oSizeMin, oSizeMax);
	}

	float CalculateFieldOfView(Rect boundingBox)
	{
		float fieldOfView = Camera.main.fieldOfView;
		Vector3 topRight = new Vector3(boundingBox.x + boundingBox.width, 0f, boundingBox.y);
		Vector3 topRightAsViewport = Camera.main.WorldToViewportPoint(topRight);

		if (topRightAsViewport.x >= topRightAsViewport.y)
			fieldOfView = Mathf.Abs(boundingBox.width) / Camera.main.aspect / fieldViewRatio;
		else
			fieldOfView = Mathf.Abs(boundingBox.height) / fieldViewRatio;

		return Mathf.Clamp(Mathf.Lerp(Camera.main.fieldOfView, fieldOfView, Time.deltaTime * zoomSpeed), fieldViewMin, fieldViewMax);
	}

}