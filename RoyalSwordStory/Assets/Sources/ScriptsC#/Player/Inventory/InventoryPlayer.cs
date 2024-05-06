using System.Collections.Generic;
using UnityEngine;

public class InventoryPlayer : MonoBehaviour
{
    [SerializeField] private List<CellInventory> Cells = new List<CellInventory>();

    private int _maxItemCountCell = 64;

    public bool AddItem(Item item, int count)
    {
        int countItem = count;
        int remains = 0;

        if (item.IsStack)
        {
            for (int i = 0; i < Cells.Count; i++)
            {
                if (Cells[i].HasItem())
                {
                    if (Cells[i].GetItemType() == item.Type)
                    {
                        if (Cells[i].GetCountOblectCell() + countItem > _maxItemCountCell)
                        {
                            // Cells[i].SetCountOblectCell(_maxItemCountCell);
                            remains = Cells[i].GetCountOblectCell() + countItem - _maxItemCountCell;
                            Cells[i].AddItem(item, countItem - remains);
                            countItem = remains;
                        }
                        else
                        {
                            Cells[i].AddItem(item, countItem);
                            return true;
                        }
                    }
                }
            }
        }
    

        for (int i = 0; i < Cells.Count; i++)
        {
            if (Cells[i].HasItem() == false)
            {
                Cells[i].AddItem(item, countItem);
                return true;
            }
        }

        return false;
    }
}
