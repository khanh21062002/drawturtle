#include<bits/stdc++.h>
#include<iostream>

using namespace std;

bool KTraSoLe(long long int n){
    if( n%2==0 ) {
        return false;
    }
    else{
        return true;
    }
}


int main(){
    int t;
    cin >> t;
    while (t--)
    {
        unsigned long l,r;
        cin >> l >> r;
        unsigned long long tong = 0;
        if(KTraSoLe(l)==true && KTraSoLe(r) == true){
            tong = (((r+l)/2)*(((r-l)/2)+1)) ;
            cout << tong << endl;
        }
        else if (KTraSoLe(l) == false  && KTraSoLe(r) == true)
        {
            tong = (((r+(l+1))/2)*(((r-(l+1))/2)+1)) ;
            cout << tong << endl;
        }
        else if (KTraSoLe(l) == true  && KTraSoLe(r) == false)
        {
            tong = ((((r-1)+l)/2)*((((r-1)-l)/2)+1)) ;
            cout << tong << endl;
        }
        else if (KTraSoLe(l) == false  && KTraSoLe(r) == false)
        {
            tong = ((((r-1)+(l+1))/2)*((((r-1)-(l+1))/2)+1)) ;
            cout << tong << endl;
        }
        else{
            cout << "-1" << endl;
        }
    }
    return 0;
}