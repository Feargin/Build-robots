using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace EnglishKids.BuildRobots
{
    public sealed class SpawnElements : MonoBehaviour
    {
        [FormerlySerializedAs("_pointsDetails")] [SerializeField] private Transform[] _spawnPointsDetails;
        [SerializeField] private Transform[] _staticPoints;
        [SerializeField] private List<DragHandler> _roboDetails;
        [SerializeField] private Transform _parent;

        public void Init()
        {
            ConveyorController.Instance.SetCountDetails(_roboDetails.Count - 1);
            for (var point = 0; point < _spawnPointsDetails.Length; point++)
            {
                var index = Random.Range(0, _roboDetails.Count - 1);
                Spawn(point, index);
                _roboDetails.Remove(_roboDetails[index]);
            }
        }

        private void Spawn(int point, int index)
        {
            var detail = Instantiate(_roboDetails[index], _spawnPointsDetails[point].position,
                Quaternion.Euler(0, 0, Random.Range(-25f, 25f)), _parent);
            var indexStaticPoint = point < 4 ? point : point - 4;
            detail.Init();
            detail._detail.StaticPoint = _staticPoints[indexStaticPoint];
        }
    }
}