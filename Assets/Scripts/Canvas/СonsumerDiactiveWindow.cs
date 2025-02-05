using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Playground2D.Game.Stats;

namespace Playground2D.Canvas.GameWindows
{
    public class СonsumerDiactiveWindow : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Text _biofluidText;
        [SerializeField] private Text _darkEnergyText;
        [SerializeField] private Text _photonFlowText;
        [SerializeField] private Text _starPlasmaText;

        [SerializeField] private Button _biofluidIncreaseButton;
        [SerializeField] private Button _biofluidDecreaseButton;
        [SerializeField] private Button _darkEnergyIncreaseButton;
        [SerializeField] private Button _darkEnergyDecreaseButton;
        [SerializeField] private Button _photonFlowIncreaseButton;
        [SerializeField] private Button _photonFlowDecreaseButton;
        [SerializeField] private Button _starPlasmaIncreaseButton;
        [SerializeField] private Button _starPlasmaDecreaseButton;

        [SerializeField]private GameStats _gameStats;

        private int _biofluidValue = 0;
        private int _darkEnergyValue = 0;
        private int _photonFlowValue = 0;
        private int _starPlasmaValue = 0;

        private bool _isBiofluidIncreasePressed = false;
        private bool _isBiofluidDecreasePressed = false;
        private bool _isDarkEnergyIncreasePressed = false;
        private bool _isDarkEnergyDecreasePressed = false;
        private bool _isPhotonFlowIncreasePressed = false;
        private bool _isPhotonFlowDecreasePressed = false;
        private bool _isStarPlasmaIncreasePressed = false;
        private bool _isStarPlasmaDecreasePressed = false;


        private void Start()
        {

            _biofluidIncreaseButton.onClick.AddListener(IncreaseBiofluid);
            _biofluidDecreaseButton.onClick.AddListener(DecreaseBiofluid);
            _darkEnergyIncreaseButton.onClick.AddListener(IncreaseDarkEnergy);
            _darkEnergyDecreaseButton.onClick.AddListener(DecreaseDarkEnergy);
            _photonFlowIncreaseButton.onClick.AddListener(IncreasePhotonFlow);
            _photonFlowDecreaseButton.onClick.AddListener(DecreasePhotonFlow);
            _starPlasmaIncreaseButton.onClick.AddListener(IncreaseStarPlasma);
            _starPlasmaDecreaseButton.onClick.AddListener(DecreaseStarPlasma);

            UpdateTexts();
        }

        private void Update()
        {
            if (_isBiofluidIncreasePressed)
            {
                _biofluidValue = _gameStats._biofluidForm;
                UpdateTexts();
            }
            if (_isBiofluidDecreasePressed)
            {
                _biofluidValue = 0;
                UpdateTexts();
            }
            if (_isDarkEnergyIncreasePressed)
            {
                _darkEnergyValue = _gameStats._darkEnergyForm;
                UpdateTexts();
            }
            if (_isDarkEnergyDecreasePressed)
            {
                _darkEnergyValue = 0;
                UpdateTexts();
            }
            if (_isPhotonFlowIncreasePressed)
            {
                _photonFlowValue = _gameStats._photonFlowForm;
                UpdateTexts();
            }
            if (_isPhotonFlowDecreasePressed)
            {
                _photonFlowValue = 0;
                UpdateTexts();
            }
            if (_isStarPlasmaIncreasePressed)
            {
                _starPlasmaValue = _gameStats._starPlasmaForm;
                UpdateTexts();
            }
            if (_isStarPlasmaDecreasePressed)
            {
                _starPlasmaValue = 0;
                UpdateTexts();
            }
        }

        private void IncreaseBiofluid()
        {
            _biofluidValue = Mathf.Min(_biofluidValue + 1, _gameStats._biofluidForm);
            UpdateTexts();
        }

        private void DecreaseBiofluid()
        {
            _biofluidValue = Mathf.Max(_biofluidValue - 1, 0);
            UpdateTexts();
        }

        private void IncreaseDarkEnergy()
        {
            _darkEnergyValue = Mathf.Min(_darkEnergyValue + 1, _gameStats._darkEnergyForm);
            UpdateTexts();
        }

        private void DecreaseDarkEnergy()
        {
            _darkEnergyValue = Mathf.Max(_darkEnergyValue - 1, 0);
            UpdateTexts();
        }

        private void IncreasePhotonFlow()
        {
            _photonFlowValue = Mathf.Min(_photonFlowValue + 1, _gameStats._photonFlowForm);
            UpdateTexts();
        }

        private void DecreasePhotonFlow()
        {
            _photonFlowValue = Mathf.Max(_photonFlowValue - 1, 0);
            UpdateTexts();
        }

        private void IncreaseStarPlasma()
        {
            _starPlasmaValue = Mathf.Min(_starPlasmaValue + 1, _gameStats._starPlasmaForm);
            UpdateTexts();
        }

        private void DecreaseStarPlasma()
        {
            _starPlasmaValue = Mathf.Max(_starPlasmaValue - 1, 0);
            UpdateTexts();
        }

        private void UpdateTexts()
        {
            _biofluidText.text = $"{_biofluidValue}/{_gameStats._biofluidForm}";
            _darkEnergyText.text = $"{_darkEnergyValue}/{_gameStats._darkEnergyForm}";
            _photonFlowText.text = $"{_photonFlowValue}/{_gameStats._photonFlowForm}";
            _starPlasmaText.text = $"{_starPlasmaValue}/{_gameStats._starPlasmaForm}";
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.pointerPress == _biofluidIncreaseButton.gameObject)
            {
                _isBiofluidIncreasePressed = true;
            }
            else if (eventData.pointerPress == _biofluidDecreaseButton.gameObject)
            {
                _isBiofluidDecreasePressed = true;
            }
            else if (eventData.pointerPress == _darkEnergyIncreaseButton.gameObject)
            {
                _isDarkEnergyIncreasePressed = true;
            }
            else if (eventData.pointerPress == _darkEnergyDecreaseButton.gameObject)
            {
                _isDarkEnergyDecreasePressed = true;
            }
            else if (eventData.pointerPress == _photonFlowIncreaseButton.gameObject)
            {
                _isPhotonFlowIncreasePressed = true;
            }
            else if (eventData.pointerPress == _photonFlowDecreaseButton.gameObject)
            {
                _isPhotonFlowDecreasePressed = true;
            }
            else if (eventData.pointerPress == _starPlasmaIncreaseButton.gameObject)
            {
                _isStarPlasmaIncreasePressed = true;
            }
            else if (eventData.pointerPress == _starPlasmaDecreaseButton.gameObject)
            {
                _isStarPlasmaDecreasePressed = true;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.pointerPress == _biofluidIncreaseButton.gameObject)
            {
                _isBiofluidIncreasePressed = false;
            }
            else if (eventData.pointerPress == _biofluidDecreaseButton.gameObject)
            {
                _isBiofluidDecreasePressed = false;
            }
            else if (eventData.pointerPress == _darkEnergyIncreaseButton.gameObject)
            {
                _isDarkEnergyIncreasePressed = false;
            }
            else if (eventData.pointerPress == _darkEnergyDecreaseButton.gameObject)
            {
                _isDarkEnergyDecreasePressed = false;
            }
            else if (eventData.pointerPress == _photonFlowIncreaseButton.gameObject)
            {
                _isPhotonFlowIncreasePressed = false;
            }
            else if (eventData.pointerPress == _photonFlowDecreaseButton.gameObject)
            {
                _isPhotonFlowDecreasePressed = false;
            }
            else if (eventData.pointerPress == _starPlasmaIncreaseButton.gameObject)
            {
                _isStarPlasmaIncreasePressed = false;
            }
            else if (eventData.pointerPress == _starPlasmaDecreaseButton.gameObject)
            {
                _isStarPlasmaDecreasePressed = false;
            }
        }
    }
}
