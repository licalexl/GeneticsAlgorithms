using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conejo : Animal
{

    protected override void Start()
    {
        pointApareamiento = GameObject.Find("SpawnConejo");
        nombre = "Conejo";
        carnivoro = false;
        Generacion();
    }


    new public void GetFood() { }
    public override void GetWater() { }
    public override void GetPartner() { }
    public override void Run() { }
    public override void Eat() { }
    public override void DrinkWater() { }

}
