﻿using System.Collections;
using UnityEngine;

namespace Game
{
    public class Cleaver : Machine
    {
        [SerializeField] private GameObject cleaver;
        [SerializeField] private float yStartPos;
        [SerializeField] private float yFinalPos;
        [SerializeField] private float timeToDrop;
        [SerializeField] private float timeToComeBackUp;
        
        protected override void CreateItemsOnSlots()
        {
            StartCoroutine(Co_WaitForCleaverToDrop());
        }

        private IEnumerator Co_WaitForCleaverToDrop()
        {
            LeanTween.moveLocalY(cleaver, yFinalPos, timeToDrop);
            
            yield return new WaitForSeconds(timeToDrop);
            
            LeanTween.moveLocalY(cleaver, yStartPos, timeToComeBackUp);
            base.CreateItemsOnSlots();
        }
    }
}