using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchManager : MonoBehaviour
{
    private IControllable _selectedObject;
    private IControllable _hitObject;
    public Camera cam;
    private Touch _initTouch;
    private Touch _touchZero;
    private Touch _touchOne;
    private Vector2 _began;
    private Vector3 _originRot;
    private Vector3 _initialScale;
    private float _initialDistance;
    private const float MagnitudeTap = 20f;
    private const float TapTime = 0.1f;
    private const float RotSpeed = 0.1f;
    private const float ZoomSpeed = 100.0f;
    private const float Dir = -1;
    private float _rotX;
    private float _rotY;
    private float _deltaX;
    private float _deltaY;
    private float _touchTime;
    private bool _moreThenOneTouch;

    private void Start()
    {
        _originRot = cam.transform.eulerAngles;
        _rotX = _originRot.x;
        _rotY = _originRot.y;
    }

    private void Update()
    {
        if (Input.touchCount <= 0) return;
        _touchZero = Input.GetTouch(0);

        Ray ourRay = cam.ScreenPointToRay(Input.touches[0].position);
        RaycastHit hit;
        if (Physics.Raycast(ourRay, out hit))
        {
            _hitObject = hit.transform.GetComponent<IControllable>();

            if (_hitObject != null)
            {
                _selectedObject = _hitObject;
                _selectedObject.ObjectSelected();
            }
            else
            {
                _selectedObject = null;
            }
        }

        _moreThenOneTouch = Input.touchCount > 1;

        if (Input.touchCount == 1 && !_moreThenOneTouch)
        {
            switch (Input.touches[0].phase)
            {
                case TouchPhase.Began:
                    InitValues();
                    _selectedObject?.MoveObject(ourRay);

                    break;
                case TouchPhase.Moved:

                    _selectedObject?.IsMoving(ourRay);

                    if (_hitObject == null)
                    {
                        RotateCamera();
                    }

                    break;

                case TouchPhase.Ended:

                    if ((_began - _touchZero.position).magnitude < MagnitudeTap && _touchTime < TapTime)
                    {
                        _selectedObject?.youve_been_touched();
                    }

                    ResetValues();

                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        else if (Input.touchCount == 2 && _moreThenOneTouch)
        {
            _touchZero = Input.GetTouch(0);
            _touchOne = Input.GetTouch(1);
            _moreThenOneTouch = true;

            if (_selectedObject != null && _moreThenOneTouch)
            {
                if (_touchZero.phase == TouchPhase.Began || _touchOne.phase == TouchPhase.Began)
                {
                    _initialDistance = Vector2.Distance(_touchZero.position, _touchOne.position);
                    _initialScale = transform.localScale;
                    _initTouch = _touchZero;
                }

                float currentDistance = Vector2.Distance(_touchZero.position, _touchOne.position);
                _selectedObject.ResizeObject(_initialDistance, currentDistance, _initialScale);
                _selectedObject.RotateObject(_initialScale);
            }
            else if (_touchZero.phase == TouchPhase.Moved && _touchOne.phase == TouchPhase.Moved)
            {
                float deltaMagnitudeDiff = CalcCameraMove(_touchOne, _touchZero);
                cam.transform.position -= transform.forward * (deltaMagnitudeDiff / ZoomSpeed);
            }
            else if (_touchZero.phase == TouchPhase.Stationary && _touchOne.phase == TouchPhase.Moved)
            {
                MoveCamera();
            }
        }
    }

    private void InitValues()
    {
        _began = _touchZero.position;
        _initTouch = _touchZero;
        _touchTime = Time.deltaTime;
    }

    private void ResetValues()
    {
        _initTouch = new Touch();
        _touchZero = new Touch();
        _touchOne = new Touch();
        _selectedObject?.ObjectDeselected();
    }

    private void MoveCamera()
    {
        Vector2 touchDeltaPos = _touchOne.deltaPosition * Time.deltaTime;
        cam.transform.Translate(-touchDeltaPos.x, -touchDeltaPos.y, 0);
    }

    private void RotateCamera()
    {
        _deltaX = _initTouch.position.x - _touchZero.position.x;
        _deltaY = _initTouch.position.y - _touchZero.position.y;
        _rotX -= _deltaY * Time.deltaTime * RotSpeed * Dir;
        _rotY += _deltaX * Time.deltaTime * RotSpeed * Dir;
        _rotX = Mathf.Clamp(_rotX, -80f, 80f);
        cam.transform.eulerAngles = new Vector3(_rotX, _rotY, 0f);
    }

    private static float CalcCameraMove(Touch touchOne, Touch touchZero)
    {
        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

        float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

        return deltaMagnitudeDiff;
    }

    public void ResetScene()
    {
        SceneManager.LoadScene("Scene01");
    }
}