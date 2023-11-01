using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;


namespace DineEase.AR
{
    /// <summary>
    /// Controls the placement of Prefabs via a tap gesture.
    /// </summary>
    public class ExtendedPlacementInteractable : ARPlacementInteractable
    {
        protected override bool CanStartManipulationForGesture(TapGesture gesture)
        {
            if (gesture.startPosition.IsPointOverUIObject())
            {
                return false;
            }

            return base.CanStartManipulationForGesture(gesture);
        }
    }
}
