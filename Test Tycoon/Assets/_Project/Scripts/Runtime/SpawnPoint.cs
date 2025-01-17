using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private string _buidingName;
    public string BuidingName => _buidingName;
}
