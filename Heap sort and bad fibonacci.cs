public class program{ // Espaço O(1) , Tempo O(1)

public static int fibbonaciruim(int n){ // Espaço O(1) , Tempo O(1)
if (n == 1 || n==2){ // Espaço O(1) , Tempo O(1)
    return 1; // Espaço O(1) , Tempo O(1)
}
else{
    return fibbonaciruim(n-1) + fibbonaciruim(n-2); // Espaço O(2^n) , Tempo O(2^n)
}

}

public static int[] HeapSortFunction(int[] arr) // Espaço O(N) , Tempo O(1)
{
    int n = arr.Length; // Espaço O(1) , Tempo O(1)

    for (int i = n / 2 - 1; i >= 0; i--) // Espaço O(1) , Tempo O(1)
    {
        Heapify(arr, n, i); // Espaço O(N) , Tempo O(N)
    }

    for (int i = n - 1; i > 0; i--) // Espaço O(1) , Tempo O(1)
    {
        int temp = arr[0]; // Espaço O(1) , Tempo O(1)
        arr[0] = arr[i]; // Espaço O(1) , Tempo O(1)
        arr[i] = temp; // Espaço O(1) , Tempo O(1)

        Heapify(arr, i, 0); // Espaço O(N) , Tempo O(N)
    }
    return arr; // Espaço O(N) , Tempo O(1)
}
private static void Heapify(int[] arr, int n, int i) // Espaço O(N) , Tempo O(N)
    {
        int largest = i;  // Espaço O(1) , Tempo O(1)
        int leftChild = 2 * i + 1;  // Espaço O(1) , Tempo O(1)
        int rightChild = 2 * i + 2;  // Espaço O(1) , Tempo O(1)

        if (leftChild < n && arr[leftChild] > arr[largest]) // Espaço O(1) , Tempo O(1)
        {
            largest = leftChild; // Espaço O(1) , Tempo O(1)
        }

        if (rightChild < n && arr[rightChild] > arr[largest]) // Espaço O(1) , Tempo O(1)
        {
            largest = rightChild; // Espaço O(1) , Tempo O(1)
        }

        if (largest != i)
        {
            int swap = arr[i]; // Espaço O(1) , Tempo O(1)
            arr[i] = arr[largest]; // Espaço O(1) , Tempo O(1)
            arr[largest] = swap; // Espaço O(1) , Tempo O(1)

            Heapify(arr, n, largest); // Espaço O(N) , Tempo O(N)
        }
    }

static void Main(string[] args){ // Espaço O(1) , Tempo O(1)

int result = fibbonaciruim(7); // Espaço O(1) , Tempo O(1)
Console.WriteLine("Fibbonaci: "+result); // Espaço O(1) , Tempo O(1)

 int[] arr = { 12, 11, 13, 5, 6, 7 }; // Espaço O(N) , Tempo O(1)
Console.WriteLine("Original array:"); // Espaço O(1) , Tempo O(1)
Printarray(arr); // Espaço O(1) , Tempo O(1)

int[] sortedArray = HeapSortFunction(arr); // Espaço O(N) , Tempo O(1)

Console.WriteLine("Sorted array:");  // Espaço O(1) , Tempo O(1)
Printarray(sortedArray); // Espaço O(1) , Tempo O(1)
}

private static void Printarray(int[] arr) // Espaço O(N) , Tempo O(N)
{
    foreach (var item in arr) // Espaço O(N) , Tempo O(N)
    {
        Console.Write(item + " "); // Espaço O(1) , Tempo O(1)
    }
    Console.WriteLine(); // Espaço O(1) , Tempo O(1)
}

}


//Total: Fibbonacci Espaço O(2^N) , Tempo O(2^n)
//Total: Heap sort Espaço O(N) , Tempo O(N log(n))
//Total: Print array Espaço O(N) , Tempo O(N)