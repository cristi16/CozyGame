﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
	[SerializeField]
	List<Transform> targetList;

	[SerializeField] 
	float boundingBoxPadding = 2f;

	[SerializeField] 
	float zoomSpeed = 1.0f;

	float oSizeMin=20f;
	float oSizeMax=float.MaxValue;

	Camera camera;

	void Start(){
		camera = Camera.main;
	}

	public void LateUpdate()
	{
		Rect boundingBox = CalculateTargetsBoundingBox();
		transform.position = CalculateCameraPosition(boundingBox);
		camera.orthographicSize = CalculateOrthographicSize(boundingBox);
	}

	Rect CalculateTargetsBoundingBox(){

		float maxX = float.MinValue;
		float minX = float.MaxValue;
		float maxY = float.MinValue;
		float minY = float.MaxValue;

		foreach(Transform t in targetList)
		{			
			if (t.position.x < minX) minX = t.position.x;
			if (t.position.x > maxX) maxX = t.position.x;

			if (t.position.y < minY) minY = t.position.y;
			if (t.position.y > maxY) maxY = t.position.y;
		}

		return Rect.MinMaxRect(minX - boundingBoxPadding, maxY + boundingBoxPadding, maxX + boundingBoxPadding, minY - boundingBoxPadding);
	}

	Vector3 CalculateCameraPosition(Rect boundingBox)
	{
		Vector2 boundingBoxCenter = boundingBox.center;

		return new Vector3(boundingBoxCenter.x, boundingBoxCenter.y, camera.transform.position.z);
	}

	float CalculateOrthographicSize(Rect boundingBox)
	{
		float orthographicSize = camera.orthographicSize;
		Vector3 topRight = new Vector3(boundingBox.x + boundingBox.width, boundingBox.y, 0f);
		Vector3 topRightAsViewport = camera.WorldToViewportPoint(topRight);

		if (topRightAsViewport.x >= topRightAsViewport.y)
			orthographicSize = Mathf.Abs(boundingBox.width) / camera.aspect / 2f;
		else
			orthographicSize = Mathf.Abs(boundingBox.height) / 2f;

		return Mathf.Clamp(Mathf.Lerp(camera.orthographicSize, orthographicSize, Time.deltaTime * zoomSpeed), oSizeMin, oSizeMax);
	}		

}