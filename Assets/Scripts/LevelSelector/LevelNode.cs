using UnityEngine;


public enum LevelType { Battle, Shop, Rest, Boss}

public class LevelNode : MonoBehaviour
{
    public LevelNode[] children;
    public LevelType type;
    public bool isUnlocked = false;


}