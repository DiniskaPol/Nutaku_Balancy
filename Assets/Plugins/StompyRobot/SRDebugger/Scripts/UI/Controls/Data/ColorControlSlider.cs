namespace SRDebugger.UI.Controls.Data
{
    using System;
    using SRF;
    using UnityEngine;
    using UnityEngine.UI;

    public class ColorControlSlider : DataBoundControl
    {
        [RequiredField] public Image Preview;
        [RequiredField] public Slider InputR;
        [RequiredField] public Slider InputG;
        [RequiredField] public Slider InputB;

        [RequiredField] public Text Title;

        protected override void Start()
        {
            base.Start();
            Clamp(InputR);
            Clamp(InputG);
            Clamp(InputB);

            InputR.onValueChanged.AddListener(OnValueChanged);
            InputG.onValueChanged.AddListener(OnValueChanged);
            InputB.onValueChanged.AddListener(OnValueChanged);
        }

        void Clamp(Slider s)
        {
            s.minValue = 0;
            s.maxValue = 255;
        }

        void OnValueChanged(float arg0)
        {
            try
            {
                var r = (byte)InputR.value;
                var g = (byte)InputG.value;
                var b = (byte)InputB.value;

                var c = new Color32(r, g, b, 255);
                UpdateValue(c);
                Preview.color = c;
                Title.text = PropertyName + ": " + c.ToString();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        byte Parse(InputField text)
        {
            try
            {
                return byte.Parse(text.text);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return 0;
            }
        }

        protected override void OnBind(string propertyName, Type t)
        {
            base.OnBind(propertyName, t);
            InputR.value = 0;
            InputG.value = 0;
            InputB.value = 0;
        }

        protected override void OnValueUpdated(object newValue)
        {
            var value = newValue == null ? new Color32(255, 255, 255, 255): (Color32) newValue;
            InputR.value = value.r;
            InputG.value = value.g;
            InputB.value = value.b;

            Preview.color = value;
            Title.text = PropertyName + ": " + value.ToString();
        }

        public override bool CanBind(Type type, bool isReadOnly)
        {
            return type == typeof (Color32) && !isReadOnly;
        }
    }
}
