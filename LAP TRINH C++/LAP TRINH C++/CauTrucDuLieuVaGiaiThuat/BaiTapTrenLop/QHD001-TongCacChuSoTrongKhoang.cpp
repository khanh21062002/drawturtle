#include<bits/stdc++.h>
#define ll long long
using namespace std;
ll xu_ly(ll x,ll A)
{
    if(A == 0)
        return 0;
    ll B = A, t = 0,mu10 = 1;
    while(B>=10)
    {
        B = B/10;
        t++;
        mu10 = mu10*10;
    }
    ll y = A/mu10, a = (A - y*mu10);
    if(y<x)
         return y*t*(mu10/10) + xu_ly(x,a);
    else if(y>x)
         return mu10 + y*t*(mu10/10) + xu_ly(x,a);
    else if(y == x)
         return a+1+y*t*(mu10/10) + xu_ly(x,a);
}

ll sochuso(ll A)
{
    ll KQ = 0;
    ll B = A, t = 0,mu10 = 1;
    while(B>=10)
    {
        B = B/10;
        t++;
        mu10 = mu10*10;
    }
    for(int i = 1;i<=t;i++)
    {
        ll nhan10 = 9*i;
        for(int j = 2;j<=i;j++)
            nhan10 = nhan10*10;
        KQ = KQ + nhan10;
    }
    KQ = KQ + (A-mu10+1)*(t+1);
    return KQ;
}

int main()
{	
	ios_base::sync_with_stdio(0);
	cin.tie(0), cout.tie(0);
    int t;
    cin >>t;
    while(t--){
        ll u[10],a,b;
        cin >>a >>b;
        if(a>b) swap(a,b);
        ll hiep = 0;
        for(int i = 1;i<=9;i++){
                u[i] = xu_ly(i,b)- xu_ly(i,a-1);
                hiep = hiep + u[i];
        }
        u[0] = sochuso(b) - sochuso(a-1) - hiep;
		long long ans=0;
        for(int i = 0;i<=9;i++) ans += u[i]*i;
		cout <<ans;
        cout <<'\n';

    }
}