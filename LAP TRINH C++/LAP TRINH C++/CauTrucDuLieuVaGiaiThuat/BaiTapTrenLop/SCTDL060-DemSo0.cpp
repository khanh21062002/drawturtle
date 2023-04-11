#include<bits/stdc++.h>
#define faster() ios_base::sync_with_stdio(false),cin.tie(0),cout.tie(0);
using namespace std;

int main()
{
    faster();
    int t;
    cin >> t;
    while (t--)
    {
        int n;
        cin >> n;
        int a[n];
        for(int i = 1 ; i<=n; i++)
        {
            cin >> a[i];
        }
        int count = 0;
        for( int i = 1 ; i <=n ; i++)
        {
            if (a[i] == 0)
            {
                count++;
            }
        }
        cout << count << endl;
    }
    return 0;
}