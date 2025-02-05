using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _footstepSounds;
    [SerializeField] private AudioClip _painSound;
    [SerializeField] private float _stepInterval = 0.5f;
    
    private AudioSource _audioSource;
    private CharacterController _characterController;
    private PlayerController _playerController;
    private float _stepTimer;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerController = GetComponent<PlayerController>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_playerController.IsGrounded && _characterController.velocity.magnitude > 0.1f)
        {
            _stepTimer -= Time.deltaTime;

            if (_stepTimer <= 0)
            {
                PlayFootstepSound();

                if (_playerController.IsRunning)
                {
                    _stepTimer = _stepInterval / 2;
                }
                else
                {
                    _stepTimer = _stepInterval;
                }
            }
        }
    }

    private void PlayFootstepSound()
    {
        AudioClip clip = _footstepSounds[Random.Range(0, _footstepSounds.Length)];

        _audioSource.PlayOneShot(clip);
    }

    public void PlayPainSound()
    {
        _audioSource.PlayOneShot(_painSound);
    }
}
