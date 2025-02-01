using UnityEngine;

public class Door : MonoBehaviour, IInterectable
{
    [SerializeField] private float _moveDistance = 1f;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private bool _isOpened = false;

    private bool _isMoving = false;

    private Transform _parent;
    private Transform _leftDoor;
    private Transform _rightDoor;

    private Vector3 _leftDoorClosedPosition;
    private Vector3 _leftDoorOpenedPosition;
    private Vector3 _rightDoorClosedPosition;
    private Vector3 _rightDoorOpenedPosition;

    private void Start()
    {
        _parent = transform.parent;
        _leftDoor = _parent.GetChild(0);
        _rightDoor = _parent.GetChild(1);

        _leftDoorClosedPosition = _leftDoor.position;
        _leftDoorOpenedPosition = _leftDoorClosedPosition - new Vector3(0, 0, _moveDistance);

        _rightDoorClosedPosition = _rightDoor.position;
        _rightDoorOpenedPosition = _rightDoorClosedPosition - new Vector3(0, 0, -_moveDistance);
    }

    private void Update()
    {
        if (_isMoving)
        {
            _leftDoor.position = Vector3.Lerp(_leftDoor.position, _isOpened ? _leftDoorOpenedPosition : _leftDoorClosedPosition, _speed * Time.deltaTime);
            _rightDoor.position = Vector3.Lerp(_rightDoor.position, _isOpened ? _rightDoorOpenedPosition : _rightDoorClosedPosition, _speed * Time.deltaTime);

            if (Vector3.Distance(_leftDoor.position, _isOpened ? _leftDoorOpenedPosition : _leftDoorClosedPosition) < 0.01f)
            {
                _isMoving = false;
            }
        }
    }

    public void Interact()
    {
       _isOpened = !_isOpened;
       _isMoving = true;
    }
}
