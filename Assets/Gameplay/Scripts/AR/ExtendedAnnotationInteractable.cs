using DineEase.Meal.Annotation;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;

namespace DineEase.AR
{
    /// <summary>
    /// An annotation that appears when the user hovers over the <see cref="GameObject"/>
    /// that the <see cref="ARAnnotationInteractable"/> component governing this annotation is attached to.
    /// This is a variation of <see cref="ARAnnotation"/> that allows for more detailed annotations.
    /// </summary>
    [Serializable]
    public class ARDetailedAnnotation
    {
        GameObject m_AnnotationVisualization;

        /// <summary>
        /// The visualization <see cref="GameObject"/> that will become active when the user hovers over this object.
        /// </summary>
        public GameObject AnnotationVisualization
        {
            get => m_AnnotationVisualization;
            set => m_AnnotationVisualization = value;
        }

        [SerializeField]
        [Tooltip("The state of the annotation")]
        bool m_IsEnabled;

        /// <summary>
        /// The state of the annotation
        /// </summary>
        public bool IsEnabled
        {
            get => m_IsEnabled;
            set => m_IsEnabled = value;
        }

        [SerializeField]
        [Tooltip("Maximum angle (in radians) off of FOV horizontal center to show annotation.")]
        float m_MaxFOVCenterOffsetAngle = 0.25f;

        /// <summary>
        /// Maximum angle (in radians) off of FOV horizontal center to show annotation.
        /// </summary>
        public float MaxFOVCenterOffsetAngle
        {
            get => m_MaxFOVCenterOffsetAngle;
            set => m_MaxFOVCenterOffsetAngle = value;
        }

        [SerializeField]
        [Tooltip("Minimum range to show annotation at.")]
        float m_MinAnnotationRange;

        /// <summary>
        /// Minimum range to show annotation at.
        /// </summary>
        public float MinAnnotationRange
        {
            get => m_MinAnnotationRange;
            set => m_MinAnnotationRange = value;
        }

        [SerializeField]
        [Tooltip("Maximum range to show annotation at.")]
        float m_MaxAnnotationRange = 10f;

        /// <summary>
        /// Maximum range to show annotation at.
        /// </summary>
        public float MaxAnnotationRange
        {
            get => m_MaxAnnotationRange;
            set => m_MaxAnnotationRange = value;
        }
    }

    /// <summary>
    /// Controls displaying one or more annotations when hovering over the <see cref="GameObject"/> this component is attached to.
    /// This component is a variation of <see cref="ARAnnotationInteractable"/> that is designed to work with <see cref="ARAnnotation"/>s.
    /// </summary>
    public class ExtendedAnnotationInteractable : ARBaseGestureInteractable
    {
        [SerializeField]
        List<ARAnnotationWindow> m_AnnotationWindows = new();

        /// <summary>
        /// The list of annotations.
        /// </summary>
        public List<ARAnnotationWindow> AnnotationWindows
        {
            get => m_AnnotationWindows;
            set => m_AnnotationWindows = value;
        }

        /// <inheritdoc />
        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractable(updatePhase);

            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
                UpdateVisualizations();
        }

        /// <inheritdoc />
        void UpdateVisualizations()
        {
            // Disable all annotations if not hovered.
            if (!isHovered)
            {
                foreach (var annotation in m_AnnotationWindows)
                {
                    annotation.Annotation.AnnotationVisualization.SetActive(false);
                }
            }
            else
            {
                // ReSharper disable once LocalVariableHidesMember -- hide deprecated camera property
                var camera = xrOrigin != null
                    ? xrOrigin.Camera
#pragma warning disable 618 // Calling deprecated property to help with backwards compatibility.
                    : (arSessionOrigin != null ? arSessionOrigin.camera : Camera.main);
#pragma warning restore 618
                if (camera == null)
                    return;

                var cameraTransform = camera.transform;
                var fromCamera = transform.position - cameraTransform.position;
                var distSquare = fromCamera.sqrMagnitude;
                fromCamera.y = 0f;
                fromCamera.Normalize();
                var dotProd = Vector3.Dot(fromCamera, cameraTransform.forward);

                foreach (var annotationWindow in m_AnnotationWindows)
                {
                    ARDetailedAnnotation annotation = annotationWindow.Annotation;

                    var enableThisFrame =
                        (Mathf.Acos(dotProd) < annotation.MaxFOVCenterOffsetAngle &&
                        distSquare >= Mathf.Pow(annotation.MinAnnotationRange, 2f) &&
                        distSquare < Mathf.Pow(annotation.MaxAnnotationRange, 2f));

                    if (annotation.AnnotationVisualization != null)
                    {
                        // If annotation is disabled, simply hide the visual.
                        if (!annotation.IsEnabled)
                        {
                            annotation.AnnotationVisualization.SetActive(false);
                            continue;
                        }

                        if (enableThisFrame && !annotation.AnnotationVisualization.activeSelf)
                            annotation.AnnotationVisualization.SetActive(true);
                        else if (!enableThisFrame && annotation.AnnotationVisualization.activeSelf)
                            annotation.AnnotationVisualization.SetActive(false);

                        // If enabled, align to camera
                        if (annotation.AnnotationVisualization.activeSelf)
                        {
                            annotation.AnnotationVisualization.transform.rotation =
                                Quaternion.LookRotation(fromCamera, transform.up);
                        }
                    }
                }
            }
        }
    }
}
