using UnityEngine;

public class Door : MonoBehaviour, IInterectable
{
    [SerializeField] private float _moveDistance = 2f;
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

    private AudioSource _doorSound;

    private void Start()
    {
        _parent = transform.parent;
        _doorSound = _parent.GetComponent<AudioSource>();

        _leftDoor = _parent.GetChild(0);
        _rightDoor = _parent.GetChild(1);

        _leftDoorClosedPosition = _leftDoor.localPosition;
        _leftDoorOpenedPosition = _leftDoorClosedPosition - new Vector3(0, 0, _moveDistance);

        _rightDoorClosedPosition = _rightDoor.localPosition;
        _rightDoorOpenedPosition = _rightDoorClosedPosition - new Vector3(0, 0, -_moveDistance);
    }

    private void Update()
    {
        if (_isMoving)
        {
            float openTime = _speed * Time.deltaTime;

            _leftDoor.localPosition = Vector3.Lerp(_leftDoor.localPosition, _isOpened ? _leftDoorOpenedPosition : _leftDoorClosedPosition, openTime);
            _rightDoor.localPosition = Vector3.Lerp(_rightDoor.localPosition, _isOpened ? _rightDoorOpenedPosition : _rightDoorClosedPosition, openTime);

            if (Vector3.Distance(_leftDoor.localPosition, _isOpened ? _leftDoorOpenedPosition : _leftDoorClosedPosition) < 0.001f)
            {
                _isMoving = false;
            }
        }
    }

    public void Interact()
    {
        
       _isOpened = !_isOpened;
       _isMoving = true;
       _doorSound.Play();
    }
}
