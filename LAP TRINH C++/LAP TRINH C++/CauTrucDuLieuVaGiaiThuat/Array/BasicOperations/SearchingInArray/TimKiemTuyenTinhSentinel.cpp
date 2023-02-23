#include<bits/stdc++.h>

using namespace std;

// Function to search x in the given array
void sentinelSearch(int arr[], int n, int key)
{
 
    // Last element of the array
    int last = arr[n - 1];
 
    // Element to be searched is
    // placed at the last index
    arr[n - 1] = key;
    int i = 0;
 
    while (arr[i] != key)
        i++;
 
    // Put the last element back
    arr[n - 1] = last;
 
    if ((i < n - 1) || (arr[n - 1] == key))
        cout << key << " co mat tai chi so " << i << endl;
    else
        cout << "khong tim thay" << endl;
}


int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        int n , key ;
        cin >> n >> key;
        int arr[n];
        for(int i = 0 ; i < n ; i++)
        {
            cin >> arr[i];
        }
        sentinelSearch(arr,n,key);
    }
    return 0;
}