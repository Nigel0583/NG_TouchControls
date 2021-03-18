using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleControl : MonoBehaviour, IControllable
{
    private Plane _objPlane;
    private Vector3 _vec;
    private Renderer _ourRenderer;
    private static readonly int ColorId = Shader.PropertyToID("_Color");

    public void MoveObject(Ray ourRay)
    {
        if (!Physics.Raycast(ourRay.origin, ourRay.direction)) return;
        var position = transform.position;
        _objPlane = new Plane(Camera.main.transform.forward * -1, position);


        _objPlane.Raycast(ourRay, out float rayDistance);
        _vec = position - ourRay.GetPoint(rayDistance);
    }

    public void IsMoving(Ray ourRay)
    {
        if (_objPlane.Raycast(ourRay, out float rayDistance))
        {
            transform.position = ourRay.GetPoint(rayDistance) + _vec;
        }
    }

    public void ResizeObject(float initialDistance, float currentDistance, Vector3 initialScale)
    {
        if (Mathf.Approximately(initialDistance, 0)) return;

        float factor = currentDistance / initialDistance;
        transform.localScale = initialScale * factor;
    }

    private void Start()
    {
        _ourRenderer = GetComponent<Renderer>();
        _ourRenderer.material.SetColor(ColorId, Color.blue);
    }

    public void ObjectSelected()
    {
        _ourRenderer.material.SetColor(ColorId, Color.red);
    }

    public void ObjectDeselected()
    {
        _ourRenderer.material.SetColor(ColorId, Color.yellow);
    }

    public void RotateObject(Vector3 initialScale)
    {
    }

    public void MoveTo(Vector3 destination)
    {
    }

    public void youve_been_touched()
    {
    }
}