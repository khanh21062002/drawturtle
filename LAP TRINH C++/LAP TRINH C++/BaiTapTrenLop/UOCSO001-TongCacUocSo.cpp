#include<bits/stdc++.h>

using namespace std;

int main(){
    int t;
    cin >> t;
    while (t--)
    {
        long long n;
        cin >> n;
        if ( n > 1 ){
            long long s =1;
            for(int i = 2 ; i <= sqrt(n) ; i++){
                if(n % i == 0 ){
                    if(i != n/i) {
                      s += n/i;
                    }
                    s += i;
                }
            }
            cout << s << endl;
        }
        else{
            cout << 0 << endl;
        }
    }
    return 0;
}
