#include<bits/stdc++.h>

using namespace std;

int binarySearch(int arr[], int low, int high, int x)
{
    while (low <= high) {
        int m = low + (high - low) / 2;
  
        // Check if x is present at mid
        if (arr[m] == x)
            return m;
  
        // If x greater, ignore left half
        if (arr[m] < x)
            low = m + 1;
  
        // If x is smaller, ignore right half
        else
            high = m - 1;
    }
  
    // if we reach here, then element was
    // not present
    return -1;
}

int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        int n , x;
        cin >> n >> x;
        int arr[n];
        for(int i = 0 ; i < n ; i++)
        {
            cin >> arr[n];
        }
        int result = binarySearch(arr , 0 , n-1 , x);
        if (result == -1)
        {
            cout << "Phan tu khong co trong mang"<<endl;
        }
        else
        {
            cout << "Phan tu co mat tai chi so: " <<result << endl;
        }
    }
    return 0;
}