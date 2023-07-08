using UnityEngine;

namespace Game
{
    public class Furnace : Machine
    {
        [SerializeField] private GameObject smokeParticles;
        [SerializeField] private GameObject tapa;
        
        protected override void StartProcessingItem()
        {
            base.StartProcessingItem();
            
            smokeParticles.SetActive(true);
            tapa.SetActive(true);
        }
        
        protected override void CreateItemsOnSlots()
        {
            base.CreateItemsOnSlots();
            
            smokeParticles.SetActive(false);
            tapa.SetActive(false);
        }
    }
}