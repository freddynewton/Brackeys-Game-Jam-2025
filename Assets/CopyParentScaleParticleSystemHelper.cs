using UnityEngine;

public class CopyParentScaleParticleSystemHelper : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        var parentTransform = transform.parent;
        var shapeModule = _particleSystem.shape;
        shapeModule.rotation = new Vector3(shapeModule.rotation.x, shapeModule.rotation.y * parentTransform.lossyScale.x, shapeModule.rotation.z);
    }
}
