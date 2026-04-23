using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower Defense/Tower Data")]
public class TowerData : ScriptableObject
{
    public string towerName;
    public int cost;
    public GameObject prefab;
    public Sprite icon; // Dung de hien thi UI Button
}