using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public List<Transform> targetList;


    float oSizeMin=20f;
	float oSizeMax=float.MaxValue;

    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    public void Update()
    {
        Vector3 center = Vector3.zero;

        float maxX = float.MinValue;
        float minX = float.MaxValue;
        float maxY = float.MinValue;
        float minY = float.MaxValue;

        foreach(Transform t in targetList)
        {
            center += t.position;

            if (t.position.x < minX) minX = t.position.x;
            if (t.position.x > maxX) maxX = t.position.x;

            if (t.position.y < minY) minY = t.position.y;
            if (t.position.y > maxY) maxY = t.position.y;
        }

        if (targetList.Count > 0) {
            center /= targetList.Count;
        }

        center.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, center, ref velocity, smoothTime, 25f);

        //float ortographicSize = (targetList[0].position - center).magnitude/1.5f;
        float deltaX = maxX - minX;
        float deltaY = maxY - minY;

		float ortographicFinal;

		if(deltaX>deltaY){
			ortographicFinal = Mathf.Sqrt(deltaX)*5f;
		} else {
			ortographicFinal = Mathf.Sqrt(deltaY)*5f;
		}
       
		/*Debug.Log("X: "+ deltaX);
        Debug.Log("Y: " + deltaY);
        Debug.Log("");
		*/

        //float currentAspect = (float)Screen.width / (float)Screen.height;
		ortographicFinal = (Screen.height) * (ortographicFinal / 800f);

		ortographicFinal = Mathf.Clamp(ortographicFinal,oSizeMin,oSizeMax);
		Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, ortographicFinal, Time.deltaTime);        

		Debug.Log ("Screen W" + Screen.width);
		Debug.Log ("Screen H" + Screen.height);
		Debug.Log ("Delta X" + deltaX);
		Debug.Log ("Delta Y" + deltaY);
    }

}