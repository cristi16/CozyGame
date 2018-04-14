using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
	
	[SerializeField]
	CozyInputManager gameManager;

	[SerializeField] 
	float boundingBoxPadding = 2f;

	[SerializeField] 
	float zoomSpeed = 1.0f;

	float oSizeMin=20f;
	float oSizeMax=float.MaxValue;

	Rect boundingBox;

	public void LateUpdate()
	{
		if (gameManager.activePlayers == 0)
			return;
		
		boundingBox = CalculateTargetsBoundingBox();
		transform.position = CalculateCameraPosition(boundingBox);
		Camera.main.orthographicSize = CalculateOrthographicSize(boundingBox);
	}

	Rect CalculateTargetsBoundingBox(){

		float maxX = float.MinValue;
		float minX = float.MaxValue;
		float maxZ = float.MinValue;
		float minZ = float.MaxValue;

		foreach (PlayerCharacterController pc in gameManager.players) {
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

		return new Vector3(boundingBoxCenter.x, Camera.main.transform.position.y, boundingBoxCenter.y);
	}

	float CalculateOrthographicSize(Rect boundingBox)
	{
		float orthographicSize = Camera.main.orthographicSize;
		Vector3 topRight = new Vector3(boundingBox.x + boundingBox.width, 0f, boundingBox.y);
		Vector3 topRightAsViewport = Camera.main.WorldToViewportPoint(topRight);

		if (topRightAsViewport.x >= topRightAsViewport.y)
			orthographicSize = Mathf.Abs(boundingBox.width) / Camera.main.aspect / 2f;
		else
			orthographicSize = Mathf.Abs(boundingBox.height) / 2f;

		return Mathf.Clamp(Mathf.Lerp(Camera.main.orthographicSize, orthographicSize, Time.deltaTime * zoomSpeed), oSizeMin, oSizeMax);
	}		

}