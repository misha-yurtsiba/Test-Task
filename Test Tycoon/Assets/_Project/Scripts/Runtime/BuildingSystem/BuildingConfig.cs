using UnityEngine;

[CreateAssetMenu(fileName = "BuindingConfig")]
public class BuildingConfig : ScriptableObject
{
    [SerializeField] private Building _buildingPrefab;
    [SerializeField] private int _cost;
    [SerializeField] private int _resourcePerSecond;
    [SerializeField] private string _buildingName;

    public Building BuildingPrefab => _buildingPrefab;
    public int Cost => _cost;
    public int ResourcePerSecond => _resourcePerSecond;
    public string BuildingName => _buildingName;

}
