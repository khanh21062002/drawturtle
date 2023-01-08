#include<bits/stdc++.h>
#define ll long long
 
using namespace std;
int main(){
    int t;
    cin >> t;
    while(t--)
    {
        int n,k;
        cin >> n >> k;
        int a[n];
        for(int i = 0 ; i < n; i++)
        {
            cin >> a[i];
        }
        int temp = 0;
        int min_sum = 0;
        for ( int i = 0; i < n - k + 1 ; i++)
        {
            int sum = 0;
            for(int j = i; j < i + k ;j++)
            {
                sum += a[j];
            }
            if( sum <= min_sum || min_sum == 0)
            {
                min_sum = sum;
                temp++;
            }
        }
        cout << temp << endl;
    }
    return 0; 
}