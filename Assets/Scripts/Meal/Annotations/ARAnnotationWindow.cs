using DineEase.AR;
using DineEase.UI;
using UnityEngine;

namespace DineEase.Meal.Annotation
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

        public override void Open()
        {
            // enable the annotation
            m_Annotation.IsEnabled = true;
        }

        protected override void Hide()
        {
            // disable the annotation
            m_Annotation.IsEnabled = false;

            base.Hide();
        }
    }
}
