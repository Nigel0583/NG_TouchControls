using UnityEngine;

public class CubeControl : MonoBehaviour, IControllable
{
    private Plane _plane;
    private Ray _tRay;
    private float _rayDistance;
    private Vector3 _vec;
    private Camera _camera;
    private Renderer _ourRenderer;
    private static readonly int ColorId = Shader.PropertyToID("_Color");

    // Start is called before the first frame update
    private void Start()
    {
        _camera = Camera.main;
        _ourRenderer = GetComponent<Renderer>();
        _ourRenderer.material.SetColor(ColorId, Color.cyan);
    }

    public void youve_been_touched()
    {
        transform.position += Vector3.up;
    }

    public void MoveTo(Vector3 destination)
    {
        transform.position = destination;
    }

    public void MoveObject(Ray ourRay)
    {
        Vector3 position = transform.position;
        _plane = new Plane(Vector3.up, position);

        _tRay = _camera.ScreenPointToRay(Input.touches[0].position);
        _plane.Raycast(_tRay, out _rayDistance);
        _vec = position - _tRay.GetPoint(_rayDistance);
    }

    public void IsMoving(Ray ourRay)
    {
        _tRay = _camera.ScreenPointToRay(Input.touches[0].position);
        if (_plane.Raycast(_tRay, out _rayDistance))
        {
            transform.position = _tRay.GetPoint(_rayDistance) + _vec;
        }
    }

    public void RotateObject(Vector3 initialScale)
    {
        transform.Rotate(initialScale, Space.World);
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
}