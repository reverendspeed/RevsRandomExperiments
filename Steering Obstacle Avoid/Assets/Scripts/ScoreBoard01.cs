using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ScoreBoard01 : MonoBehaviour {

	class Employee : IComparable<Employee>{
		public int Score { get; set; }
		public string Name { get; set; }
		
		public int CompareTo(Employee other){
			// Alphabetic sort if salary is equal. [A to Z]
			if (this.Score == other.Score){
				return this.Name.CompareTo(other.Name);
			}
			// Default to salary sort. [High to low]
			return other.Score.CompareTo(this.Score);
		}
		public override string ToString(){
			// String representation.
			return this.Score.ToString() + "," + this.Name;
		}
	}

	void Start () {
		List<Employee> list = new List<Employee>();
		list.Add(new Employee() { Name = "Steve", Score = 10000 });	
		// Uses IComparable.CompareTo()
		list.Sort();
		// Uses Employee.ToString
		foreach (var element in list){
			Debug.Log (element);
		}
	}
}


//		list.Add(new Employee() { Name = "Janet", Salary = 10000 });
//		list.Add(new Employee() { Name = "Andrew", Salary = 10000 });
//		list.Add(new Employee() { Name = "Bill", Salary = 500000 });
//		list.Add(new Employee() { Name = "Lucy", Salary = 8000 });

