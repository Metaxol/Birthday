using UnityEngine;

public class GoBack : MonoBehaviour {

    [HideInInspector] public int Control = 0;
    private LevelManagement GetLevelManagement;

    private void Awake()
    {
        GetLevelManagement = FindObjectOfType<LevelManagement>();
    }

    private void Update()
    {
        print(Control);
    }

    private void GoBackAfterUpgrade()
    {
        if(Control == 0)
        {
            GetComponent<Animation>()["Upgrade_Holder"].speed = 0;
        }
        else
        {
            GetComponent<Animation>()["Upgrade_Holder"].speed = 1;
        }
    }

    private void Revert()
    {
        Control = 0;
        GetLevelManagement.Panel.SetActive(false);
        gameObject.SetActive(false);
    }
}
