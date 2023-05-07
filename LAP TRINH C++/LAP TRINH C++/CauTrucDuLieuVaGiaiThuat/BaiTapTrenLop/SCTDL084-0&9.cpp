#include <bits/stdc++.h>

using namespace std;

int Check(int n)
{ 
    int dem = 0;
    while (n > 0)
    {
        int a = n % 10;
        if (a != 0 && a != 9)
            dem++;
        n /= 10;
    }
    if (dem == 0)
        return 1;
    else
        return 0;
}

int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        queue<int> Q;
        int i = 1;   
        int dem = 0; 
        int x;       
        cin >> x;
        while (dem == 0)
        {
            Q.push(9 * i);
            if (Q.front() % x == 0 && Check(Q.front()) == 1)
            {
                cout << Q.front() << endl;
                dem++;
                break;
            }
            else
            {
                i++;
                Q.pop();
            }
        }
    }
    return 0;
}