using UnityEngine;
using UnityEngine.SceneManagement;

namespace EnglishKids.BuildRobots
{
    public sealed class UIManager : MonoBehaviour
    {
        public GameObject InitialPanel { get; private set; }
        private GameObject _lastPanel;
        [SerializeField] private GameObject _initialPanel;

        private void Start()
        {
            InitialPanel = _initialPanel;
            EnablePanel(InitialPanel);
        }

        public void EnablePanel(GameObject panel)
        {
            if (_lastPanel != null)
                _lastPanel.SetActive(false);
            _lastPanel = panel;
            _lastPanel.SetActive(true);
        }

        public void Restart() //temp void
        {
            SceneManager.LoadScene("BuildRobots");
        }
    }
}