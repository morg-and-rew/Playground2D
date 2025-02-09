using UnityEngine;

namespace Playground2D.Game.Stats
{
    public class GameStats : MonoBehaviour, ICollectble
    {
        public static GameStats Instance { get; private set; }

        [field: SerializeField] public int _photonFlowForm { get; private set; } = 0;
        [field: SerializeField] public int _starPlasmaForm { get; private set; } = 0;
        [field: SerializeField] public int _biofluidForm { get; private set; } = 0;
        [field: SerializeField] public int _darkEnergyForm { get; private set; } = 0;
        [field: SerializeField] public int _antiMaterial { get; private set; } = 0;

        public delegate void FormChanged(int newValue);
        public static event FormChanged OnBiofluidFormChanged;
        public static event FormChanged OnDarkEnergyFormChanged;
        public static event FormChanged OnPhotonFlowFormChanged;
        public static event FormChanged OnStarPlasmaFormChanged;
        public static event FormChanged OnAntiMaterialChanged;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public void CollectForm(int biofluid = 0, int darkEnergy = 0, int photonFlow = 0, int starPlasma = 0, int antiMaterial = 0)
        {
            if (biofluid > 0)
            {
                _biofluidForm += biofluid;
                OnBiofluidFormChanged?.Invoke(_biofluidForm);
            }

            if (darkEnergy > 0)
            {
                _darkEnergyForm += darkEnergy;
                OnDarkEnergyFormChanged?.Invoke(_darkEnergyForm);
            }

            if (photonFlow > 0)
            {
                _photonFlowForm += photonFlow;
                OnPhotonFlowFormChanged?.Invoke(_photonFlowForm);
            }

            if (starPlasma > 0)
            {
                _starPlasmaForm += starPlasma;
                OnStarPlasmaFormChanged?.Invoke(_starPlasmaForm);
            }

            if (antiMaterial > 0)
            {
                _antiMaterial += antiMaterial;
                OnAntiMaterialChanged?.Invoke(_antiMaterial);
            }
        }

        public void SpendForm(int biofluid = 0, int darkEnergy = 0, int photonFlow = 0, int starPlasma = 0, int antiMaterial = 0)
        {
            if (biofluid > 0)
            {
                _biofluidForm -= biofluid;
                OnBiofluidFormChanged?.Invoke(_biofluidForm);
            }

            if (darkEnergy > 0)
            {
                _darkEnergyForm -= darkEnergy;
                OnDarkEnergyFormChanged?.Invoke(_darkEnergyForm);
            }

            if (photonFlow > 0)
            {
                _photonFlowForm -= photonFlow;
                OnPhotonFlowFormChanged?.Invoke(_photonFlowForm);
            }

            if (starPlasma > 0)
            {
                _starPlasmaForm -= starPlasma;
                OnStarPlasmaFormChanged?.Invoke(_starPlasmaForm);
            }

            if (antiMaterial > 0)
            {
                _antiMaterial -= antiMaterial;
                OnAntiMaterialChanged?.Invoke(_antiMaterial);
            }
        }
    }
}