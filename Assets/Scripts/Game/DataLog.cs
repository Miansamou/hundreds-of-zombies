using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game
{
    public class DataLog : MonoBehaviour
    {
        public static DataLog Instance;
        public GameObject logPanel, textContent;
        private const int MaxMessages = 10;
        private readonly List<Message> messages = new();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
        
        public void SendMessagesToLog(string text)
        {
            if (messages.Count >= MaxMessages)
            {
                Destroy(messages[0].textObject.gameObject);
                messages.Remove(messages[0]);
            }

            var newMessage = new Message
            {
                text = text
            };

            var newText = Instantiate(textContent, logPanel.transform);

            newMessage.textObject = newText.GetComponent<TMP_Text>();

            newMessage.textObject.text = newMessage.text;

            messages.Add(newMessage);
        }
    }

    [System.Serializable]
    public class Message
    {
        public string text;
        public TMP_Text textObject;
    }
}