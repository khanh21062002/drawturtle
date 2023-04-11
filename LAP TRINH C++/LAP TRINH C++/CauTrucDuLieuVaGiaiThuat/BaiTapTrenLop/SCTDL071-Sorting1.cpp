#include <bits/stdc++.h>

using namespace std;

int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        int n;
        cin >> n;
        int a[n];
        for (int i = 0; i < n; i++)
        {
            cin >> a[i];
        }
        sort(a, a + n);

        int *p1 = a, *p2 = a + n - 1;
        while (p1 <= p2)
        {
            if (p1 == p2)
            {
                cout << *p1 << " ";
                break;
            }

            cout << *p2 << " " << *p1 << " ";

            p2--;
            p1++;

            if (p1 == p2)
            {
                cout << *p1 << " ";
                break;
            }
        }

        cout << endl;
    }
    return 0;
}