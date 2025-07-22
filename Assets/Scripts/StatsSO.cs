using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Scriptable Objects/Stats")]
public class StatsSO : ScriptableObject
{
    public string Name;
    public float turnSpeed = 2f;
    public float maxSpeed = 100;
    public float acceleration = 20;
    public float driftFactor = .95f;

    public int maxHealth = 100;

    public int damage = 10;
}
