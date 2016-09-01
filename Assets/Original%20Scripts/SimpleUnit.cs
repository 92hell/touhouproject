using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UI;


public class SimpleUnit : Unit {
	public Color PlayerColor;
	private Transform Highlighter;
	public Color LeadingColor; 
	public Sprite PlayerPicture;
	public Image UnitImage;
	public Text InfoText;
	public Text StatsText;
	public Text UnitNameText;
	//public Text PointsText;
	public CellGrid CellParent;
	public SimpleDisplay SimpleDisplay;
	private List<Cell> _pathsInRange;


	public override void Initialize() { 
		base.Initialize(); transform.position += new Vector3(0, 0, -1); GetComponent<Renderer>().material.color = LeadingColor; 
		AttackButton.SetActive(false);
		_pathsInRange = new List<Cell>();
		MoveButton.SetActive(false);
		DefendButton.SetActive(false);
		WaitButton.SetActive(false);
		if(PlayerNumber>1)
		{
			AttackButton.SetActive(false);
			MoveButton.SetActive(false);
		}
		//IsSelected=false;  
		IsGoingToAttack=false;
		IsGoingToMove=false;
		AttackButton.GetComponent<Button>().onClick.AddListener(()=>{
		IsGoingToAttack=!IsGoingToAttack;
		foreach (var currentUnit in CellParent.Units)
        {
            if (currentUnit.PlayerNumber.Equals(this.PlayerNumber))
                continue;
        
            if (this.IsUnitAttackable(currentUnit,this.Cell))
            {
                if(this.IsGoingToAttack)
                currentUnit.SetState(new UnitStateMarkedAsReachableEnemy(currentUnit)); 
            }
        }
		AttackButton.GetComponent<Button>().interactable=false;
		AttackButton.GetComponent<Image>().color=Color.gray;
		
	});
		MoveButton.GetComponent<Button>().onClick.AddListener(()=>{
		IsGoingToMove=!IsGoingToMove;
			
		_pathsInRange = this.GetAvailableDestinations(CellParent.Cells);
        foreach (var cell in _pathsInRange)
        {
            if(this.IsGoingToMove)
            cell.MarkAsReachable();
        }
        	MoveButton.GetComponent<Button>().interactable=false;
			MoveButton.GetComponent<Image>().color=Color.gray;

		});
		DefendButton.GetComponent<Button>().onClick.AddListener(()=>{
			IsDefending=true;
			this.PhysicalDefense+=2;
			this.StatsText.text="";
			this.StatsText.text = "\n\n Hit Points: " + this.HitPoints +"/"+this.TotalHitPoints + "\n Phys.Attack: " + this.PhysicalAttack+ "\n Phys.Defense: " + this.PhysicalDefense+ "\n Att.Range: " + this.AttackRange;
			this.DefendButton.GetComponent<Button>().interactable=false;
			this.DefendButton.GetComponent<Image>().color=Color.gray;
			this.ActionPoints--;
		});		
		WaitButton.GetComponent<Button>().onClick.AddListener(()=>{
			MovementPoints=0;
			ActionPoints=0;
			OnUnitDeselected();
		});

	} 
	protected override void Defend(Unit other, int damage)
	{
		base.Defend(other, damage);
		UpdateHpBar();
	}

	public override void Move(Cell destinationCell, List<Cell> path)
	{
		if(IsGoingToMove)
		base.Move(destinationCell, path);
	}
	private IEnumerator Jerk(Unit other)
	{
		var heading = other.transform.position - transform.position;
		var direction = heading / heading.magnitude;
		float startTime = Time.time;

		while (startTime + 0.25f > Time.time)
		{
			transform.position = Vector3.Lerp(transform.position, transform.position + (direction / 2.5f), ((startTime + 0.25f) - Time.time));
			yield return 0;
		}
		startTime = Time.time;
		while (startTime + 0.25f > Time.time)
		{
			transform.position = Vector3.Lerp(transform.position, transform.position - (direction / 2.5f), ((startTime + 0.25f) - Time.time));
			yield return 0;
		}
		transform.position = Cell.transform.position + new Vector3(0, 0, -1.5f); ;
	}
	private void UpdateHpBar()
	{
		if (GetComponentInChildren<Image>() != null)
		{
			GetComponentInChildren<Image>().transform.localScale = new Vector3((float)((float)HitPoints / (float)TotalHitPoints), 1, 1);
			GetComponentInChildren<Image>().color = Color.Lerp(Color.red, Color.green,
				(float)((float)HitPoints / (float)TotalHitPoints));
		}
	}
	public void GoingToAttack(bool IsGoingToAttack)
	{
		IsGoingToAttack=!IsGoingToAttack;
		//Debug.Log(IsGoingToAttack);
		//Debug.Log(UnitName);
	}
	public override void MarkAsAttacking(Unit other)
	{      
		
	}

	public override void MarkAsDefending(Unit other)
	{       
	}

	public override void MarkAsDestroyed()
	{      
	}

	public override void MarkAsFinished()
	{
	}
	public override void MarkAsFriendly() { 
		IsSelected=false;
		//Debug.Log(IsSelected);
		GetComponent<Renderer>().material.color = LeadingColor + new Color(0.8f, 1, 0.8f); 
	}
	public override void MarkAsReachableEnemy() { 
			MoveButton.SetActive(false);
			GetComponent<Renderer>().material.color =Color.red ; 
	}
	public override void OnUnitDeselected()
	{
		MarkAsFriendly();
		SimpleDisplay.IsUnitSelected=false;
		SimpleDisplay.UnitImage.color = Color.gray;
	    SimpleDisplay.StatsText.text = "";
	    SimpleDisplay.UnitNameText.text="";
	    //SimpleDisplay.PointsText.text="";
	    SimpleDisplay.TerrainName.text="";
	    AttackButton.SetActive(false);
	    MoveButton.SetActive(false);
	    DefendButton.SetActive(false);
	    WaitButton.SetActive(false);
	    IsGoingToAttack=false;
	    IsGoingToMove=false;
	    MoveButton.GetComponent<Button>().interactable=true;
		MoveButton.GetComponent<Image>().color=Color.white;
		AttackButton.GetComponent<Button>().interactable=true;
		AttackButton.GetComponent<Image>().color=Color.white;
		DefendButton.GetComponent<Button>().interactable=true;
		DefendButton.GetComponent<Image>().color=Color.white;
		WaitButton.GetComponent<Button>().interactable=true;
		WaitButton.GetComponent<Image>().color=Color.white;
	}
	public override void MarkAsSelected() { 
			this.GetComponent<Renderer>().material.color = Color.green;
		}
		 
	public override void OnUnitSelected(){
			MarkAsSelected();
			SimpleDisplay.IsUnitSelected=true;
			this.UnitNameText.text = this.UnitName;
			this.UnitNameText.fontStyle=FontStyle.Bold;
			//this.PointsText.text="\n Action Points: "+ this.ActionPoints +"/"+this.TotalActionPoints +"\n Move Points: "+ this.MovementPoints +"/"+this.TotalMovementPoints; 
			this.StatsText.text = "\n\n Hit Points: " + this.HitPoints +"/"+this.TotalHitPoints + "\n Phys.Attack: " + this.PhysicalAttack+ "\n Phys.Defense: " + this.PhysicalDefense + "\n Att.Range: " + this.AttackRange;
			this.UnitImage.GetComponent<Image>().sprite = this.PlayerPicture;
			AttackButton.SetActive(true);
			DefendButton.SetActive(true);
			WaitButton.SetActive(true);
			if(!this.CanAttack)
			{
				AttackButton.GetComponent<Button>().interactable=false;
				AttackButton.GetComponent<Image>().color=Color.gray;
			}
			if(this.CanAttack)
			{
				AttackButton.GetComponent<Button>().interactable=true;
				AttackButton.GetComponent<Image>().color=Color.white;
			}
			if(this.MovementPoints>0&&!this.IsGoingToMove)
			{
				this.MoveButton.SetActive(true);
				this.MoveButton.GetComponent<Button>().interactable=true;
				this.MoveButton.GetComponent<Image>().color=Color.white;
			}
			if(this.ActionPoints>0)
			{
				this.DefendButton.SetActive(true);
				this.DefendButton.GetComponent<Button>().interactable=true;
				this.DefendButton.GetComponent<Image>().color=Color.white;
			}
			if(this.MovementPoints==0)
			{
				this.MoveButton.SetActive(true);
				this.MoveButton.GetComponent<Button>().interactable=false;
				this.MoveButton.GetComponent<Image>().color=Color.gray;
			}
	}
	public override void UnMark() { 
		GetComponent<Renderer>().material.color = LeadingColor; 
	}

}
	
