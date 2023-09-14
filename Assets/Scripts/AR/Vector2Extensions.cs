using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DineEase.AR
{
    public static class Vector2Extensions
    {
        public static bool IsPointOverUIObject(this Vector2 pos)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return false;
            }

            PointerEventData eventPosition = new(EventSystem.current)
            {
                position = new Vector2(pos.x, pos.y)
            };

            List<RaycastResult> results = new();
            EventSystem.current.RaycastAll(eventPosition, results);

            return results.Count > 0;
        }
    }
}
