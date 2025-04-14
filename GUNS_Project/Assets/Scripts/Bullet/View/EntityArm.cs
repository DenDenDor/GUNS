using UnityEngine;

public class EntityArm : MonoBehaviour
{
   [SerializeField] private Transform _shotPoint;

   public Transform ShotPoint => _shotPoint;
}
