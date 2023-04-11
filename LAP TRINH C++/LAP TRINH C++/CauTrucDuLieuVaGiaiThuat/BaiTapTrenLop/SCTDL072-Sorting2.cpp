#include<bits/stdc++.h>
using namespace std;

bool compare(int a, int b, int x) {
    return abs(x - a) < abs(x - b);
}

void sort_absolute(int arr[], int n, int x) {
    sort(arr, arr + n, [x](int a, int b) { return compare(a, b, x); });
}

int main() {
    int t;
    cin >> t;

    while (t--) {
        int n, x;
        cin >> n >> x;

        int arr[n];
        for (int i = 0; i < n; i++) {
            cin >> arr[i];
        }

        sort_absolute(arr, n, x);

        for (int i = 0; i < n; i++) {
            cout << arr[i] << " ";
        }
        cout << endl;
    }

    return 0;
}
