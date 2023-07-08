using System.Collections.Generic;
using Game.Managers;
using Game.UI;
using UnityEngine;

namespace Game
{
	[System.Serializable]
	public class Goal
	{
		public ItemSO goalItem;
		public int maxAmount;

		[HideInInspector] public int currentAmount;

		public void Update()
		{
			currentAmount++;
		}
		
		public bool IsCompleted()
		{
			return maxAmount <= currentAmount;
		}
	}
	
	public class GoalsManager : Managers.Singleton<GoalsManager>
	{
		[SerializeField] private GameObject goalItemPrefab;
		[SerializeField] private Transform goalItemsPanel;
		[SerializeField] private List<Goal> goals;

		private List<GoalItemUI> goalItemsUi;

		private void Start()
		{
			goalItemsUi = new List<GoalItemUI>();
			
			CreateGoalsUI();
		}

		private void CreateGoalsUI()
		{
			foreach (Goal goal in goals)
			{
				GoalItemUI giui = Instantiate(goalItemPrefab, goalItemsPanel).GetComponent<GoalItemUI>();
				giui.ConfigureGoalItem(goal);
				goalItemsUi.Add(giui);
			}
		}
		
		public void UpdateGoal(ItemSO itemSO)
		{
			foreach (Goal goal in goals)
			{
				if (goal.goalItem != itemSO)
					continue;

				goal.Update();
				UpdateGoalUI(goal);
			}

			if (!GoalsAreCompleted())
				return;
			
			TimeManager.Instance.Pause();
			CanvasManager.Instance.SwitchCanvas(CanvasType.GameLevelVictoryMenu);
		}

		private bool GoalsAreCompleted()
		{
			foreach (Goal goal in goals)
			{
				if (!goal.IsCompleted())
					return false;
			}

			return true;
		}

		private void UpdateGoalUI(Goal goal)
		{
			foreach (GoalItemUI giui in goalItemsUi)
			{
				if (giui.CheckGoal(goal))
					giui.UpdateGoalItemUI();
			}
		}
	}
}