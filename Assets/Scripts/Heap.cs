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

        HeapNode<T> node = new HeapNode<T>(data, value);

        size++;
        int i = size - 1;
        harr[i] = node;
        while (i != 0 && harr[Parent(i)].value > harr[i].value) {
            Swap(i, Parent(i));
            i = Parent(i);
        }
    }

    public T Pop(out float value) {
        if (size <= 0 ) {
            value = 0;
            return default(T);
        }
        if (size == 1) {
            size --;
            value = harr[0].value;
            return harr[0].data;
        }

        HeapNode<T> root = harr[0];
        harr[0] = harr[size-1];
        size--;
        Heapify(0);
        value = root.value;
        return root.data;
    }

    private int Parent(int i) {
        return (i-1)/2;
    }

    private int Left(int i) {
        return 2*i + 1;
    }

    private int Right(int i) {
        return 2*i + 2;
    }

    private void Swap(int i, int j) {
        HeapNode<T> temp = harr[i];
        harr[i] = harr[j];
        harr[j] = temp;
    }

    public void Heapify(int i) {
        int l = Left(i);
        int r = Right(i);
        int smallest = i;
        if (l < size && harr[l].value < harr[i].value) {
            smallest = l;
        }
        if (r < size && harr[r].value < harr[smallest].value) {
            smallest = r;
        }
        if (smallest != i) {
            Swap(i, smallest);
            Heapify(smallest);
        }
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

