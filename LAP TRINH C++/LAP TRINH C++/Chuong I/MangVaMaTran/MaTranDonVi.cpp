#include<stdio.h>
#include<iostream>
#include<math.h>

using namespace std;

void nhapMaTran(int x[100][100],int n){
    int i,j;
    for( i=0 ; i<n;i++){
        for(j=0;j<n;j++){
            cin >> x[i][j];
        }
    }
}
int donvi(int n, int A[100][100])
{
            int i, j;
            for(i=0; i<n; i++)
            {
                        if(A[i][i] !=1)
                        {
                                    return 0;
                        }
            }
            for(i=0; i<n; i++)
            {
                        for(j=i+1; j<n; j++)
                        {
                                    if((A[i][j] !=0) || (A[j][i] !=0))
                                    {
                                                return 0;
                                    }
                        }
            }
            return 1;
}
int main(){
    int t;
    cin >> t;
    while (t--)
    {
        int n, a[100][100];
        cin >> n;
        nhapMaTran(a,n);
        if(donvi(n,a)==1){
            cout<<"YES\n";
        }
        if(donvi(n,a)==0){
            cout<<"NO\n";
        }
    }
    return 0;
}