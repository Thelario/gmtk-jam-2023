using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GoalItemUI : MonoBehaviour
    {
        [SerializeField] private Image goalItemImage;
        [SerializeField] private TMP_Text goalText;

        private Goal _goal;
        
        public void ConfigureGoalItem(Goal goal)
        {
            _goal = goal;
            goalItemImage.sprite = goal.goalItem.itemSprite;
            goalText.text = goal.currentAmount + "/" + goal.maxAmount;
        }

        public bool CheckGoal(Goal goal)
        {
            return _goal.goalItem == goal.goalItem;
        }

        public void UpdateGoalItemUI()
        {
            goalText.text = _goal.currentAmount + "/" + _goal.maxAmount;
        }
    }
}