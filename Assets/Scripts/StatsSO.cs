using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Scriptable Objects/Stats")]
public class StatsSO : ScriptableObject
{
    [Header("UnitName")]
    public string Name;

    [Header("Defense")]
    public int maxHealth = 100;

    [Header("Offense")]
    public int damage = 10;
    public int rammingDamage = 5;
    public float fireRate = 0.2f;

    [Header("Movement")]
    public float turnSpeed = 100f;
    public float maxSpeed = 1000;
    public float acceleration = 100;
    public float driftFactor = .95f;

    [Header("Reward")]
    public int currency = 5;
}
