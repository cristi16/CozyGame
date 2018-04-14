using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/EnemyStats")]
public class EnemyStats : ScriptableObject {

	public float moveSpeed = 1;
	public float lookRange = 40f;
	public float lookSphereCastRadius = 1f;

	public float attackRange = 1f;
	public float attackRate = 1f;
	public int attackDamage = 50;

    [Header("WanderState")]
    public float wanderRadius = 2f;
    public float scanForPlayerRadius = 5f;
}