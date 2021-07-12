using UnityEngine;
using System.Collections.Generic;

namespace EnglishKids.BuildRobots
{
    public static class ExtensionMethod
    {
        public static bool RectOverlaps(this RectTransform rectTransOne, RectTransform rectTransTwo, float compression)
        {
            Rect rectOne = new Rect(rectTransOne.position.x - (rectTransOne.position.x * compression), 
                rectTransOne.localPosition.y - (rectTransOne.position.x * compression), rectTransOne.rect.width,
                rectTransOne.rect.height);
            Rect rectTwo = new Rect(rectTransTwo.position.x - (rectTransTwo.position.x * compression), 
                rectTransTwo.position.y - (rectTransTwo.position.x * compression), rectTransTwo.rect.width,
                rectTransTwo.rect.height);

            return rectOne.Overlaps(rectTwo);
        }
        
        public static void Clone<T>(this IList<T> listToClone, IList<T> listFromClone)
        {
            listToClone.Clear();
            Debug.Log("!!");
            foreach (var t in listFromClone)
            {
                listToClone.Add(t);
            }
        }
    }
}