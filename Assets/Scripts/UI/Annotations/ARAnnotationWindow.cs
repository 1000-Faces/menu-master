using DineEase.AR;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DineEase.UI
{
    public abstract class ARAnnotationWindow : FormWindow
    {
        [SerializeField] ARDetailedAnnotation m_Annotation;

        public ARDetailedAnnotation Annotation
        {
            get => m_Annotation;
            set => m_Annotation = value;
        }

        /// <inheritdoc />
        protected virtual void Awake()
        {
            // set the visualization GameObject to this
            m_Annotation.AnnotationVisualization = gameObject;

            // Disable the annotation at first. For safety!
            m_Annotation.IsEnabled = false;
            gameObject.SetActive(false);
        }

        public override void Open(string title)
        {
            // enable the annotation
            m_Annotation.IsEnabled = true;

            base.Open(title);
        }

        protected override void Hide()
        {
            // disable the annotation
            m_Annotation.IsEnabled = false;

            base.Hide();
        }
    }
}
