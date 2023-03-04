#include<bits/stdc++.h>

using namespace std;

int binarySearch(int A[], int N, int K) {
    int l = 0, r = N - 1;
    while (l <= r) {
        int mid = (l + r) / 2;
        if (A[mid] == K) {
            return mid;
        } else if (A[mid] < K) {
            l = mid + 1;
        } else {
            r = mid - 1;
        }
    }
    return -1;
}

int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        int n , k;
        cin >> n >> k;
        int a[n];
        for(int i = 0; i < n ; i++)
        {
            cin >> a[i];
        }
        int result = binarySearch(a , n , k);
        if(result == -1)
        {
            cout << "NO" << endl;
        }
        else
        {
            cout << result + 1 << endl;
        }
    }
    return 0;
}