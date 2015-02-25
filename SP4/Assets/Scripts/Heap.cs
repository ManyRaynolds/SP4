using UnityEngine;
using System.Collections;
using System;

public class Heap<T>  where T : IHeapItem<T>  {

	T[] items;
	int currentItemCount;

	//Heap Binary Tree
	public Heap(int maxHeapSize) {
		items = new T[maxHeapSize];
	}

	public void Add(T item) {
		item.HeapIndex = currentItemCount;
		items[currentItemCount] = item;
		SortUp (item);
		currentItemCount++;
	}

	//Removing from first item in the heap
	public T RemoveFirst() {
		T firstItem = items[0];
		currentItemCount--;
		items[0]  = items[currentItemCount];
		items[0].HeapIndex = 0;
		SortDown(items[0]);
		return firstItem;
	}

	public bool Contains(T item) {
		return Equals (items[item.HeapIndex], item);
	}

	public void UpdateItem(T item) {
		SortUp (item);
		//currently do not need to use a SortDown func
	}

	//item accessor
	public int Count {
		get {
			return currentItemCount;
		}
	}

	void SortUp(T item) {
		//Heap uses formula of (n-1)/2 to Upheap
		int parentIndex = (item.HeapIndex-1)/2;

		while (true) {
			T parentItem = items[parentIndex];

			//swapping if got higher priority, 1, if same, 0 else, -1
			if (item.CompareTo(parentItem) > 0) {
				Swap(item,parentItem);
			}
			else{
				break;
			}
		}
	}

	void SortDown(T item) {
		while(true) {
			int childIndexLeft = item.HeapIndex * 2 + 1;
			int childIndexRight = item.HeapIndex * 2 + 2;
			int swapIndex = 0;

			if (childIndexLeft < currentItemCount) {
				swapIndex = childIndexLeft;
				if (childIndexRight < currentItemCount) {
					if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
					{
						swapIndex = childIndexRight;
					}
				}
				if (item.CompareTo(items[swapIndex]) < 0) {
					Swap(item, items[swapIndex]);
				}
				else {
					return;
				}
			}
			else {
				return;
			}
		}
	}

	void Swap(T itemA, T itemB) {
		items[itemA.HeapIndex] = itemB;
		items[itemB.HeapIndex] = itemA;
		int itemAIndex = itemA.HeapIndex;
		itemA.HeapIndex = itemB.HeapIndex;
		itemB.HeapIndex = itemAIndex;
	}
}

public interface IHeapItem<T> : IComparable<T>{
	int HeapIndex {
		get;
		set;
	}
}
