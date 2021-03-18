using UnityEngine;

public class SphereControl : MonoBehaviour, IControllable
{
    private Renderer _ourRenderer;
    private float _speedMod = 100;
    private static readonly int ColorId = Shader.PropertyToID("_Color");
    
    public void IsMoving(Ray ourRay)
    {
        Touch touch = Input.GetTouch(0);
        var transformPos = transform;
        var position = transformPos.position;
        position = new Vector3(position.x + touch.deltaPosition.x * _speedMod,
            position.y,
            position.z + touch.deltaPosition.y * _speedMod);
        transformPos.position = position;
    }

    public void youve_been_touched()
    {
        transform.position += Vector3.right;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _speedMod = 0.001f;
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

    public void ResizeObject(float initialDistance, float currentDistance, Vector3 initialScale)
    {
    }

    public void RotateObject(Vector3 initialScale)
    {
    }

    public void MoveObject(Ray ourRay)
    {
    }

    public void MoveTo(Vector3 destination)
    {
    }
}