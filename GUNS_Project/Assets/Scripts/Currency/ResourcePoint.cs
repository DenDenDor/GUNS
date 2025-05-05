using UnityEngine;

public class ResourcePoint : MonoBehaviour
{
 [SerializeField] private Transform _silver;
 [SerializeField] private Transform _gold;
 public Transform Gold => _gold;
 public Transform Silver => _silver;
}
