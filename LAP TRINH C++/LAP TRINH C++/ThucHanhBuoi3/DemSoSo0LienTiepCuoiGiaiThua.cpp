#include<bits/stdc++.h>
#include<string.h>

using namespace std;

long long int DemChuSo0(long long int n)
{
    long long dem = 0;
    for(long long i = 5 ; i <= n ; i=i*5 )
    {
        dem = dem + n/i;
    }
    return dem;
}

main()
{
    int t;
    cin >> t;
    while (t--)
    {
        long long int n;
        cin >> n;
        cout << DemChuSo0(n) << endl;
        
    }
    return 0;
}
