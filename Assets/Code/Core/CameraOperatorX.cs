
using UnityEngine;

public class CameraOperatorX : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    void LateUpdate()
    {
        transform.position = new Vector3(_target.transform.position.x + 8,
            transform.position.y,
            transform.position.z);
    }
}
