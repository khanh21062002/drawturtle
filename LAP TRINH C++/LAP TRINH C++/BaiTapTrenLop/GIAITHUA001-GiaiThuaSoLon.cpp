#include<bits/stdc++.h>

using namespace std;

void GiaiThua(long long n){
    int a[500000] ,i,j,m=1;
    long long r =0,q;
    a[0] = 1;
    for ( i =2 ; i<=n ; i++){
        for ( j = 0; j < m ; j++)
        {
            q = r;
            r = (a[j]*i+r)/10;
            a[j] = (a[j]*i+q)%10;
        }
        while (r>0)
        {
            a[m] = r %10;
            m++;
            r = r / 10;
        }
    }
    for( i = m - 1 ; i >= 0 ; i--){
        cout << a[i];
    }
    cout << endl;
}

int main(){
    int t;
    cin >> t;
    while (t--)
    {
        long long n;
        cin >> n;
        GiaiThua(n);
    }
    return 0;
}