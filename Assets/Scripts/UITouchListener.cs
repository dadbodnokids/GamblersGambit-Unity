using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Scott.ScratchCard
{
   [RequireComponent(typeof(RectTransform))]
   public class UITouchListener : MonoBehaviour
    {
        public Camera uiCamera;
        [SerializeField]
        public TouchEvent OnTouch;

        RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, uiCamera, out Vector2 localPosInRect);
                OnTouch.Invoke((int)localPosInRect.x, (int)localPosInRect.y);
            }
        }
    }
    [System.Serializable]
    public class TouchEvent : UnityEvent<int, int> { }
}
