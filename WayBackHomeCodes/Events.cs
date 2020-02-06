using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Events : MonoBehaviour {

    public GameObject radio;
    public GameObject Vcam;
    public float speed;
    bool radioControl = true;
    public GameObject mediceCabin;
    [Space]
    public GameObject lantern;
    public Text objIndicator;
    public GameObject radioTable;
    [Space]
    public GameObject hatchRoomevent;
    public GameObject panel;
    [Space]
    public GameObject commRoomEvent;
    public GameObject food;
    [Space]
    public GameObject toolbox;
    [Space]
    public GameObject engine;
    bool open=true;
    bool Panelchange = true;
    bool finalChange = true;
    bool engineChange = true;
    bool foodChange = true;
    bool ControlChange = true;
    

    public void RadioTable()
    {
        if (radioControl == true)
        {
            if (Vcam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize <= 5.5f)
            {

                Vcam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize += speed * Time.deltaTime;
                radio.GetComponent<FamilyPhoto>().eventTalks = true;

            }
            if (Vcam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize >= 5.5f)
            {
                Vcam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 5.5f;
                radioTable.GetComponent<FamilyPhoto>().after = true;
                objIndicator.text = "OBJECTIVE:Go to the Sick Bay and use the Medicine Cabinet.";
                mediceCabin.GetComponent<FamilyPhoto>().enabled = true;
                radioControl = false;
                
            }
        }

        
        
        
    }

    public void Lantern()
    {
        lantern.SetActive(true);
        Character.animator.Play("LanternStop");
        objIndicator.text = "OBJECTIVE:Go to the Communication Room using the map (M key) and use the radio.";
        radioTable.GetComponent<FamilyPhoto>().after = false;

        Character.Lwalk = true;
        Destroy(GameObject.FindGameObjectWithTag("lantern"));
    }

    public void medicineCabin()
    {
        
        if (Panelchange == true)
        {
            objIndicator.text = "OBJECTIVE:Go to the control room and use the Control Panel";
            hatchRoomevent.SetActive(true);
            ControlPanel.objective1 = true;
            Panelchange = false;
        }
        
    }

    public void Engine()
    {
        if (engineChange == true)
        {
            objIndicator.text = "OBJECTIVE:Go to the Kithcen and eat something.";
            commRoomEvent.SetActive(true);
            food.GetComponent<KitchenEvent>().enabled = true;
            engineChange = false;
        }
        
        
        
    }

    public void Food()
    {
        if (foodChange == true)
        {
            toolbox.GetComponent<FamilyPhoto>().enabled = true;
            objIndicator.text = "Go to the Supply Room and take your toolbox.";
            foodChange = false;
        }
        

    }

    public void ToolBox()
    {
        
        if (open == true)
        {
            objIndicator.text = "Go to the Engine Room and fix the engine.";
            engine.GetComponent<EngineRepair>().enabled = true;
            engine.GetComponent<FamilyPhoto>().enabled = false;
            toolbox.SetActive(false);
            open = false;
        }
        
    }
    
    public void Panel()
    {
        if (ControlChange == true)
        {
            objIndicator.text = "OBJECTIVE:Go to the Engine room and take a look at the engine.";
            ControlChange = false;
        }
        
    }

    public void Eng()
    {
        objIndicator.text = "OBJECTIVE:Go to the control room and use the Control Panel";
        if (finalChange == true)
        {
            ControlPanel.objective2 = true;
            finalChange = false;
        }
    }

    public void exit()
    {
        Application.Quit();
    }
}
