#include<bits/stdc++.h>

using namespace std;

int dem( int n, int m , int mx)
{

    if(n==0 || mx==0) return 1;
    if(n>=pow(m,mx)) return dem(n-pow(m,mx),m,mx)+dem(n,m,mx-1);
    else return dem(n,m,mx-1);
}
int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        int n,m,mx;
        cin >> n >> m;
        int tmp = 1;
        mx= -1;
        while (tmp <= n)
        {
            tmp = tmp * m;
            mx++;
        }
        cout << dem(n,m,mx) << endl;
    }
    return 0;
}