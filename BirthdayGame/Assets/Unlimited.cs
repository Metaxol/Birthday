using UnityEngine;

public class Unlimited : MonoBehaviour {

    private LevelManagement GetLevelManagement;

    private bool ControlBool = false;

    private void Start()
    {
        GetLevelManagement = FindObjectOfType<LevelManagement>();
    }

    private void Update()
    {
        if (ControlBool)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Application.Quit();
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {

            }
        }

        if(GetLevelManagement.Enemies_Killed >= 100)
        {
            
            //Make an Unlimited - Option.
        }
    }

    private void ChooseUnlimited()
    {
        GetComponent<Animator>().speed = 0;
        ControlBool = true; //Continue here
    }
}
