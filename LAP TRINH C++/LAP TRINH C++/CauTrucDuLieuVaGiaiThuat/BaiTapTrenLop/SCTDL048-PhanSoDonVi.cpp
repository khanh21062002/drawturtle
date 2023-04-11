#include<bits/stdc++.h>
using namespace std;

int main()
{
    long long tu, mau;
    int t; cin >>t;
    while (t--)
    {
        cin >> tu >> mau;
        long long res;
        while (1)
        {
            if (mau%tu==0)
            {
                cout << "1/"<< mau/tu<<"\n";
                break;
            }
            else
            {
                res = mau/tu +1;
                cout << "1/" << res << " + ";
                tu = tu*res -mau;
                mau = mau*res;
            }
        }
        
    }
    return 0;
}