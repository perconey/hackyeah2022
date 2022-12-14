using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderManager : MonoBehaviour
{
    [field:SerializeField] public SOProductGroup AllProducts { get; private set; }


    public Single MaxItemsPerCustomer = 5f;
    public Single MinItemsPerCustomer = 1f;
    public Single DifficultyRaiseSpeed = 0.1f;

    private Single CurrentItemsApproxPerCustomer;

    public static OrderManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(Instance);
    }

    public void Start()
    {
        CurrentItemsApproxPerCustomer = MinItemsPerCustomer;

        #region TEST

        #endregion TEST
    }

    public SOProduct[] GetOrder()
    {
        List<SOProduct> products = new List<SOProduct>();

        //itemki z zakresu (x, x+1)
        CurrentItemsApproxPerCustomer += DifficultyRaiseSpeed;
        Int32 realMin = (Int32)Math.Floor(CurrentItemsApproxPerCustomer);
        Int32 realMax = (Int32)Math.Floor(CurrentItemsApproxPerCustomer + 1f);

        if (realMin > MaxItemsPerCustomer - 1)
            realMin = (Int32)MaxItemsPerCustomer - 1;

        if (realMax > MaxItemsPerCustomer)
            realMax = (Int32)MaxItemsPerCustomer;

        //losuje ilo�� itemk�w
        Int32 numberOfItems = Convert.ToInt32(Random.Range(realMin, realMax + 1));

        if (numberOfItems == 0)
            numberOfItems = 1;

        //losuje konkretne taski
        for (int i = 0; i < numberOfItems; i++)
        {
            Int32 randomedItemIndex = Random.Range(0, AllProducts.Products.Length-2);
            SOProduct newProduct = AllProducts.Products[randomedItemIndex];
            products.Add(newProduct);
        }
        

        return products.ToArray();
    }
}
