namespace SRDebugger.UI.Controls.Data
{
    using System;
    using SRF;
    using UnityEngine;
    using UnityEngine.UI;

    public class ColorControl : DataBoundControl
    {
        [RequiredField] public Image Preview;
        [RequiredField] public InputField InputR;
        [RequiredField] public InputField InputG;
        [RequiredField] public InputField InputB;

        [RequiredField] public Text Title;

        protected override void Start()
        {
            base.Start();
            InputR.onValueChanged.AddListener(OnValueChanged);
            InputG.onValueChanged.AddListener(OnValueChanged);
            InputB.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(string _)
        {
            try
            {
                var r = Parse(InputR);
                var g = Parse(InputG);
                var b = Parse(InputB);

                var c = new Color32(r, g, b, 255);
                UpdateValue(c);
                Preview.color = c;
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
            Title.text = propertyName + " (RGB, [0; 255])";
            InputR.text = "";
            InputG.text = "";
            InputB.text = "";
        }

        protected override void OnValueUpdated(object newValue)
        {
            var value = newValue == null ? new Color32(255, 255, 255, 255): (Color32) newValue;
            InputR.text = value.r.ToString();
            InputG.text = value.g.ToString();
            InputB.text = value.b.ToString();

            Preview.color = value;
        }

        public override bool CanBind(Type type, bool isReadOnly)
        {
            return type == typeof (Color32) && !isReadOnly;
        }
    }
}
