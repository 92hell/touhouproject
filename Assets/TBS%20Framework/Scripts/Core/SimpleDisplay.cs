using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SimpleDisplay : MonoBehaviour {

	public CellGrid CellGrid; 
	public GameObject UnitsParent;
	public GameObject EndTurnButton;
	public Image UnitImage;
	public Text InfoText;
	public Text StatsText;
	public Text UnitNameText;
	//public Text PointsText;
	public Text TerrainName;
	public Text TerrainType;
	public bool IsUnitSelected;	
	void Start()
	{
		UnitImage.color = Color.gray;
		CellGrid.GameStarted += OnGameStarted;
		CellGrid.TurnEnded += OnTurnEnded;  
			EndTurnButton.GetComponent<Button>().onClick.AddListener(()=>{
					CellGrid.EndTurn();
				}); 
		//CellGrid.GameEnded += OnGameEnded;
	}
		void Update () {  
			//User ends his turn by pressing "n" on keyboard. 
				EndTurnButton.GetComponent<Button>().onClick.AddListener(()=>{
					CellGrid.EndTurn();
				});

	}

	private void OnGameStarted(object sender, EventArgs e)
	{
		foreach (Transform unit in UnitsParent.transform)
		{
			unit.GetComponent<Unit>().UnitHighlighted += OnUnitHighlighted;
			unit.GetComponent<Unit>().UnitDehighlighted += OnUnitDehighlighted;
			unit.GetComponent<Unit>().UnitAttacked += OnUnitAttacked;

		}

		foreach (Transform cell in CellGrid.transform)
		{
			cell.GetComponent<Cell>().CellHighlighted += OnCellHighlighted;
			cell.GetComponent<Cell>().CellDehighlighted += OnCellDehighlighted;
		}
	//OnTurnEnded(sender,e);
	}

	//private void OnGameEnded(object sender, EventArgs e)
	//{
	//	InfoText.text = "Player " + ((sender as CellGrid).CurrentPlayerNumber + 1) + " wins!";
	//}
	private void OnTurnEnded(object sender, EventArgs e)
	{
	//	NextTurnButton.interactable = ((sender as CellGrid).CurrentPlayer is HumanPlayer);

		//InfoText.text = "Player " + ((sender as CellGrid).CurrentPlayerNumber);
	}
	private void OnCellDehighlighted(object sender, EventArgs e)
	{
		var tile = sender as SampleSquare;
		if(!IsUnitSelected&&!tile.IsHighlighted)
		{
			UnitImage.color = Color.gray;
	    	StatsText.text = "";
	    	TerrainName.text="";
		}
		
	}
	private void OnCellHighlighted(object sender, EventArgs e)
	{
			var tile = sender as SampleSquare;
			if(!IsUnitSelected&&tile.IsHighlighted)
			{
				UnitImage.GetComponent<Image>().sprite = tile.TileImage;
				TerrainName.text=tile.TerrainType;
			}
	
	}
	private void OnUnitAttacked(object sender, AttackEventArgs e)
	{
		if (!(CellGrid.CurrentPlayer is HumanPlayer)) return;
		OnUnitDehighlighted(sender, new EventArgs());

		if ((sender as Unit).HitPoints <= 0) return;

		OnUnitHighlighted(sender, e);
	}
	private void OnUnitDehighlighted(object sender, EventArgs e)
	{
		var unit = sender as SimpleUnit;
		if(!IsUnitSelected)
		{
			StatsText.text = "";
			UnitNameText.text="";
			//PointsText.text="";
			UnitImage.GetComponent<Image>().sprite = null;
			UnitImage.color = Color.gray;
			unit.AttackButton.SetActive(false);
			unit.MoveButton.SetActive(false);
			unit.DefendButton.SetActive(false);
			unit.WaitButton.SetActive(false);
		}
		if(unit.PlayerNumber>1)
		{
				unit.AttackButton.SetActive(false);
				unit.MoveButton.SetActive(false);
				unit.DefendButton.SetActive(false);
				unit.WaitButton.SetActive(false);
		}
	}
	private void OnUnitHighlighted(object sender, EventArgs e)
	{
		var unit = sender as SimpleUnit;
		if(!IsUnitSelected)
		{
		UnitNameText.text = unit.UnitName;
		UnitNameText.fontStyle=FontStyle.Bold;
		//PointsText.text="\n Action Points: "+ unit.ActionPoints +"/"+unit.TotalActionPoints +"\n Move Points: "+ unit.MovementPoints +"/"+unit.TotalMovementPoints; 
		StatsText.text = "\n\n Hit Points: " + unit.HitPoints +"/"+unit.TotalHitPoints + "\n Phys.Attack: " + unit.PhysicalAttack+ "\n Phys.Defense: " + unit.PhysicalDefense + "\n Att.Range: " + unit.AttackRange;
		UnitImage.GetComponent<Image>().sprite = unit.PlayerPicture;
		TerrainName.text="";
		unit.AttackButton.SetActive(true);
		unit.MoveButton.SetActive(true);
		unit.DefendButton.SetActive(true);
		unit.WaitButton.SetActive(true);
		if(!unit.CanAttack||unit.ActionPoints==0)
		{
			unit.AttackButton.GetComponent<Button>().interactable=false;
			unit.AttackButton.GetComponent<Image>().color=Color.gray;
		}
		if(unit.MovementPoints>0)
		{
			unit.MoveButton.GetComponent<Button>().interactable=true;
			unit.MoveButton.GetComponent<Image>().color=Color.white;
			unit.WaitButton.GetComponent<Button>().interactable=true;
			unit.WaitButton.GetComponent<Image>().color=Color.white;
		}
		if(unit.MovementPoints==0)
		{
			unit.MoveButton.GetComponent<Button>().interactable=false;
			unit.MoveButton.GetComponent<Image>().color=Color.gray;
		}
		}
		if(unit.CanAttack&&unit.ActionPoints>0)
		{
			unit.AttackButton.GetComponent<Button>().interactable=true;
			unit.AttackButton.GetComponent<Image>().color=Color.white;
		}
		if(unit.ActionPoints>0)
		{
			unit.DefendButton.GetComponent<Button>().interactable=true;
			unit.DefendButton.GetComponent<Image>().color=Color.gray;
		}
		if(unit.ActionPoints==0)
		{
			unit.DefendButton.GetComponent<Button>().interactable=false;
			unit.DefendButton.GetComponent<Image>().color=Color.gray;
			unit.AttackButton.GetComponent<Button>().interactable=false;
			unit.AttackButton.GetComponent<Image>().color=Color.gray;
			unit.WaitButton.GetComponent<Button>().interactable=false;
			unit.WaitButton.GetComponent<Image>().color=Color.gray;
		}
	
		if(unit.PlayerNumber>1)
		{
			unit.AttackButton.SetActive(false);
			unit.MoveButton.SetActive(false);
			unit.DefendButton.SetActive(false);
			unit.WaitButton.SetActive(false);
		}
		
	}
	public void RestartLevel()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

}
