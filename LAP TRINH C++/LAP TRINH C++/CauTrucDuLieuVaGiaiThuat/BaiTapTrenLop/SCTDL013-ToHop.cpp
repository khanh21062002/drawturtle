#include <iostream>
using namespace std;

void printSubset(int a[], int n, int k, int i, int j) {
    if (j == k) {
        for (int x = 0; x < k; x++) {
            cout << a[x] << "";
        }
        cout << " ";
        return;
    }
    if (i > n) {
        return;
    }
    a[j] = i;
    printSubset(a, n, k, i+1, j+1);
    printSubset(a, n, k, i+1, j);
}

int main() {
    int t;
    cin >> t;
    while (t--)
    {
        int N, K;
        cin >> N >> K;
        int a[K];
        printSubset(a, N, K, 1, 0);
        cout << endl;
    }
    return 0;
}
