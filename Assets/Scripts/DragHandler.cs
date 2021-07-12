using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace EnglishKids.BuildRobots
{
    public sealed class DragHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public static DragHandler DragDetail { get; private set; }
        public Transform StaticPoint { get; set; }
        public TypeDetails Type => _type;
        public RectTransform Rect => _rect;
        private Vector3 _startRotation;
        private Transform _startParent;
        private CanvasGroup _canvasGroup;
        private RectTransform _rect;

        [FormerlySerializedAs("_Type")] 
        [SerializeField]
        private TypeDetails _type;

        public void Init()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rect = GetComponent<RectTransform>();
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            DragDetail = this;
            transform.parent = StaticPoint;
            _startParent = transform.parent;
            _startRotation = transform.rotation.eulerAngles;
            _canvasGroup.blocksRaycasts = false;
            AnimationsManager.Instance.RotationWhenMoving(true, transform, _startRotation);
            AudioController.Instance.PlayAudioClipEffect(6, 1);
            _startParent.SetSiblingIndex(3);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //if(_isStop) return;
            _canvasGroup.blocksRaycasts = true;
            if (_startParent == transform.parent)
            {
                transform.parent = StaticPoint;
                AnimationsManager.Instance.RotationWhenMoving(false, transform, _startRotation);
                AnimationsManager.Instance.ResetDrag(transform, StaticPoint.position, 0.4f);
                AudioController.Instance.PlayAudioClipEffect(11, 1);
            }
            else
            {
                AnimationsManager.Instance.ResetDrag(transform, transform.parent.position, 0.2f);
                AnimationsManager.Instance.RunStarsAnimationClip(transform.parent.position);
                AudioController.Instance.PlayAudioClipEffect(2, 1);
                if(DragDetail != null) DragDetail.enabled = false;
            }

            DragDetail = null;
        }

        public void OnDrag(PointerEventData eventData)
        {
            //if(_isStop) return;
            transform.position = Input.mousePosition;
        }
    }

    public enum TypeDetails
    {
        LeftHeand_L = 1,
        RightHeand_L,
        Head_L,
        Wheel_L,
        LeftHeand_R,
        RightHeand_R,
        Head_R,
        Wheel_R,
    }
}