#include<bits/stdc++.h>
#define ll long long 

using namespace std;

bool check( ll n)
{
    if ( n < 0)
    {
        n = n * -1;
    }
    string s = to_string(n);
    for( int i = 0 ; i < s.size() ; i++)
    {
        if((i + 1) % 2 != (s[i] - '0') % 2)
        {
            return false;
        }
    }
    return true;
}

int main() {
    int t;
    cin >> t;
    while (t--)
    {
        ll n;
        cin >> n;
        if(check(n)) cout << "YES\n";
        else cout << "NO\n";   
    }
    
    return 0;
}
