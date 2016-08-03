using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SimpleUnit : Unit {
	public Color PlayerColor;

	public string UnitName;

	private Transform Highlighter;
	public Color LeadingColor; 
	public Sprite PlayerPicture;
	public override void Initialize() { 
		base.Initialize(); transform.position += new Vector3(0, 0, -1); GetComponent<Renderer>().material.color = LeadingColor; 
	} 
	protected override void Defend(Unit other, int damage)
	{
		base.Defend(other, damage);
		UpdateHpBar();
	}

	public override void Move(Cell destinationCell, List<Cell> path)
	{
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
		GetComponent<Renderer>().material.color = LeadingColor + new Color(0.8f, 1, 0.8f); 
	}
	public override void MarkAsReachableEnemy() { 
		GetComponent<Renderer>().material.color =Color.red ; 
	}
	public override void MarkAsSelected() { 
		GetComponent<Renderer>().material.color = Color.green; 
	}
	public override void UnMark() { 
		GetComponent<Renderer>().material.color = LeadingColor; 
	}
}
	
