using UnityEngine;
using UnityEngine.EventSystems;

namespace EnglishKids.BuildRobots
{
    public sealed class DragHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public static DragHandler DragDetail { get; private set; }
        
        [SerializeField] private float _moveSpeed;
        private Vector3 _startRotation;
        private Transform _startParent;
        private CanvasGroup _canvasGroup;
        private RectTransform _rect;
        public Detail _detail { get; private set; }
        
        public void Init()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _detail = GetComponentInChildren<Detail>();
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            DragDetail = this;
            transform.parent = _detail.StaticPoint;
            _startParent = transform.parent;
            _startRotation = transform.rotation.eulerAngles;
            _canvasGroup.blocksRaycasts = false;
            
            AnimationsManager.Instance.RotationWhenMoving(true, transform, _startRotation);
            AudioController.Instance.PlayAudioClipEffect(6, 1);
            
            _startParent.SetSiblingIndex(3);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;
            if (_startParent == transform.parent)
            {
                transform.parent = _detail.StaticPoint;
                
                AnimationsManager.Instance.RotationWhenMoving(false, transform, _startRotation);
                AnimationsManager.Instance.ResetDrag(transform, _detail.StaticPoint.position, _moveSpeed);
                AudioController.Instance.PlayAudioClipEffect(11, 1);
            }
            else
            {
                AnimationsManager.Instance.ResetDrag(transform, transform.parent.position, _moveSpeed / 2);
                AnimationsManager.Instance.RunStarsAnimationClip(transform.parent.position);
                AudioController.Instance.PlayAudioClipEffect(2, 1);
                
                if(DragDetail != null) DragDetail.enabled = false;
            }
            DragDetail = null;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }
    }

    
}