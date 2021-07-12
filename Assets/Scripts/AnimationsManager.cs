using System.Collections;
using DG.Tweening;
//using Spine;
using UnityEngine;
using UnityEngine.Serialization;

namespace EnglishKids.BuildRobots
{
    public sealed class AnimationsManager : Singleton<AnimationsManager>
    {
        [Header("--------------Settings animations---------------")]
        [Space]
        [FormerlySerializedAs("_rotationSpeed")]
        [SerializeField]
        [Range(0.2f, 2f)]
        private float rotationSpeed;

        [FormerlySerializedAs("_moveSpeedConveyor")] 
        [SerializeField] [Range(0.5f, 3f)]
        private float moveSpeedConveyor;

        [Space(2)] [Header("-------------------Systems----------------------")] 
        [SerializeField]
        private RectTransform _conveyor;

        [SerializeField] private Transform _stars;
        [SerializeField] private Transform _starsParent;
        public static event System.Action<bool> IsMoveConveyor;
        private float _yConveyorPosition = 0;

        private void OnEnable() => ConveyorController.RunConveyor += StartingMovement;
        private void OnDisable() => ConveyorController.RunConveyor -= StartingMovement;
        private void StartingMovement() => StartCoroutine(MoveConveyor());

        private IEnumerator MoveConveyor()
        {
            StartCoroutine(DelayEventIsMoveConveyor());
            yield return new WaitForSeconds(0.5f);
            
            var sequence = DOTween.Sequence();
            AudioController.Instance.PlayAudioClipEffect(5, 1);
            sequence.AppendInterval(0.2f);
            sequence.Append(_conveyor.DOAnchorPos(new Vector2(0, _yConveyorPosition), moveSpeedConveyor, true).SetEase(Ease.Linear));
            sequence.OnComplete(OnCompliteMoveConveyor);
            _yConveyorPosition -= 1080;
        }

        private void OnCompliteMoveConveyor()
        {
            AudioController.Instance.PlayAudioClipEffect(5, 1);
            IsMoveConveyor?.Invoke(false);
        }

        private IEnumerator DelayEventIsMoveConveyor()
        {
            yield return new WaitForSeconds(1);
            IsMoveConveyor?.Invoke(true);
        }

        public void RotationWhenMoving(bool onDrag, Transform movable, Vector3 startRotation)
        {
            var target = onDrag ? Vector3.zero : startRotation;
            movable.DORotate(target, rotationSpeed);
        }

        public void ResetDrag(Transform movable, Vector3 startPosition, float move)
        {
            movable.DOMove(startPosition, move).SetEase(Ease.Linear);
        }

        public void RunStarsAnimationClip(Vector3 position)
        {
            var star = Instantiate(_stars, position, Quaternion.identity, _starsParent);
            Destroy(star.gameObject, 1f);
        }
    }
}
