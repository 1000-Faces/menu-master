using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;

namespace DineEase.AR
{
    /// <summary>
    /// Controls the selection of an object via a Tap gesture.
    /// </summary>
    public class ExtendedSelectionInteractable : ARSelectionInteractable
    {
        /// <inheritdoc />
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
