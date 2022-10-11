#include <bits/stdc++.h>
using namespace std;
int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        string S;
        cin >> S;
        if (S[0] == '-')
            cout << S.length() - 1 << endl;
        else
            cout << S.length()<< endl;
        
    }
    return 0;
}