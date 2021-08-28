using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShip : MonoBehaviour
{
    public Laser laser;
    public Transform laserSource;
    public Transform turret;
    public float LerpSpeed;
    public float LeprThicknessSpeed;
    public float ActivateTimeDelay;

    float targetLength;
    public Explode explodeShip;
    Vector3 hitPoint;
    public ParticleSystem scorch;
    Vector2 hitCoords;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            StartCoroutine(RaycastCoroutine());
            StartCoroutine(LaserCoroutine());
        }
        if (Input.GetMouseButtonUp(0))
        {
            scorch.Stop();
            StopAllCoroutines();
            laser.Deactivate();
            laser.HitDeactivate();
            StartCoroutine(LaserDeactivateCoroutine());
        }
    }

    IEnumerator LaserDeactivateCoroutine()
    {
        float startThickness = laser.Thickness;
        float lerp = 0;
        while (lerp < 1)
        {
            laser.Thickness = Mathf.Lerp(startThickness, 0, lerp);
            lerp += Time.deltaTime * LeprThicknessSpeed;
            yield return null;
        }
        laser.ResetLaser();
        laser.Length = 0;
    }

    IEnumerator LaserCoroutine()
    {
        laser.Activate();
        yield return new WaitForSeconds(ActivateTimeDelay);
        float startLength = 0;
        float lerp = 0;
        while (lerp < 1)
        {
            laser.Length = Mathf.Lerp(startLength, targetLength, lerp);
            lerp += Time.deltaTime* LerpSpeed;
            yield return null;
        }
        laser.HitActivate();
        scorch.Play();
        while (true)
        {
            if (explodeShip)
            {
                explodeShip.ReduceHealth(1 * Time.deltaTime, hitPoint, laserSource.position);
            }
            else
            {
                scorch.transform.localPosition = new Vector3(hitCoords.x*2-1f, hitCoords.y*2-1f, 10);

            }
            if (targetLength < 0)
            {
                laser.Length += Time.deltaTime * LerpSpeed*20;
                laser.HitDeactivate();
            }
            else
            {
                laser.Length = targetLength;
            }

            yield return null;
        }
    }

    IEnumerator RaycastCoroutine()
    {
        while (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                hitCoords = hit.textureCoord;
                explodeShip = hit.transform.GetComponentInParent<Explode>();
                hitPoint = hit.point;
                targetLength = (hit.point - laserSource.position).magnitude;
                turret.forward = (hit.point - laserSource.position).normalized;
            }
            else
            {
                targetLength = -1;
            }
            yield return null;
        }
    }
}
