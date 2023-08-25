using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2PressurePlatePuzzle : MonoBehaviour
{

    public Torch Torch;
    private PressurePlate plate;
    public GameObject[] objectsToActivate;

    
    void Start()
    {
        plate = GetComponent<PressurePlate>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Torch.isLit && plate.Activated)
        {
            plate.Activated = false;
        } else if (Torch.isLit && plate.Activated)
        {
            foreach (var item in objectsToActivate)
            {
                item.GetComponent<Activatable>().Activate();
            }
        }
    }
}
