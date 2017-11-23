using System.Collections.Generic;
using System;

public class functionalDependency {
	public int LHS; //32 bits
	public int RHS; //32 bits
	public functionalDependency(int LHS, int RHS) {
		this.LHS = LHS;
		this.RHS = RHS;
	}
}
public class findKeys {
	public static List<int> solution(List<functionalDependency> myFDs, int numVars) {
		int mask = (int) Math.Pow(2, numVars) - 1;
		int key = mask;
		List<int> keys = new List<int>();
		while(mask >= 1) {
			int nextNum = mask - 1;
			int origNumber = mask;
			bool done = false;
			while(!done) {
				done = true;
				for(int i = 0; i < myFDs.Count; i++) {
					if(mask == key) {
						keys.Add(origNumber);
						done = true;
						break;
					}
					if((mask & myFDs[i].LHS) == myFDs[i].LHS) {
						if((mask & myFDs[i].RHS) != myFDs[i].RHS) {
							done = false;
							mask = mask | myFDs[i].RHS;
						}
					}

				}
			}
			mask = nextNum;
		}
		return(keys);
	}
	public static void printKeyCombo(int mask) {
		if((mask & 1) != 0) { System.Console.Write("A"); }
		if((mask & 2) != 0) { System.Console.Write("B"); }
		if((mask & 4) != 0) { System.Console.Write("C"); }
		if((mask & 8) != 0) { System.Console.Write("D"); }
		if((mask & 16) != 0) { System.Console.Write("E"); }
		if((mask & 32) != 0) { System.Console.Write("F"); }
		if((mask & 64) != 0) { System.Console.Write("G"); }
		if((mask & 128) != 0) { System.Console.Write("H"); }
		if((mask & 256) != 0) { System.Console.Write("I"); }
		System.Console.WriteLine("");
	}
	public static void Main(string[] args) { // One Hot Encoding
		List<functionalDependency> myList = new List<functionalDependency>();
		// A = 1, B = 2, C = 4...
		myList.Add(new functionalDependency(0x1 | 0x2, 0x4 | 0x8)); //AB -> CD
		myList.Add(new functionalDependency(0x1 | 0x4 | 0x8 | 0x10, 0x2 | 0x20)); //ACDe -> BF
		myList.Add(new functionalDependency(0x2, 0x1 | 0x4 | 0x8));
		myList.Add(new functionalDependency(0x4 | 0x8, 0x1 | 0x20));
		myList.Add(new functionalDependency(0x4 | 0x8 | 0x10, 0x20 | 0x40));
		myList.Add(new functionalDependency(0x10 | 0x2, 0x8));
		List<int> allMyKeys = solution(myList, 8);
		for(int i = 0; i < allMyKeys.Count; i++) {
			for(int j = 0; j < allMyKeys.Count; j++) {
				if(i == j) { continue; }
				if(i < allMyKeys.Count && j < allMyKeys.Count && (allMyKeys[i] & allMyKeys[j]) == allMyKeys[i]) { // jth key is superset of ith key
					allMyKeys.Remove(allMyKeys[j]);
					i = 0;
					j = 0;
				}
			}
		}
		for(int i = 0; i < allMyKeys.Count; i++) {
			printKeyCombo(allMyKeys[i]);
		}
	}
}
