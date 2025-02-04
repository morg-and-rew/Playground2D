using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Playground2D.Canvas.StoringFormsWindows
{
    public class StoringFormsWindow : MonoBehaviour
    {
        [SerializeField] private Text biofluidText;
        [SerializeField] private Text darkEnergyText;
        [SerializeField] private Text photonFlowText;
        [SerializeField] private Text starPlasmaText;

        private void OnEnable()
        {
            GameStats.OnBiofluidFormChanged += UpdateBiofluidText;
            GameStats.OnDarkEnergyFormChanged += UpdateDarkEnergyText;
            GameStats.OnPhotonFlowFormChanged += UpdatePhotonFlowText;
            GameStats.OnStarPlasmaFormChanged += UpdateStarPlasmaText;
        }

        private void OnDisable()
        {
            GameStats.OnBiofluidFormChanged -= UpdateBiofluidText;
            GameStats.OnDarkEnergyFormChanged -= UpdateDarkEnergyText;
            GameStats.OnPhotonFlowFormChanged -= UpdatePhotonFlowText;
            GameStats.OnStarPlasmaFormChanged -= UpdateStarPlasmaText;
        }

        private void UpdateBiofluidText(float newValue)
        {
            if (biofluidText != null)
            {
                biofluidText.text = newValue.ToString();
            }
        }

        private void UpdateDarkEnergyText(float newValue)
        {
            if (darkEnergyText != null)
            {
                darkEnergyText.text = newValue.ToString();
            }
        }

        private void UpdatePhotonFlowText(float newValue)
        {
            if (photonFlowText != null)
            {
                photonFlowText.text = newValue.ToString();
            }
        }

        private void UpdateStarPlasmaText(float newValue)
        {
            if (starPlasmaText != null)
            {
                starPlasmaText.text = newValue.ToString();
            }
        }
    }
}
