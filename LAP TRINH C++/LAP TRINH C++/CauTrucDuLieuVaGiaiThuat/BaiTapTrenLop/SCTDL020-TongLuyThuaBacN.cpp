#include<bits/stdc++.h>

using namespace std; 

int count_unique_ways(int m, int n, int last = 0) {
    if (m == 0) {
        return 1;
    }
    
    int count = 0;
    int limit = (last == 0) ? int(sqrt(m)) : int(pow(m - 1, 1.0 / n)) + last;
    for (int i = 1; i <= limit; i++) {
        int x = int(pow(i, n));
        if (x <= m && i > last) {
            count += count_unique_ways(m - x, n, i);
        }
    }
    
    return count;
}

int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        int m,n;
        cin >> m >> n;
        int count = count_unique_ways(m,n);
        cout << count << endl;
    }
    return 0;
}