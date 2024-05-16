using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Animal
{

    protected override void Start()
    {
        nombre = "Fox";
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
