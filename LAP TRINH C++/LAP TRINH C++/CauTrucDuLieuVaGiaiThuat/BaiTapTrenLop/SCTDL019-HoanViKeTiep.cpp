#include<bits/stdc++.h>

using namespace std;

void nextPermutation(int X[], int n) {
    int i, j;
    // Tìm vị trí i đầu tiên từ cuối cùng của mảng sao cho X[i] < X[i+1]
    for (i = n - 2; i >= 0; i--) {
        if (X[i] < X[i+1]) {
            break;
        }
    }
    // Nếu không tìm thấy vị trí i thì hoán vị hiện tại là hoán vị cuối cùng
    if (i < 0) {
        reverse(X, X+n);
        return;
    }
    // Tìm vị trí j đầu tiên từ cuối cùng của mảng sao cho X[j] > X[i]
    for (j = n - 1; j > i; j--) {
        if (X[j] > X[i]) {
            break;
        }
    }
    // Hoán đổi X[i] và X[j]
    swap(X[i], X[j]);
    // Đảo ngược các phần tử từ i+1 đến cuối cùng của mảng
    reverse(X+i+1, X+n);
}

int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        int N ;
        cin >> N;
        int arr[N];
        for(int i = 0 ; i < N ; i++)
        {
            cin >> arr[i];
        }
        nextPermutation(arr, N);
        for (int i = 0; i < N; i++)
        {
            cout << arr[i] << " ";
        }
        cout << endl;
    }
    
}