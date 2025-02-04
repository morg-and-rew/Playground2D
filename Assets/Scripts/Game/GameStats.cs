using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour//, ICollectble
{
    [field: SerializeField] public int _biofluidForm { get; private set; } = 0;
    [field: SerializeField] public int _darkEnergyForm { get; private set; } = 0;
    [field: SerializeField] public int _photonFlowForm { get; private set; } = 0;
    [field: SerializeField] public int _starPlasmaForm { get; private set; } = 0;

    public delegate void FormChanged(float newValue);
    public static event FormChanged OnBiofluidFormChanged;
    public static event FormChanged OnDarkEnergyFormChanged;
    public static event FormChanged OnPhotonFlowFormChanged;
    public static event FormChanged OnStarPlasmaFormChanged;

    public void CollectForm(int biofluid, int darkEnergy, int photonFlow, int starPlasma)
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
    }
}
