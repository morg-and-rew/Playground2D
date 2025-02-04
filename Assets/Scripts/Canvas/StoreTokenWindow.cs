using UnityEngine;

public interface IFormCollector
{
    void CollectForm(int biofluid = 0, int darkEnergy = 0, int photonFlow = 0, int starPlasma = 0, int antiMaterial = 0);
}

public class StoreTokenWindow : MonoBehaviour
{
    private IFormCollector _formCollector;

    public void Initialize(IFormCollector formCollector)
    {
        _formCollector = formCollector;
    }

    public void OnButton1Clicked()
    {
        _formCollector.CollectForm(antiMaterial: 5);
    }

    public void OnButton2Clicked()
    {
        _formCollector.CollectForm(antiMaterial: 10);
    }

    public void OnButton3Clicked()
    {
        _formCollector.CollectForm(antiMaterial: 15);
    }

    public void OnButton4Clicked()
    {
        _formCollector.CollectForm(antiMaterial: 20);
    }
}