using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    private int health;
    protected string name;

    public Data(int health, string name)
    {
        this.health = health;
        this.name = name;
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
}

public class States : MonoBehaviour
{
    Data data=new Data(100,"Dinasaur");
    public int attack=30;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Attack ������ ���� " + attack);
        Debug.Log("Data�� �̸� "+data.Name);
    }

   
    
}
