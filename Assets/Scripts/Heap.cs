public class Heap<T> {
    private HeapNode<T>[] harr;
    public int capacity;
    public int size;

    public Heap(int capacity) {
        this.capacity = capacity;
        harr = new HeapNode<T>[capacity];
        size = 0;
    }

    public void Insert(T data, float value) {
        if (size >= capacity) {
            return;
        }

        HeapNode node = new HeapNode(data, value);

        size++;
        int i = size - 1;
        harr[i] = node;
        while (i != 0 && harr[Parent(i)].value > harr[i].value) {
            HeapNode temp = harr[Parent(i)];
            harr[Parent(i)] = harr[i];
            harr[i] = temp;
            i = Parent(i);
        }
    }

    public T Pop(out float value) {
        if (size <= 0 ) {
            return null;
        }
        if (size == 1) {
            size --;
            return harr[0];
        }
    }

    private int Parent(int i) {
        return (i-1)/2;
    }
}

public struct HeapNode<T> {
    public T data;
    public float value;
    public HeapNode(T data, float value) {
        this.data = data;
        this.value = value;
    }
}