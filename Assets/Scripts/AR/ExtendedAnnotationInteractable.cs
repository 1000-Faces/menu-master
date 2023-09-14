using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;

namespace DineEase.AR.Interactables
{
    /// <summary>
    /// An annotation that appears when the user hovers over the <see cref="GameObject"/>
    /// that the <see cref="ARAnnotationInteractable"/> component governing this annotation is attached to.
    /// This is a extended version of <see cref="ARAnnotation"/> that allows for more detailed annotations.
    /// </summary>
    [Serializable]
    public class ARDetailedAnnotation : ARAnnotation
    {
        [SerializeField]
        [Tooltip("The identifier of the annotation element")]
        string m_Name;

        /// <summary>
        /// The identifier of the annotation element
        /// </summary>
        public string Name
        {
            get => m_Name;
            set => m_Name = value;
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
    }

    /// <summary>
    /// Controls displaying one or more annotations when hovering over the <see cref="GameObject"/> this component is attached to.
    /// This component is a variation of <see cref="ARAnnotationInteractable"/> that is designed to work with <see cref="ARAnnotation"/>s.
    /// </summary>
    public class ExtendedAnnotationInteractable : ARBaseGestureInteractable
    {
        [SerializeField]
        List<ARDetailedAnnotation> m_Annotations = new();

        /// <summary>
        /// The list of annotations.
        /// </summary>
        public List<ARDetailedAnnotation> Annotations
        {
            get => m_Annotations;
            set => m_Annotations = value;
        }

        /// <inheritdoc />
        protected override void OnEnable()
        {
            base.OnEnable();
            
            // Disable all annotations at first. For safety!
            foreach (var annotation in m_Annotations)
            {
                annotation.IsEnabled = false;
                annotation.annotationVisualization.SetActive(false);
            }
        }

        /// <inheritdoc />
        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractable(updatePhase);

            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
                UpdateVisualizations();
        }

        public ARDetailedAnnotation GetAnnotation(string name)
        {
            return m_Annotations.Find(annotation => annotation.Name == name);
        }

        public void ActivateAnnotation(string[] annotationNames)
        {
            // Disable all annotations at first
            foreach (var annotation in m_Annotations)
            {
                annotation.IsEnabled = false;
            }

            // Enable the annotations that are passed in
            foreach (var name in annotationNames)
            {
                m_Annotations.Find(ann => ann.Name == name).IsEnabled = true;
            }
        }

        void UpdateVisualizations()
        {
            // Disable all annotations if not hovered.
            if (!isHovered)
            {
                foreach (var annotation in m_Annotations)
                {
                    annotation.annotationVisualization.SetActive(false);
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

                foreach (var annotation in m_Annotations)
                {
                    // If annotation is disabled, simply hide the visual and skip the heavy processing.
                    if (!annotation.IsEnabled)
                    {
                        annotation.annotationVisualization.SetActive(false);
                        continue;
                    }
                    
                    var enableThisFrame =
                        (Mathf.Acos(dotProd) < annotation.maxFOVCenterOffsetAngle &&
                        distSquare >= Mathf.Pow(annotation.minAnnotationRange, 2f) &&
                        distSquare < Mathf.Pow(annotation.maxAnnotationRange, 2f));
                    if (annotation.annotationVisualization != null)
                    {
                        if (enableThisFrame && !annotation.annotationVisualization.activeSelf)
                            annotation.annotationVisualization.SetActive(true);
                        else if (!enableThisFrame && annotation.annotationVisualization.activeSelf)
                            annotation.annotationVisualization.SetActive(false);

                        // If enabled, align to camera
                        if (annotation.annotationVisualization.activeSelf)
                        {
                            annotation.annotationVisualization.transform.rotation =
                                Quaternion.LookRotation(fromCamera, transform.up);
                        }
                    }
                }
            }
        }
    }
}
