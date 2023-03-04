#include<bits/stdc++.h>
#define M 102

using namespace std;

int n,x[M],dem = 0;
bool cot[M] , cheo1[M] , cheo2[M];

void qlHoanVi(int i)
{
    for(int j = 1 ; j <=n ; j++)
    {
        if(!cot[j] && !cheo1[i-j+n] && !cheo2[i+j-1])
        {
            x[i] = j;
            cot[j] = true;
            cheo1[i-j+n] = true;
            cheo2[i+j-1] = true;
            if(i==n)
            {
                dem++;
            }
            else{
                qlHoanVi(i+1);
            }
            cot[j] = false;
            cheo1[i-j+n] = false;
            cheo2[i+j-1] = false;
        }
    }
}

int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        cin >> n;
        qlHoanVi(1);
        cout<< dem << endl;
        
    }
    return 0;
}