#include <bits/stdc++.h>
#define MAX 50
#define ll  long long
using namespace std;

int arr[MAX];  
int n, k;

int C(int k, int n) {
    if (k == 0 || k == n) return 1;
    if (k == 1) return n;   
    return C(k - 1, n - 1) + C(k, n - 1);
}

void res() {
    cout << "[";
    for (int i = 1; i <= k; i++){
        cout << arr[i];
        if(i < k) cout << " ";
    }
    cout << "]";
    cout << endl;
}

void key(int i) {
    for (int j = arr[i-1] + 1; j <= n-k+i; j++)  
    {
        arr[i] = j;
        if (i == k) {
            res();
        }
        else{
            key(i + 1);  
        }
    }
}

int main() {
    int t;
	cin >> t;
	while(t--)
    {  
        cin >> n >> k;
        cout << C(k,n) << endl;
        key(1);
    }
    return 0;  
}