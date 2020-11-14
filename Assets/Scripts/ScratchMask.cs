using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Scott.ScratchCard
{
    [System.Serializable]
    public class ScratchMaskEvent : UnityEvent<ScratchMask> { }

    [RequireComponent(typeof(RectTransform), typeof(Image))]
    public class ScratchMask : MonoBehaviour
    {
        #region Declarations
        public int scratchRadius = 10;
        public UnityEvent OnScratch;
        public ScratchMaskEvent OnScratched;

        Texture2D maskTexture;
        RectTransform rectTransform;
        int width;
        int height;

        #endregion

        #region Initialization
        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            width = (int)rectTransform.rect.width;
            height = (int)rectTransform.rect.height;
            Reset();
        }

        private void Reset()
        {
            maskTexture = new Texture2D(width, height);
            Color32[] cols = maskTexture.GetPixels32();
            for (int i = 0; i < cols.Length; i++)
            {
                cols[i] = new Color32(0, 0, 0, 255);
            }
            maskTexture.SetPixels32(cols);
            Image image = GetComponent<Image>();
            image.material = new Material(image.material);
            image.material.mainTexture = maskTexture;
            maskTexture.Apply(false);
        }
        #endregion

        #region Scratching
        public void OnMouseOver(BaseEventData baseEvent) {
            PointerEventData ev = baseEvent as PointerEventData;
            if (ev.button == PointerEventData.InputButton.Left) {
                Vector2 localPoint;
                Camera cam = Camera.main;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, ev.position, cam, out localPoint);
                Scratch((int)localPoint.x, (int)localPoint.y);
            }
        }

        public void Scratch(int xCenter, int yCenter)
        {
            int xOffset, yOffset, xPos, yPos, yRange;
            Color32[] tempArray = maskTexture.GetPixels32();
            bool hasChanged = false;

            for (xOffset = -scratchRadius; xOffset <= scratchRadius; xOffset++)
            {
                yRange = (int)Mathf.Ceil(Mathf.Sqrt(scratchRadius * scratchRadius - xOffset * xOffset));
                for (yOffset = -yRange; yOffset <= yRange; yOffset++)
                {
                    xPos = xCenter + xOffset;
                    yPos = yCenter + yOffset;
                    hasChanged = TryScratchPixel(xPos, yPos, ref tempArray) || hasChanged;
                }
            }
            if (hasChanged)
            {
                OnScratch.Invoke();
                OnScratched.Invoke(this);
                maskTexture.SetPixels32(tempArray);
                maskTexture.Apply(false);
            }
        }

        public bool TryScratchPixel(int xPos, int yPos, ref Color32[] pixels)
        {
            if (xPos >= 0 && xPos < width && yPos >= 0 && yPos < height)
            {
                int index = yPos * width + xPos;
                if (pixels[index].a != 0)
                {
                    pixels[index].a = 0;
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
