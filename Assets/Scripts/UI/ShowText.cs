using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ShowText : MonoBehaviour
    {
        [TextArea(3, 10)] public string fullText;
        private const float Delay = 0.01f;
        private string currentText = "";
        private TMP_Text displayText;
        private bool endText;

        private void Awake()
        {
            displayText = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            StartCoroutine(WaitUpdateText());
        }

        private IEnumerator TextAppearing()
        {
            for (var i = 0; i <= fullText.Length; i++)
            {
                if (endText) yield break;
                currentText = fullText[..i];
                displayText.text = currentText;
                yield return new WaitForSeconds(Delay);
            }
        }

        private IEnumerator WaitUpdateText()
        {
            yield return new WaitForSeconds(Delay);
            displayText.text = "";
            StartCoroutine(TextAppearing());
        }

        public bool FinishedText()
        {
            return fullText.Length == displayText.text.Length;
        }

        public void ShowAllText()
        {
            endText = true;
            displayText.text = fullText;
        }

        public string SetterFullText
        {
            set => fullText = value;
        }
    }
}