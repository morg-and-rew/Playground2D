using Playground2D.Game.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Playground2D.Canvas.StoringFormsWindows
{
    public class StoringFormsWindow : MonoBehaviour
    {
        [SerializeField] private Text _biofluidText;
        [SerializeField] private Text _darkEnergyText;
        [SerializeField] private Text _photonFlowText;
        [SerializeField] private Text _starPlasmaText;

        [SerializeField] private GameStats _gameStats;

        private void OnEnable()
        {
            GameStats.OnBiofluidFormChanged += UpdateBiofluidText;
            GameStats.OnDarkEnergyFormChanged += UpdateDarkEnergyText;
            GameStats.OnPhotonFlowFormChanged += UpdatePhotonFlowText;
            GameStats.OnStarPlasmaFormChanged += UpdateStarPlasmaText;

            _biofluidText.text = _gameStats._biofluidForm.ToString();
            _darkEnergyText.text = _gameStats._darkEnergyForm.ToString();
            _photonFlowText.text = _gameStats._photonFlowForm.ToString();
            _starPlasmaText.text = _gameStats._starPlasmaForm.ToString();
        }

        private void OnDisable()
        {
            GameStats.OnBiofluidFormChanged -= UpdateBiofluidText;
            GameStats.OnDarkEnergyFormChanged -= UpdateDarkEnergyText;
            GameStats.OnPhotonFlowFormChanged -= UpdatePhotonFlowText;
            GameStats.OnStarPlasmaFormChanged -= UpdateStarPlasmaText;
        }

        private void UpdateBiofluidText(int newValue)
        {
            if (_biofluidText != null)
            {
                _biofluidText.text = newValue.ToString();
            }
        }

        private void UpdateDarkEnergyText(int newValue)
        {
            if (_darkEnergyText != null)
            {
                _darkEnergyText.text = newValue.ToString();
            }
        }

        private void UpdatePhotonFlowText(int newValue)
        {
            if (_photonFlowText != null)
            {
                _photonFlowText.text = newValue.ToString();
            }
        }

        private void UpdateStarPlasmaText(int newValue)
        {
            if (_starPlasmaText != null)
            {
                _starPlasmaText.text = newValue.ToString();
            }
        }
    }
}
