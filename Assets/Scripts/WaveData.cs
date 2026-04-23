using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Tower Defense/Wave")]
public class WaveData : ScriptableObject
{
    public GameObject enemyPrefab; 
    public int count;              
    public float rate;             
}