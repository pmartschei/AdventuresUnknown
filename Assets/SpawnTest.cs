using AdventuresUnknownSDK.Core.Objects.Enemies;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Objects.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTest : MonoBehaviour
{

    public Enemy enemy;

    public void Spawn()
    {
        if (!enemy) return;
        EnemyModel instance = Instantiate(enemy.Model);
        instance.Enemy = enemy;
    }
    public void Create()
    {
        Item item = ObjectsManager.FindObjectByIdentifier<Item>("core.items.gems.basic");
        ItemStack itemStack = item.CreateItem();
        Inventory inventory = ObjectsManager.FindObjectByIdentifier<Inventory>("core.inventories.equipment");
        inventory.AddItemStack(itemStack);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
