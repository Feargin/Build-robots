using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnglishKids.BuildRobots
{
    public sealed class SpawnElements : MonoBehaviour
    {
        [SerializeField] private Transform[] _pointsDetails;
        [SerializeField] private Transform[] _staticPoints;
        [SerializeField] private List<DragHandler> _roboDetails;
        [SerializeField] private Transform _parent;

        public void Init()
        {

            ConveyorController.Instance.SetCountDetails(_roboDetails.Count - 1);
            for (int point = 0; point < _pointsDetails.Length; point++)
            {
                var index = Random.Range(0, _roboDetails.Count - 1);
                Spawn(point, index);
                _roboDetails.Remove(_roboDetails[index]);
            }
        }

        private void Spawn(int point, int index)
        {
            var detail = Instantiate(_roboDetails[index], _pointsDetails[point].position,
                Quaternion.Euler(0, 0, Random.Range(-25f, 25f)), _parent);
            var indexStaticPoint = point < 4 ? point : point - 4;
            detail.StaticPoint = _staticPoints[indexStaticPoint];
            detail.Init();
        }
    }
}