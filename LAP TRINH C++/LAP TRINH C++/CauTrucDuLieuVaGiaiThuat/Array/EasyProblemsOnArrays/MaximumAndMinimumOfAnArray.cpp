#include<bits/stdc++.h>

using namespace std;

struct MaximumAndMinimumOfAnArray
{
    long long min;
    long long max;
};

MaximumAndMinimumOfAnArray getMinMax(long long arr[], long long n)
{
    struct MaximumAndMinimumOfAnArray minmax;

    if (n == 1)
    {
        minmax.min = arr[0];
        minmax.max = arr[0];
        return minmax;
    }

    if(arr[0] < arr[1])
    {
        minmax.min = arr[0];
        minmax.max = arr[1];
    }
    else
    {
        minmax.min = arr[1];
        minmax.max = arr[0];
    }
    for(int i = 2 ; i < n; i++)
    {
        if (arr[i] > minmax.max)    
            minmax.max = arr[i];
             
        else if (arr[i] < minmax.min)    
            minmax.min = arr[i];
    }
    return minmax;
}

int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        long long n;
        cin >> n;
        long long a[n];
        for(int i = 0 ; i < n ; i++)
        {
            cin >> a[i];
        }
        struct MaximumAndMinimumOfAnArray minmax = getMinMax(a,n);

        cout << minmax.min << endl;
        cout << minmax.max << endl;
    }
    return 0;
}
