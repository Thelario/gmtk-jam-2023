using Game.Managers;
using UnityEngine;

namespace Game
{
    public class Furnace : Machine
    {
        [SerializeField] private GameObject smokeParticles;
        [SerializeField] private GameObject tapa;
        
        protected override void StartProcessingItem()
        {
            SoundManager.Instance.PlaySound(SoundType.FuegoHorno);
            
            base.StartProcessingItem();
            
            smokeParticles.SetActive(true);
            tapa.SetActive(true);
        }
        
        protected override void CreateItemsOnSlots()
        {
            base.CreateItemsOnSlots();
            
            SoundManager.Instance.PlaySound(SoundType.FuegoHornoTerminado);
            smokeParticles.SetActive(false);
            tapa.SetActive(false);
        }
    }
}