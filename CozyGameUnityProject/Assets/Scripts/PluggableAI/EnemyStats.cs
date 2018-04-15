using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/EnemyStats")]
public class EnemyStats : ScriptableObject {

    [Header("Attack Action")]
    public float attackRange = 1f;
    public float attackSphereCastRadius = 1f;
    public float attackRate = 1f;
	public int attackDamage = 50;

    [Header("Wander Action")]
    public float wanderRadius = 2f;
    public Color wanderRadiusColor = Color.white;
    public float scanForPlayerRadius = 5f;
    public Color scanForPlayerRadiusColor = Color.white;
    [Header("Listen Action")]
    public float listenForNoiseRadius = 10f;
    public Color listenForNoiseRadiusColor = Color.white;
    public AnimationCurve noiseRolloff = new AnimationCurve();
}