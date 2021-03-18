using UnityEngine;

internal interface IControllable
{
    void youve_been_touched();

    void ObjectSelected();

    void ObjectDeselected();
    void MoveTo(Vector3 destination);

    void MoveObject(Ray ourRay);

    void IsMoving(Ray ourRay);

    void ResizeObject(float initialDistance, float currentDistance, Vector3 initialScale);

    void RotateObject(Vector3 initialScale);
}