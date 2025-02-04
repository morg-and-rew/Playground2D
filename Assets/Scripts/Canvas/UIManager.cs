using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Animator startingWindow;
    public Text userID;
    public void OnClickEvent()
    {
        startingWindow.SetBool("Start", true);
        Application.ExternalCall("handleUnitySyncUserIDEvent");
    }

    public void ChangeUserIDLabel(string tgID)
    {
        userID.text = tgID;
    }


}
