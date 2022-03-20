using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePriorityQueue {
    public class PriorityQueue<T> {
        private List<T> heap;
        private Dictionary<T, int> reverseTab;
        private Comparer<T> comparer;

        public PriorityQueue() {
            heap = new List<T>();
            reverseTab = new Dictionary<T, int>();
            comparer = Comparer<T>.Default;
        }

        public PriorityQueue(Comparer<T> comparer) {
            heap = new List<T>();
            this.comparer = comparer;
            reverseTab = new Dictionary<T, int>();
        }

        public PriorityQueue(List<T> list) {
            reverseTab = new Dictionary<T, int>();
            comparer = Comparer<T>.Default;
            HeapBuild(list);
        }

        public PriorityQueue(List<T> list, Comparer<T> comparer) {
            reverseTab = new Dictionary<T, int>();
            this.comparer = comparer;
            HeapBuild(list);
        }

        public int Count { get => heap.Count; }

        public T Peek() {
            if (heap.Count == 0)
                throw new InvalidOperationException();
            return heap[0];
        }

        public T Poll() {
            T item;
            int size = heap.Count;
            if (size == 0)
                throw new InvalidOperationException();
            Swap(0, size - 1);
            item = heap[size - 1];
            heap.RemoveAt(size - 1);
            reverseTab.Remove(item);
            Heapify(0);
            return item;
        }

        public void Add(T item) {
            int index = heap.Count;
            int parent = ParentIndex(index);
            heap.Add(item);

            while (index >= 1 && comparer.Compare(item, heap[parent]) > 0) {
                Swap(index, parent);
                index = parent;
                parent = ParentIndex(index);
            }
            reverseTab[item] = index;
        }

        public void Update(T item) {
            int index = reverseTab[item];
            int parent = ParentIndex(index);

            while (index >= 1 && comparer.Compare(item, heap[parent]) > 0) {
                Swap(index, parent);
                index = parent;
                parent = ParentIndex(index);
            }
            reverseTab[item] = index;
            Heapify(index);
        }

        public bool Contains(T item) {
            return reverseTab.ContainsKey(item);
        }

        public void Clear() {
            heap.Clear();
            reverseTab.Clear();
        }

        private void HeapBuild(List<T> list) {
            heap = new List<T>(list);
            for (int i = 0; i < heap.Count; i++)
                reverseTab[heap[i]] = i;
            for (int i = (heap.Count - 1) / 2; i >= 0; i--)
                Heapify(i);
        }

        private void Heapify(int index) {
            int left = LeftChildIndex(index);
            int right = RightChildIndex(index);
            int size = heap.Count;
            int max = index;

            if (left < size && comparer.Compare(heap[left], heap[index]) > 0)
                max = left;
            else if (right < size && comparer.Compare(heap[right], heap[max]) > 0)
                max = right;

            if (max != index) {
                Swap(index, max);
                Heapify(max);
            }
        }

        private int LeftChildIndex(int index) {
            return index * 2 + 1;
        }

        private int RightChildIndex(int index) {
            return index * 2 + 2;
        }

        private int ParentIndex(int index) {
            return (index - 1) / 2;
        }

        private void Swap(int index1, int index2) {
            T temp = heap[index1];
            heap[index1] = heap[index2];
            heap[index2] = temp;
            reverseTab[heap[index1]] = index1;
            reverseTab[heap[index2]] = index2;
        }
    }
}
