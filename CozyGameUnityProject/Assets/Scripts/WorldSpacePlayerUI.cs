using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpacePlayerUI : MonoBehaviour
{
    public PlayerCharacterController PlayerCharacterController;

    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(PlayerCharacterController.transform.position);
    }
}
