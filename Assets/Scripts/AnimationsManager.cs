using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace EnglishKids.BuildRobots
{
    public sealed class AnimationsManager : Singleton<AnimationsManager>
    {
        
        [Header("--------------Settings animations---------------")]
        [Space]
        [SerializeField, Range(0.2f, 2f)] 
        private float _rotationSpeed;
        [SerializeField, Range(0.5f, 3f)]
        private float _moveSpeedConveyor;

        [Space(2), Header("-------------------Systems----------------------")] 
        [SerializeField]
        private RectTransform _conveyor;

        [SerializeField] private Transform _stars;
        [SerializeField] private Transform _starsParent;

        private void OnEnable() => ConveyorController.OnRunConveyor += StartingMovement;
        private void OnDisable() => ConveyorController.OnRunConveyor -= StartingMovement;
        private void StartingMovement() => StartCoroutine(MoveConveyor());

        private IEnumerator MoveConveyor()
        {
            yield return new WaitForSeconds(0.5f);
            
            AudioController.Instance.PlayAudioClipEffect(5, 1);
            
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(0.2f);
            sequence.Append(_conveyor.DOAnchorPos(new Vector2(0, _conveyor.transform.localPosition.y - 1080f), _moveSpeedConveyor, true).SetEase(Ease.Linear));
            sequence.OnComplete(OnCompliteMoveConveyor);
        }

        private void OnCompliteMoveConveyor()
        {
            AudioController.Instance.PlayAudioClipEffect(5, 1);
        }
        

        public void RotationWhenMoving(bool isDrag, Transform movable, Vector3 startRotation)
        {
            var target = isDrag ? Vector3.zero : startRotation;
            movable.DORotate(target, _rotationSpeed);
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
