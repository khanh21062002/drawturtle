#include<bits/stdc++.h>

using namespace std;

int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        int k , s , count = 0;
        cin >> k >> s;
        for( int x = 0 ; x <=k ; x++)
        {
            int yz = s - x;
            if(yz >= 0 && yz <= 2 * k)
            {
                count += min(yz + 1, k + 1) - max(yz - k , 0);
            }
        }
        cout << count << endl;
    }
    return 0;
}