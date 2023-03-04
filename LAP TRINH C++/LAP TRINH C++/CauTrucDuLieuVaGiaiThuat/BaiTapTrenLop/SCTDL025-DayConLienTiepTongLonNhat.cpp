#include<bits/stdc++.h>

using namespace std;

int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        long long n;
        cin >> n;
        long long A[n];
        for(long long i = 0 ; i < n ; i++ )
        {
            cin >> A[i];
        }

        long long sum = A[0];
        long long maxSum = A[0];
        for(int i = 1 ; i < n ; i++)
        {
            sum = max(sum + A[i], A[i]);
            maxSum = max( maxSum ,sum);
        }
        cout << maxSum << endl;
    }
    return 0;
}