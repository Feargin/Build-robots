using UnityEngine;

namespace EnglishKids.BuildRobots
{
    public class Detail : MonoBehaviour
    {
        public TypeDetails Type => _type;
        [SerializeField] private TypeDetails _type;
        public Transform StaticPoint { get; set; }
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