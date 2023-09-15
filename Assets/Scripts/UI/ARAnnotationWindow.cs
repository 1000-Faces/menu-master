using DineEase.AR;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DineEase.UI
{
    public abstract class ARAnnotationWindow : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI m_Title;

        public string Title
        {
            get => m_Title.text;
            set => m_Title.text = value;
        }

        [SerializeField] Button m_CloseButton;

        [SerializeField] ARDetailedAnnotation m_Annotation;

        public ARDetailedAnnotation Annotation
        {
            get => m_Annotation;
            set => m_Annotation = value;
        }


        /// <inheritdoc />
        protected virtual void Awake()
        {
            // check if the annotation is null
            if (m_Annotation == null)
            {
                throw new NoNullAllowedException("Annotation cannot be null");
            }

            // set the visualization GameObject to this
            m_Annotation.AnnotationVisualization = gameObject;

            // Disable the annotation at first. For safety!
            m_Annotation.IsEnabled = false;
            gameObject.SetActive(false);
        }

        /// <inheritdoc />
        protected virtual void Start()
        {
            // handling events
            m_CloseButton.onClick.AddListener(Close);
        }

        public virtual void Open()
        {
            // enable the annotation
            m_Annotation.IsEnabled = true;
        }

        public virtual void Close()
        {
            // disable the annotation
            m_Annotation.IsEnabled = false;
        }
    }
}
