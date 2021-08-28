using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Laser : MonoBehaviour
{
    public Transform _LaserBody;
    public float Length;
    [SerializeField] private float _scaleFactor;
    float _startThickness;
    public float Thickness;

    [SerializeField] ParticleSystem _SourceEffect;
    [SerializeField] ParticleSystem _HitEffect;
    [SerializeField] Transform _HitPoint;

    private void Start()
    {
        _startThickness = Thickness;
        _scaleFactor = transform.localScale.x;
        if (transform.parent != null)
        {
            CalcScaleFactor(transform.parent);
        }
    }

    public void Activate()
    {
        if (!_SourceEffect.isPlaying)
        {
            _SourceEffect.Play();
        }
    }

    public void HitActivate()
    {
        if (!_HitEffect.isPlaying)
        {
            _HitEffect.Play();
        }
    }

    public void HitDeactivate()
    {
        _HitEffect.Stop();
    }

    public void Deactivate()
    {
        _SourceEffect.Stop();
    }

    void CalcScaleFactor(Transform parent)
    {
        _scaleFactor *= parent.localScale.z;
        if (parent.parent != null)
        {
            CalcScaleFactor(parent.parent);
        }
    }

    public void ResetLaser()
    {
        Vector3 scale = _LaserBody.localScale;
        scale.y = 0;
        scale.x = 0;
        scale.z = 0;
        _LaserBody.localScale = scale;
        Thickness = _startThickness;
    }

    private void Update()
    {
        Vector3 scale = _LaserBody.localScale;
        scale.y = Thickness;
        scale.x = Thickness;
        scale.z = (Length / _scaleFactor);
        _LaserBody.localScale = scale;

        _HitEffect.transform.forward = _HitPoint.transform.forward;
        _HitEffect.transform.position = _HitPoint.transform.position;
    }
}
