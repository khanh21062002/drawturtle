#include<bits/stdc++.h>
using namespace std;
void tinh(long long n)
{
    for(long long i=2;i*i*i<=n;++i)
    {
        long long t=0;
        while(n%i==0)
        {
            ++t;
            n/=i;
            if(t>1)
            {
                cout<<"YES"<< endl;
                return;
            }
        }
    }
    long long u=sqrt(n);
    if(u*u==n)cout<<"YES"<<endl;
    else cout<<"NO"<<endl;
    return;
}
int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        ios_base::sync_with_stdio(false);
        cin.tie(0),cout.tie(0);
        long long n;
        cin>>n;
        tinh(n);
    }
    return 0;
}