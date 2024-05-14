using UnityEngine;

public class ConfettiParticleController : MonoBehaviour
{
    private Transform parentCheckpoint;

    void Start()
    {
        // Get reference to the parent checkpoint
        parentCheckpoint = transform.parent;
    }

    void Update()
    {
        // Update the position of the confetti particle relative to the parent checkpoint
        transform.position = parentCheckpoint.position;
    }
}
