#include<bits/stdc++.h>
#include<string.h>

using namespace std;

int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        // string child,bang_1,parent,bang_2;
        string a,b;
        cin >> a >> b;
        // cin >> child >> bang_1 >> a >> parent >> bang_2 >> b;
        cout << b.find(a) << endl;
        getchar();
        //cout << child << bang_1 << a << parent << bang_2 << b<< endl;
    }
    return 0;
}