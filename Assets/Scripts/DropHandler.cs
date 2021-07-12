using UnityEngine;
using UnityEngine.EventSystems;

namespace EnglishKids.BuildRobots
{
    public sealed class DropHandler : MonoBehaviour, IDropHandler
    {
        public static event System.Action<DropHandler> OnDropDetail;
        [SerializeField] private TypeDetails _type;

        public bool IsEmpty { get; private set; } = true;

        public void OnDrop(PointerEventData eventData)
        {
            if (!IsEmpty) return;
            var detail = DragHandler.DragDetail;
            if (detail == null || detail._detail.Type != _type) return;
            detail.transform.SetParent(transform);
            
            ConveyorController.Instance.UpdateCountDetails();
            
            IsEmpty = false;
            OnDropDetail?.Invoke(this);
        }
    }
}
