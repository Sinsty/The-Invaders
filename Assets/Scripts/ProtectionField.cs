using UnityEngine;

public class ProtectionField : MonoBehaviour
{
    public void SwitchOff()
    {
        Debug.Log("Protection disabled");
        gameObject.SetActive(false);
    }
}
