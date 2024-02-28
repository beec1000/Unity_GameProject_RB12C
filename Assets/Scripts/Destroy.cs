using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private float aliveTime = 3f;
    private void Awake() => Destroy(gameObject, aliveTime);
}
