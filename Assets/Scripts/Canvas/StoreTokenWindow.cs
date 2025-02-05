using Playground2D.Game.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Playground2D.Canvas.StoreTokenWindows
{
    public class StoreTokenWindow : MonoBehaviour
    {
        [SerializeField] GameStats _gameStats;
        [SerializeField] private Text _antiMaterialText;

        private void OnEnable()
        {
            GameStats.OnAntiMaterialChanged += UpdateAntiMaterialText;
            _antiMaterialText.text = _gameStats._antiMaterial.ToString();
        }

        private void OnDisable()
        {
            GameStats.OnAntiMaterialChanged -= UpdateAntiMaterialText;
        }

        public void OnButton1Clicked()
        {
            _gameStats.CollectForm(antiMaterial: 5);
        }

        public void OnButton2Clicked()
        {
            _gameStats.CollectForm(antiMaterial: 10);
        }

        public void OnButton3Clicked()
        {
            _gameStats.CollectForm(antiMaterial: 15);
        }

        public void OnButton4Clicked()
        {
            _gameStats.CollectForm(antiMaterial: 20);
        }

        private void UpdateAntiMaterialText(int newValue)
        {
            _antiMaterialText.text = newValue.ToString();
        }
    }
}