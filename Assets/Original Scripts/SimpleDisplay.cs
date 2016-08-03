using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SimpleDisplay : MonoBehaviour {

	public CellGrid CellGrid; 
	public GameObject UnitsParent;
	public Button NextTurnButton;
	public Image UnitImage;
	public Text InfoText;
	public Text StatsText;
	public Text UnitNameText;
	public Text PointsText;
	public Text TerrainName;
	public Text TerrainType;

	void Start()
	{
		UnitImage.color = Color.gray;
		CellGrid.GameStarted += OnGameStarted;
		CellGrid.TurnEnded += OnTurnEnded;   
		//CellGrid.GameEnded += OnGameEnded;
	}
		void Update () { 
		if(Input.GetKeyDown(KeyCode.N)) { 
			CellGrid.EndTurn();
			//User ends his turn by pressing "n" on keyboard. 
		}
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
		//	cell.GetComponent<Cell>().CellDehighlighted += OnCellDehighlighted;
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

		InfoText.text = "Player " + ((sender as CellGrid).CurrentPlayerNumber);
	}
	//private void OnCellDehighlighted(object sender, EventArgs e)
	//{
	//	UnitImage.color = Color.gray;
	//	StatsText.text = "";
	//}
	private void OnCellHighlighted(object sender, EventArgs e)
	{
		UnitImage.color = Color.gray;
		var tile = sender as SampleSquare;
		UnitImage.GetComponent<Image>().sprite = tile.TileImage;
		TerrainName.text=tile.TerrainType;
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
		StatsText.text = "";
		UnitNameText.text="";
		PointsText.text="";
		UnitImage.GetComponent<Image>().sprite = null;

		//UnitImage.color = Color.gray;
	}
	private void OnUnitHighlighted(object sender, EventArgs e)
	{
		var unit = sender as SimpleUnit;
		UnitNameText.text = unit.UnitName;
		UnitNameText.fontStyle=FontStyle.Bold;
		PointsText.text="\n Action Points: "+ unit.ActionPoints +"/"+unit.TotalActionPoints +"\n Move Points: "+ unit.MovementPoints +"/"+unit.TotalMovementPoints; 
		StatsText.text = "\n\n Hit Points: " + unit.HitPoints +"/"+unit.TotalHitPoints + "\n Phys.Attack: " + unit.PhysicalAttack+ "\n Phys.Defense: " + unit.PhysicalDefense + "\n Att.Range: " + unit.AttackRange;
		UnitImage.GetComponent<Image>().sprite = unit.PlayerPicture;

	}

	public void RestartLevel()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

}
