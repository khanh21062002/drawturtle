#include<stdio.h>
#include<iostream>
#include<math.h>

using namespace std;

int main(){
    int t;
    cin >>t;
    while (t--)
    {
        int m,n,p;
        int i,j,k,l;
        int a[100],b[100],kq[100];
        cin >> n;
        for(i = 0; i <= n; i++){
            cin >> a[i];
        }
        cin >> m;
        for(j = 0; j<=m ; j++){
            cin >> b[j];
        }
        if(n>m){
            p = n;
            for(k = p; k > m ; k-- ){
                kq[k] = a[k];
            }
            for(l = m ; l>=0;l-- ){
                kq[l] = a[l] + b[l];
            }
        }
        else{
            p =m;
            for(int k = p; k > n ; k-- ){
                kq[k] = b[k];
            }
            for(int l = n; l>=0;l-- ){
                kq[l] = a[l] + b[l];
            }
        }
        cout << p << "\n";
        for (int i = 0; i<=p ; i++){
            cout << kq[i] << " ";
        }
        cout << "\n";
    }
    return 0;
}