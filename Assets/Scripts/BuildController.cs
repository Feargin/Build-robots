using System.Linq;
using UnityEngine;

namespace EnglishKids.BuildRobots
{

    public sealed class BuildController : MonoBehaviour
    {
        [SerializeField] private TypeRobot _type;
        [SerializeField] private DropHandler[] _details;
        [SerializeField] private GameObject _template;
        [SerializeField] private GameObject _completedRobot;
        [SerializeField] private GameObject _rectangle;
        [SerializeField] private int _indexSound;

        private void OnDisable() => DropHandler.CheckArrayDetails -= CheckArrayDetails;
        private void OnEnable() => DropHandler.CheckArrayDetails += CheckArrayDetails;

        private void Start()
        {
            _template.SetActive(true);
            _completedRobot.SetActive(false);
            _rectangle.SetActive(false);
            _indexSound = (int) _type == 0 ? 3 : 4;
        }

        private void CheckArrayDetails(DropHandler detail)
        {
            if (!_details.Contains(detail)) return;
            _rectangle.SetActive(true);
            AudioController.Instance.PlayAudioClipEffect(_indexSound, 2);
            for (var index = 0; index < _details.Length; index++)
            {
                if (!_details[index].IsFull) return;
                if (index == _details.Length - 1) ChangeTemplateRobot();
            }
        }

        private void ChangeTemplateRobot()
        {
            _template.SetActive(false);
            _completedRobot.SetActive(true);
            int indexClip = (int) _type == 0 ? 8 : 10;
            AudioController.Instance.PlayAudioClipEffect(indexClip, 1);
            AnimationsManager.Instance.RunStarsAnimationClip(transform.position);
            DropHandler.CheckArrayDetails -= CheckArrayDetails;
        }

    }

    public enum TypeRobot
    {
        Red,
        Blue,
    }
}