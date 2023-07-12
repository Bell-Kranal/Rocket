using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Logic.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMover : MonoBehaviour, IHaveFirstPosition
    {
        [Header("Movement")]
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _maxRotation;
        [SerializeField] private float _maxPositionDeltaForSpeed;
        [SerializeField] private float _maxPositionDeltaForRotation;
        [Space(10)]
        [Header("Rigidbody")]
        [SerializeField] private float _forwardImpulse;
        [Space(10)]
        [Header("Return to Y position")]
        [SerializeField] private float _maxDeltaBetweenCurrentPositionAndFirstPosition;
        [SerializeField] private float _interpolationToReturnPosition;
        
        private Vector2 _firstFingerPosition;
        private Vector2 _currentFingerPosition;
        private float _firstXRotation;
        private float _firstYRotation;
        private float _firstZRotation;
        private float _firstYPosition;
        private float _firstZPosition;
        private Rigidbody _rigidbody;
        private bool _canJump;
        private Sequence _returnToStartRotationSequence;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            _firstYPosition = transform.position.y;
            _firstZPosition = transform.position.z;
            
            _firstXRotation = transform.eulerAngles.x;
            _firstYRotation = 85f;
            _firstZRotation = 90f;
            
            _firstFingerPosition = Input.mousePosition;
            _currentFingerPosition = Input.mousePosition;
            FirstPosition = transform.position;
        }

        private void Update()
        {
            CheckTouchUp();
            
            if (!Input.GetMouseButton(0))
                return;

            CheckTouchDown();
            CheckCurrentFingerPosition();
            ReturnToFirstPosition();
        }

        public Vector3 FirstPosition { get; set; }

        public void Reset()
        {
            _rigidbody.useGravity = false;
            _rigidbody.velocity = Vector3.zero;
            this.enabled = false;
            
            _rigidbody.freezeRotation = true;
            transform.eulerAngles = new Vector3(_firstXRotation, _firstYRotation, _firstZRotation);
            _rigidbody.freezeRotation = false;
        }

        public void GoToFirstPosition()
        {
            transform.position = FirstPosition;
        }

        private void CheckCurrentFingerPosition()
        {
            _currentFingerPosition = Input.mousePosition;
            
            if (_currentFingerPosition.x < _firstFingerPosition.x)
            {
                Move(Vector3.left);
                Rotate(Vector3.left);
            }
            else
            {
                Move(Vector3.right);
                Rotate(Vector3.right);
            }
        }

        private void CheckTouchDown()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _rigidbody.useGravity = false;
                _firstFingerPosition = Input.mousePosition;

                if (transform.eulerAngles.x != _firstXRotation)
                {
                    _returnToStartRotationSequence = DOTween.Sequence();
                    _returnToStartRotationSequence.Append(transform.DORotate(new Vector3(_firstXRotation, 90f, _firstZRotation), 0.25f));
                    _returnToStartRotationSequence.Play();
                }
            }
        }

        private void CheckTouchUp()
        {
            if (Input.GetMouseButtonUp(0))
            {
                _rigidbody.useGravity = true;

                if (_canJump)
                {
                    _rigidbody.AddForce(transform.forward * _forwardImpulse, ForceMode.Impulse);
                    _rigidbody.AddTorque(transform.right * Random.Range(-0.05f, 0.05f), ForceMode.Impulse);
                }

                _canJump = false;
            }
        }

        private void ReturnToFirstPosition()
        {
            if (Math.Abs(transform.position.y - _firstYPosition) > _maxDeltaBetweenCurrentPositionAndFirstPosition)
            {
                transform.position = new Vector3(transform.position.x,
                    Mathf.Lerp(transform.position.y, _firstYPosition, _interpolationToReturnPosition * Time.deltaTime),
                    transform.position.z);
            }
            else
            {
                _canJump = true;
            }
            
            if (Math.Abs(transform.position.z - _firstZPosition) > 0.1f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y,
                    Mathf.Lerp(transform.position.z, _firstZPosition, _interpolationToReturnPosition * Time.deltaTime));
            }
        }

        private void Rotate(Vector3 direction)
        {
            if (_returnToStartRotationSequence != null && _returnToStartRotationSequence.IsPlaying())
            {
                return;
            }
            
            transform.eulerAngles = new Vector3(CalculateXRotation(direction), _firstYRotation, _firstZRotation);
        }

        private float CalculateXRotation(Vector3 direction)
        {
            float currentAngle = transform.eulerAngles.x;
            float targetAngle = _firstXRotation + GetRotation() * direction.x;
            float resultValue = Mathf.Lerp(targetAngle, currentAngle, Time.deltaTime);
            
            return resultValue;
        }

        private void Move(Vector3 direction) =>
            transform.position += direction * GetSpeed() * Time.deltaTime;

        private float GetSpeed() =>
            Mathf.Lerp(0f, _maxSpeed, GetInterpolationValue(_maxPositionDeltaForSpeed));

        private float GetRotation() =>
            Mathf.Lerp(0, _maxRotation, GetInterpolationValue(_maxPositionDeltaForRotation));

        private float GetInterpolationValue(float delta) =>
            Mathf.Clamp(Mathf.Abs(_currentFingerPosition.x - _firstFingerPosition.x) / delta, 0f, 1f);
    }
}