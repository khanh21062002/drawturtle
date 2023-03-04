#include <bits/stdc++.h>
#define ll long long

#define faster() ios_base::sync_with_stdio(false),cin.tie(0),cout.tie(0);
const int MOD=1e9+7;

using namespace std;

int main()
{
    faster();
    int t;
    cin >>t;
    while(t--){
        string a, b;
        cin >>a >>b;
        ll asum=0, bsum=0, asum1=0, bsum1=0;
        for(int i = 0; i < a.size(); ++i){
            if(a[i] == '3') a[i] = '5';
            asum = asum*10 + a[i]-48;
        }
        for(int i = 0; i < b.size(); ++i){
            if(b[i] == '3') b[i] = '5';
            bsum = bsum*10 + b[i]-48;
        }
        asum += bsum;
        for(int i = 0; i < a.size(); ++i){
            if(a[i] == '5') a[i] = '3';
            asum1 = asum1*10 + a[i]-48;
        }
        for(int i = 0; i < b.size(); ++i){
            if(b[i] == '5') b[i] = '3';
            bsum1 = bsum1*10 + b[i]-48;
        }
        asum1 += bsum1;
        cout <<asum1 <<' '<<asum <<'\n';
    }

   return 0;
}