using UnityEngine;
using UnityEngine.EventSystems;

namespace EnglishKids.BuildRobots
{
    public sealed class DropHandler : MonoBehaviour, IDropHandler
    {
        public static System.Action<DropHandler> CheckArrayDetails;
        [SerializeField] private TypeDetails _type;

        public bool IsFull { get; private set; } = false;

        public void OnDrop(PointerEventData eventData)
        {
            if (IsFull) return;
            var detail = DragHandler.DragDetail;
            if (detail == null || detail.Type != _type) return;
            detail.transform.SetParent(transform);
            ConveyorController.Instance.UpdateCountDetails();
            IsFull = true;
            CheckArrayDetails?.Invoke(this);
        }
    }
}
